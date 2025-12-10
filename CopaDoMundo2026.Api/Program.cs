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
                .ConfigureFunctionsWebApplication()
                .ConfigureServices(services =>
                {
                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();

                    var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString")
                        ?? "Server=localhost;Database=MeuProjetoDB;User Id=sa;Password=123;TrustServerCertificate=True;";

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    services.AddScoped<AutenticacaoService>();
                })
                .Build();

            host.Run();
        }
    }
}
