using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Middleware
{
    public class CorsMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var requestData = await context.GetHttpRequestDataAsync();

            if (requestData != null)
            {
                // Tratar requisição OPTIONS (preflight)
                if (requestData.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
                {
                    var preflightResponse = requestData.CreateResponse(HttpStatusCode.OK);
                    AddCorsHeaders(preflightResponse);

                    context.GetInvocationResult().Value = preflightResponse;
                    return;
                }
            }

            // Continuar para a próxima função
            await next(context);

            // Adicionar headers CORS na resposta
            var response = context.GetHttpResponseData();
            if (response != null)
            {
                AddCorsHeaders(response);
            }
        }

        private void AddCorsHeaders(HttpResponseData response)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Credentials", "false");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With");
        }
    }
}
