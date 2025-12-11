using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Middlewares
{
    public class CorsMiddleware : IFunctionsWorkerMiddleware
    {
        // ⭐ Configure origens permitidas via environment variable
        private readonly string[] _origensPermitidas;

        public CorsMiddleware()
        {
            var origens = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS") ?? "*";
            _origensPermitidas = origens.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var requestData = await context.GetHttpRequestDataAsync();

            if (requestData != null)
            {
                // Obter origem da requisição
                var origin = requestData.Headers.TryGetValues("Origin", out var origins)
                    ? origins.FirstOrDefault()
                    : null;

                // Tratar requisição OPTIONS (preflight)
                if (requestData.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
                {
                    var preflightResponse = requestData.CreateResponse(HttpStatusCode.OK);
                    AddCorsHeaders(preflightResponse, origin);

                    var invocationResult = context.GetInvocationResult();
                    invocationResult.Value = preflightResponse;
                    return;
                }

                // Continuar para próximo middleware
                await next(context);

                // Adicionar CORS na resposta
                var invocationResult2 = context.GetInvocationResult();
                if (invocationResult2?.Value is HttpResponseData response)
                {
                    AddCorsHeaders(response, origin);
                }
            }
            else
            {
                await next(context);
            }
        }

        private void AddCorsHeaders(HttpResponseData response, string requestOrigin)
        {
            // ⭐ Verificar se origem é permitida
            string allowedOrigin = "*";

            if (_origensPermitidas.Length > 0 && _origensPermitidas[0] != "*")
            {
                // Se origem específica está na lista, permitir
                if (!string.IsNullOrEmpty(requestOrigin) &&
                    _origensPermitidas.Contains(requestOrigin, StringComparer.OrdinalIgnoreCase))
                {
                    allowedOrigin = requestOrigin;
                }
                else
                {
                    allowedOrigin = _origensPermitidas[0]; // Primeira origem como fallback
                }
            }

            if (!response.Headers.Contains("Access-Control-Allow-Origin"))
            {
                response.Headers.Add("Access-Control-Allow-Origin", allowedOrigin);
            }

            var useCredentials = allowedOrigin != "*" ? "true" : "false";

            if (!response.Headers.Contains("Access-Control-Allow-Credentials"))
            {
                response.Headers.Add("Access-Control-Allow-Credentials", useCredentials);
            }

            if (!response.Headers.Contains("Access-Control-Allow-Methods"))
            {
                response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            }

            if (!response.Headers.Contains("Access-Control-Allow-Headers"))
            {
                response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With");
            }

            if (!response.Headers.Contains("Access-Control-Expose-Headers"))
            {
                response.Headers.Add("Access-Control-Expose-Headers", "Content-Length, Content-Type");
            }

            if (!response.Headers.Contains("Access-Control-Max-Age"))
            {
                response.Headers.Add("Access-Control-Max-Age", "600");
            }
        }
    }
}
