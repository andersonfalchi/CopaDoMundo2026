using CopaDoMundo.Models.Models.DTOs;
using CopaDoMundo2026.Api.Middlewares;
using CopaDoMundo2026.Api.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Functions
{
    public class PalpitesFunction
    {
        private readonly PalpitesService _palpites;
        private readonly JwtService _jwt;
        private readonly ILogger<PalpitesFunction> _logger;

        public PalpitesFunction(
            PalpitesService palpites,
            JwtService jwt,
            ILogger<PalpitesFunction> logger)
        {
            _palpites = palpites;
            _jwt = jwt;
            _logger = logger;
        }

        [Function("CriarPalpite")]
        public async Task<HttpResponseData> CriarPalpite(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "palpites")]
        HttpRequestData req)
        {
            var (autenticado, erro, usuarioId) = await AuthorizationMiddleware
                .ValidarTokenAsync(req, _jwt);

            if (!autenticado)
            {
                var unauthorized = req.CreateResponse(HttpStatusCode.Unauthorized);
                await unauthorized.WriteStringAsync(erro ?? "Não autorizado");
                return unauthorized;
            }

            var palpiteDto = await req.ReadFromJsonAsync<CriarPalpiteDTO>();
            if (palpiteDto == null)
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("Dados inválidos");
                return bad;
            }

            var (sucesso, mensagem, palpite) = await _palpites
                .CriarPalpiteAsync(usuarioId!.Value, palpiteDto);

            var response = req.CreateResponse(sucesso ? HttpStatusCode.Created : HttpStatusCode.BadRequest);
            await response.WriteAsJsonAsync(new { sucesso, mensagem, palpite });

            return response;
        }

        [Function("ObterMeusPalpites")]
        public async Task<HttpResponseData> ObterMeusPalpites(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "palpites/meus")]
        HttpRequestData req)
        {
            var (autenticado, erro, usuarioId) = await AuthorizationMiddleware
                .ValidarTokenAsync(req, _jwt);

            if (!autenticado)
            {
                var unauthorized = req.CreateResponse(HttpStatusCode.Unauthorized);
                await unauthorized.WriteStringAsync(erro ?? "Não autorizado");
                return unauthorized;
            }

            var palpites = await _palpites.ObterPalpitesUsuarioAsync(usuarioId!.Value);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(palpites);

            return response;
        }

        [Function("AtualizarPalpite")]
        public async Task<HttpResponseData> AtualizarPalpite(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "palpites/{id}")]
        HttpRequestData req,
            int id)
        {
            var (autenticado, erro, usuarioId) = await AuthorizationMiddleware
                .ValidarTokenAsync(req, _jwt);

            if (!autenticado)
            {
                var unauthorized = req.CreateResponse(HttpStatusCode.Unauthorized);
                await unauthorized.WriteStringAsync(erro ?? "Não autorizado");
                return unauthorized;
            }

            var palpiteDto = await req.ReadFromJsonAsync<AtualizarPalpiteDTO>();
            if (palpiteDto == null)
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("Dados inválidos");
                return bad;
            }

            var (sucesso, mensagem) = await _palpites
                .AtualizarPalpiteAsync(id, usuarioId!.Value, palpiteDto);

            var response = req.CreateResponse(sucesso ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            await response.WriteAsJsonAsync(new { sucesso, mensagem });

            return response;
        }
    }
}
