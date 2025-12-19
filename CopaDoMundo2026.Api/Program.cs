using CopaDoMundo2026.Api.Middlewares;
using CopaDoMundo2026.Api.Services;
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
                .ConfigureFunctionsWebApplication(builder =>
                {
                    builder.UseMiddleware<CorsMiddleware>();
                })
                .ConfigureServices(services =>
                {
                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();

                    var connectionString = Environment.GetEnvironmentVariable("DatabaseConnection");
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));      

                    services.AddScoped<AutenticacaoService>();
                    services.AddScoped<PalpiteService>();
                    services.AddScoped<JogoService>();

                    var jwtSecret = Environment.GetEnvironmentVariable("JwtSecret")
                        ?? throw new InvalidOperationException("JwtSecret não configurada");

                    var jwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? "CopaApp";

                    services.AddSingleton(new JwtService(jwtSecret, jwtIssuer));


                })
                .Build();

           host.Run();
        }
    }
}
