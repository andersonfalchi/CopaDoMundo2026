using CopaDoMundo2026.Api.Exceptions;
using CopaDoMundo2026.Api.Models;
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

namespace CopaDoMundo2026.Api.Functions
{
    public class RegistrarFunction
    {
        private readonly AutenticacaoService _auth;
        private readonly ILogger<RegistrarFunction> _logger;

        public RegistrarFunction(AutenticacaoService auth, ILogger<RegistrarFunction> logger)
        {
            _auth = auth;
            _logger = logger;
        }

        [Function("Registrar")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registrar")]
        HttpRequestData req)
        {
            _logger.LogInformation("Processando requisição de registro");

            var registro = await req.ReadFromJsonAsync<RegistroDTO>();
            if (registro == null)
            {
                throw new ValidationException("registro", "Dados de registro são obrigatórios");
            }

            if (string.IsNullOrWhiteSpace(registro.NomeUsuario))
            {
                throw new ValidationException("nomeUsuario", "Nome de usuário é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(registro.Senha))
            {
                throw new ValidationException("senha", "Senha é obrigatória");
            }

            if (registro.Senha.Length < 6)
            {
                throw new ValidationException("senha", "Senha deve ter no mínimo 6 caracteres");
            }

            var usuario = await _auth.RegistrarAsync(registro);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(ApiResponse<object>.SuccessResponse(new
            {
                mensagem = "🎉 Cadastro realizado com sucesso! Bem-vindo ao time!",
                usuario = new
                {
                    Id = usuario.Id,
                    NomeUsuario = usuario.NomeUsuario
                }
            }));

            return response;
        }
    }
}
