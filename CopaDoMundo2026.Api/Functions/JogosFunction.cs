using CopaDoMundo.Models.Models;
using CopaDoMundo2026.Api.Services;
using CopaMundo2026.Models;
using CopaMundo2026.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Functions;

public class JogosFunction
{
    private readonly JogoService _jogoService;
    private readonly ILogger<JogosFunction> _logger;

    public JogosFunction(JogoService jogoService, ILogger<JogosFunction> logger)
    {
        _jogoService = jogoService;
        _logger = logger;
    }

    [Function("GetTodosJogos")]
    public async Task<HttpResponseData> GetTodosJogos(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos")] HttpRequestData req)
    {
        _logger.LogInformation("Buscando todos os jogos");

        try
        {
            var jogos = await _jogoService.ObterTodosJogosAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(jogos);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar jogos");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"Erro: {ex.Message}");
            return errorResponse;
        }
    }

    [Function("GetJogoPorId")]
    public async Task<HttpResponseData> GetJogoPorId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos/{id:int}")] HttpRequestData req,
        int id)
    {
        _logger.LogInformation($"Buscando jogo com ID: {id}");

        try
        {
            var jogo = await _jogoService.ObterJogoPorIdAsync(id);

            if (jogo == null)
            {
                var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await notFoundResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = $"Jogo com ID {id} não encontrado" });
                return notFoundResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new { sucesso = true, jogo });
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar jogo {id}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    [Function("GetJogosPorRodada")]
    public async Task<HttpResponseData> GetJogosPorRodada(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos/rodada/{rodada}")] HttpRequestData req,
        string rodada)
    {
        _logger.LogInformation($"Buscando jogos da rodada: {rodada}");

        try
        {
            var jogos = await _jogoService.ObterJogosPorRodadaAsync(rodada);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(jogos);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar jogos da rodada {rodada}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    [Function("GetJogosPorGrupo")]
    public async Task<HttpResponseData> GetJogosPorGrupo(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos/grupo/{grupo}")] HttpRequestData req,
        string grupo)
    {
        _logger.LogInformation($"Buscando jogos do grupo: {grupo}");

        try
        {
            var jogos = await _jogoService.ObterJogosPorGrupoAsync(grupo);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(jogos);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar jogos do grupo {grupo}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    [Function("GetJogosPorFase")]
    public async Task<HttpResponseData> GetJogosPorFase(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos/fase/{fase:int}")] HttpRequestData req,
        int fase)
    {
        _logger.LogInformation($"Buscando jogos da fase: {fase}");

        try
        {
            var jogos = await _jogoService.ObterJogosPorFaseAsync(fase);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(jogos);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar jogos da fase {fase}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    [Function("GetProximosJogos")]
    public async Task<HttpResponseData> GetProximosJogos(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos/proximos")] HttpRequestData req)
    {
        _logger.LogInformation("Buscando próximos jogos");

        try
        {
            var jogos = await _jogoService.ObterProximosJogosAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(jogos);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar próximos jogos");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    [Function("GetJogosFinalizados")]
    public async Task<HttpResponseData> GetJogosFinalizados(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jogos/finalizados")] HttpRequestData req)
    {
        _logger.LogInformation("Buscando jogos finalizados");

        try
        {
            var jogos = await _jogoService.ObterJogosFinalizadosAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(jogos);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar jogos finalizados");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    //[Function("AtualizarResultadoJogo")]
    //public async Task<HttpResponseData> AtualizarResultadoJogo(
    //    [HttpTrigger(AuthorizationLevel.Function, "put", Route = "jogos/{id:int}/resultado")] HttpRequestData req,
    //    int id)
    //{
    //    _logger.LogInformation($"Atualizando resultado do jogo {id}");

    //    try
    //    {
    //        var resultado = await req.ReadFromJsonAsync<ResultadoJogoDto>();

    //        if (resultado == null)
    //        {
    //            var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
    //            await badRequest.WriteAsJsonAsync(new { sucesso = false, mensagem = "Dados inválidos" });
    //            return badRequest;
    //        }

    //        var (sucesso, mensagem, jogo) = await _jogoService.AtualizarResultadoAsync(id, resultado.QtdGolsSelecaoA, resultado.QtdGolsSelecaoB);

    //        var response = req.CreateResponse(sucesso ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

    //        if (sucesso && jogo != null)
    //        {
    //            await response.WriteAsJsonAsync(new { sucesso = true, mensagem, jogo });
    //        }
    //        else
    //        {
    //            await response.WriteAsJsonAsync(new { sucesso = false, mensagem });
    //        }

    //        return response;
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, $"Erro ao atualizar resultado do jogo {id}");
    //        var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
    //        await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
    //        return errorResponse;
    //    }
    //}

    [Function("CriarJogo")]
    public async Task<HttpResponseData> CriarJogo(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "jogos")] HttpRequestData req)
    {
        _logger.LogInformation("Criando novo jogo");

        try
        {
            var novoJogo = await req.ReadFromJsonAsync<Jogo>();

            if (novoJogo == null)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteAsJsonAsync(new { sucesso = false, mensagem = "Dados do jogo inválidos" });
                return badRequest;
            }

            var (sucesso, mensagem, jogo) = await _jogoService.CriarJogoAsync(novoJogo);

            var response = req.CreateResponse(sucesso ? HttpStatusCode.Created : HttpStatusCode.BadRequest);

            if (sucesso && jogo != null)
            {
                await response.WriteAsJsonAsync(new { sucesso = true, mensagem, jogo });
            }
            else
            {
                await response.WriteAsJsonAsync(new { sucesso = false, mensagem });
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar jogo");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

    [Function("DeletarJogo")]
    public async Task<HttpResponseData> DeletarJogo(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "jogos/{id:int}")] HttpRequestData req,
        int id)
    {
        _logger.LogInformation($"Deletando jogo {id}");

        try
        {
            var (sucesso, mensagem) = await _jogoService.DeletarJogoAsync(id);

            var response = req.CreateResponse(sucesso ? HttpStatusCode.NoContent : HttpStatusCode.NotFound);

            if (!sucesso)
            {
                await response.WriteAsJsonAsync(new { sucesso = false, mensagem });
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao deletar jogo {id}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { sucesso = false, mensagem = ex.Message });
            return errorResponse;
        }
    }

}
