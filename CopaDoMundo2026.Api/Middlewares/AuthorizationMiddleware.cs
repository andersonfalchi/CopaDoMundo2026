using CopaDoMundo2026.Api.Services;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Middlewares
{
    public static class AuthorizationMiddleware
    {
        public static async Task<(bool, string?, int?)> ValidarTokenAsync(
            HttpRequestData req,
            JwtService jwtService)
        {
            if (!req.Headers.TryGetValues("Authorization", out var authHeaders))
                return (false, "Token não fornecido", null);

            var token = authHeaders.FirstOrDefault()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
                return (false, "Token inválido", null);

            var principal = jwtService.ValidarToken(token);
            if (principal == null)
                return (false, "Token inválido ou expirado", null);

            var usuarioId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return (true, null, usuarioId);
        }
    }
}
