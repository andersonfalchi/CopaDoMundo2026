using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CorsMiddleware> _logger;

        public CorsMiddleware(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CorsMiddleware>();

            var origens = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
                 ?? "*";

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
                _logger.LogInformation($"✈️ Preflight detectado para origem: {origin}");

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

        private void AddCorsHeaders(HttpResponseData response, string? requestOrigin)
        {
            string allowedOrigin;

            // ⭐ Determinar origem permitida
            if (_origensPermitidas.Contains("*"))
            {
                allowedOrigin = "*";
            }
            else if (!string.IsNullOrEmpty(requestOrigin) &&
                     _origensPermitidas.Any(o => o.Equals(requestOrigin, StringComparison.OrdinalIgnoreCase)))
            {
                allowedOrigin = requestOrigin; // Origem permitida
            }
            else
            {
                // Origem não permitida, mas ainda responde para debug
                _logger.LogWarning($"⚠️ Origem não permitida: {requestOrigin}. Permitidas: {string.Join(", ", _origensPermitidas)}");
                allowedOrigin = _origensPermitidas[0]; // Fallback
            }

            _logger.LogInformation($"🔓 CORS Origin permitida: {allowedOrigin}");

            // Limpar headers existentes
            response.Headers.Remove("Access-Control-Allow-Origin");
            response.Headers.Remove("Access-Control-Allow-Credentials");
            response.Headers.Remove("Access-Control-Allow-Methods");
            response.Headers.Remove("Access-Control-Allow-Headers");
            response.Headers.Remove("Access-Control-Expose-Headers");
            response.Headers.Remove("Access-Control-Max-Age");

            // Adicionar headers CORS
            response.Headers.Add("Access-Control-Allow-Origin", allowedOrigin);

            if (allowedOrigin != "*")
            {
                response.Headers.Add("Access-Control-Allow-Credentials", "true");
            }

            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With, Accept");
            response.Headers.Add("Access-Control-Expose-Headers", "Content-Length, Content-Type");
            response.Headers.Add("Access-Control-Max-Age", "3600");
        }
    }
}
