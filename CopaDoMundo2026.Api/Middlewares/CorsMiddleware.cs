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
            var origens = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
                ?? "http://localhost:44396,https://localhost:44396";

            _origensPermitidas = origens.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Trim())
                .ToArray(); ;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var requestData = await context.GetHttpRequestDataAsync();

            if (requestData == null)
            {
                await next(context);
                return;
            }

            // Obter origem da requisição
            var origin = requestData.Headers.TryGetValues("Origin", out var origins)
                ? origins.FirstOrDefault()
                : null;

            // Tratar requisição OPTIONS (preflight)
            if (requestData.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                var preflightResponse = requestData.CreateResponse(HttpStatusCode.NoContent);
                AddCorsHeaders(preflightResponse, origin);

                context.GetInvocationResult().Value = preflightResponse;
                return;
            }

            await next(context);

            var invocationResult = context.GetInvocationResult();
            if (invocationResult?.Value is HttpResponseData response)
            {
                AddCorsHeaders(response, origin);
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
                    allowedOrigin = _origensPermitidas[0];
                }
            }

            response.Headers.Remove("Access-Control-Allow-Origin");
            response.Headers.Remove("Access-Control-Allow-Credentials");
            response.Headers.Remove("Access-Control-Allow-Methods");
            response.Headers.Remove("Access-Control-Allow-Headers");
            response.Headers.Remove("Access-Control-Expose-Headers");
            response.Headers.Remove("Access-Control-Max-Age");

            response.Headers.Add("Access-Control-Allow-Origin", allowedOrigin);           
        }
    }
}
