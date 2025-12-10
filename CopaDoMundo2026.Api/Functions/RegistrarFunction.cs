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
    public class RegistrarFunction
    {
        private readonly AutenticacaoService _auth;

        public RegistrarFunction(AutenticacaoService auth)
        {
            _auth = auth;
        }

        [Function("Registrar")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registrar")] HttpRequestData req)
        {
            var registro = await req.ReadFromJsonAsync<RegistroDTO>();
            if (registro == null)
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("Dados inválidos");
                return bad;
            }

            var (sucesso, mensagem) = await _auth.RegistrarAsync(registro);

            var response = req.CreateResponse(sucesso ? HttpStatusCode.Created : HttpStatusCode.BadRequest);
            await response.WriteAsJsonAsync(new { sucesso, mensagem });
            return response;
        }
    }
}
