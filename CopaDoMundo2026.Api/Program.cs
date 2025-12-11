using CopaDoMundo2026.Api.Middleware;
using CopaMundo2026.Context;
using CopaMundo2026.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CopaDoMundo2026.Api
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication(app =>
                {
                    // ✅ Registrar o middleware CORS
                    app.UseMiddleware<CorsMiddleware>();
                })
                .ConfigureServices(services =>
                {
                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();

                    var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));

                    services.AddScoped<AutenticacaoService>();
                })
                .Build();

            host.Run();
        }
    }
}
