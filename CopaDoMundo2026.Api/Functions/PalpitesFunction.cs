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
        private readonly PalpiteService _palpiteService;
        private readonly ILogger<PalpitesFunction> _logger;

        public PalpitesFunction(PalpiteService palpiteService, ILogger<PalpitesFunction> logger)
        {
            _palpiteService = palpiteService;
            _logger = logger;
        }

        [Function("GetPalpitesUsuario")]
        public async Task<HttpResponseData> GetPalpitesUsuario(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "palpites/usuario/{usuarioId:int}")] HttpRequestData req,
            int usuarioId)
        {
            _logger.LogInformation($"Buscando palpites do usuário {usuarioId}");

            try
            {
                var palpites = await _palpiteService.ObterPalpitesUsuarioAsync(usuarioId);
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(palpites);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar palpites do usuário {usuarioId}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
                return errorResponse;
            }
        }

        [Function("GetPalpitesPorJogo")]
        public async Task<HttpResponseData> GetPalpitesPorJogo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "palpites/jogo/{jogoId:int}")] HttpRequestData req,
            int jogoId)
        {
            _logger.LogInformation($"Buscando palpites do jogo {jogoId}");

            try
            {
                var palpites = await _palpiteService.ObterPalpitesPorJogoAsync(jogoId);
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(palpites);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar palpites do jogo {jogoId}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
                return errorResponse;
            }
        }

        [Function("GetPalpitePorId")]
        public async Task<HttpResponseData> GetPalpitePorId(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "palpites/{id:int}")] HttpRequestData req,
            int id)
        {
            _logger.LogInformation($"Buscando palpite {id}");

            try
            {
                var palpite = await _palpiteService.ObterPalpitePorIdAsync(id);

                if (palpite == null)
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    await notFoundResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = $"Palpite com ID {id} não encontrado" });
                    return notFoundResponse;
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(new { sucesso = true, palpite });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar palpite {id}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
                return errorResponse;
            }
        }

        //[Function("CriarPalpite")]
        //public async Task<HttpResponseData> CriarPalpite(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "palpites")] HttpRequestData req)
        //{
        //    _logger.LogInformation("Criando novo palpite");

        //    try
        //    {
        //        var palpiteDto = await req.ReadFromJsonAsync<PalpiteDto>();

        //        if (palpiteDto == null)
        //        {
        //            var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
        //            await badRequest.WriteAsJsonAsync(new { sucesso = false, mensagem = "Dados do palpite inválidos" });
        //            return badRequest;
        //        }

        //        var (sucesso, mensagem, palpite) = await _palpiteService.CriarPalpiteAsync(palpiteDto);

        //        var response = req.CreateResponse(sucesso ? HttpStatusCode.Created : HttpStatusCode.BadRequest);

        //        if (sucesso && palpite != null)
        //        {
        //            await response.WriteAsJsonAsync(new { sucesso = true, mensagem, palpite });
        //        }
        //        else
        //        {
        //            await response.WriteAsJsonAsync(new { sucesso = false, mensagem });
        //        }

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao criar palpite");
        //        var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
        //        await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
        //        return errorResponse;
        //    }
        //}

        //[Function("SalvarPalpitesLote")]
        //public async Task<HttpResponseData> SalvarPalpitesLote(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "palpites/salvar-lote")] HttpRequestData req)
        //{
        //    _logger.LogInformation("Salvando palpites em lote");

        //    try
        //    {
        //        var palpites = await req.ReadFromJsonAsync<List<PalpiteDto>>();

        //        if (palpites == null || palpites.Count == 0)
        //        {
        //            var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
        //            await badRequest.WriteAsJsonAsync(new { sucesso = false, mensagem = "Nenhum palpite fornecido" });
        //            return badRequest;
        //        }

        //        var resultado = await _palpiteService.SalvarPalpitesLoteAsync(palpites);

        //        var response = req.CreateResponse(HttpStatusCode.OK);
        //        await response.WriteAsJsonAsync(resultado);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao salvar palpites em lote");
        //        var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
        //        await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
        //        return errorResponse;
        //    }
        //}

        //[Function("AtualizarPalpite")]
        //public async Task<HttpResponseData> AtualizarPalpite(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "palpites/{id:int}")] HttpRequestData req,
        //    int id)
        //{
        //    _logger.LogInformation($"Atualizando palpite {id}");

        //    try
        //    {
        //        var palpiteDto = await req.ReadFromJsonAsync<PalpiteDto>();

        //        if (palpiteDto == null)
        //        {
        //            var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
        //            await badRequest.WriteAsJsonAsync(new { sucesso = false, mensagem = "Dados inválidos" });
        //            return badRequest;
        //        }

        //        var (sucesso, mensagem, palpite) = await _palpiteService.AtualizarPalpiteAsync(id, palpiteDto);

        //        var response = req.CreateResponse(sucesso ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

        //        if (sucesso && palpite != null)
        //        {
        //            await response.WriteAsJsonAsync(new { sucesso = true, mensagem, palpite });
        //        }
        //        else
        //        {
        //            await response.WriteAsJsonAsync(new { sucesso = false, mensagem });
        //        }

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Erro ao atualizar palpite {id}");
        //        var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
        //        await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
        //        return errorResponse;
        //    }
        //}

        [Function("DeletarPalpite")]
        public async Task<HttpResponseData> DeletarPalpite(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "palpites/{id:int}")] HttpRequestData req,
            int id)
        {
            _logger.LogInformation($"Deletando palpite {id}");

            try
            {
                var (sucesso, mensagem) = await _palpiteService.DeletarPalpiteAsync(id);

                var response = req.CreateResponse(sucesso ? HttpStatusCode.NoContent : HttpStatusCode.NotFound);

                if (!sucesso)
                {
                    await response.WriteAsJsonAsync(new { sucesso = false, mensagem });
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar palpite {id}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
                return errorResponse;
            }
        }

        //[Function("GetRanking")]
        //public async Task<HttpResponseData> GetRanking(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "palpites/ranking")] HttpRequestData req)
        //{
        //    _logger.LogInformation("Buscando ranking de palpites");

        //    try
        //    {
        //        var ranking = await _palpiteService.ObterRankingAsync();
        //        var response = req.CreateResponse(HttpStatusCode.OK);
        //        await response.WriteAsJsonAsync(ranking);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao buscar ranking");
        //        var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
        //        await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
        //        return errorResponse;
        //    }
        //}
    }

    public class PalpiteDto
    {
        public int UsuarioId { get; set; }
        public int JogoId { get; set; }
        public int QtdGolsSelecaoA { get; set; }
        public int QtdGolsSelecaoB { get; set; }
    }
}
