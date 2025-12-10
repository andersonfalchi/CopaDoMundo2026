using CopaMundo2026.Models;
using CopaMundo2026.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Functions
{
    public class LoginFunction
    {
        private readonly AutenticacaoService _auth;

        public LoginFunction(AutenticacaoService auth)
        {
            _auth = auth;
        }

        [Function("Login")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequestData req)
        {
            var loginDto = await req.ReadFromJsonAsync<LoginDTO>();
            if (loginDto == null)
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("Dados de login inválidos");
                return bad;
            }

            var (sucesso, mensagem, usuario) = await _auth.LoginAsync(loginDto);

            var response = req.CreateResponse(sucesso ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

            var usuarioSafe = usuario == null ? null : new
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
            };

            await response.WriteAsJsonAsync(new
            {
                sucesso,
                mensagem,
                usuario = usuarioSafe
            });

            return response;
        }
    }
}
