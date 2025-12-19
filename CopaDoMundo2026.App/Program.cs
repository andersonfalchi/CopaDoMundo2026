using CopaDoMundo2026;
using CopaDoMundo2026.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiUrl/"] ?? "https://copamundoapi-fvf0e2cahxbtbwb3.brazilsouth-01.azurewebsites.net/")
    //BaseAddress = new Uri(builder.Configuration["ApiUrl/"] ?? "http://localhost:7071/")
});

builder.Services.AddScoped<AuthApiClient>();
builder.Services.AddScoped<JogosApiClient>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
