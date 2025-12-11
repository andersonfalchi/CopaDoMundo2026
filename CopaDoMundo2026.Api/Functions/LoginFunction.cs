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
    public class LoginFunction
    {
        private readonly AutenticacaoService _auth;
        private readonly ILogger<LoginFunction> _logger;

        public LoginFunction(AutenticacaoService auth, ILogger<LoginFunction> logger)
        {
            _auth = auth;
            _logger = logger;
        }

        [Function("Login")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")]
        HttpRequestData req)
        {
            _logger.LogInformation("Processando requisição de login");

            var loginDto = await req.ReadFromJsonAsync<LoginDTO>();
            if (loginDto == null)
            {
                throw new ValidationException("login", "Dados de login são obrigatórios");
            }

            if (string.IsNullOrWhiteSpace(loginDto.Usuario))
            {
                throw new ValidationException("nomeUsuario", "Nome de usuário é obrigatório");
            }

            if (string.IsNullOrWhiteSpace(loginDto.Senha))
            {
                throw new ValidationException("senha", "Senha é obrigatória");
            }

            var usuario = await _auth.LoginAsync(loginDto);

            var usuarioSafe = new
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario
            };

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ApiResponse<object>.SuccessResponse(new
            {
                mensagem = $"⚽ Gooool! Bem-vindo de volta, {usuario.NomeUsuario}!",
                usuario = usuarioSafe
            }));

            return response;
        }
    }
}
