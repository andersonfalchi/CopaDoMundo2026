using CopaMundo2026.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CopaDoMundo2026.Services;

public class AuthApiClient
{
    private readonly HttpClient _http;
    public AuthApiClient(HttpClient http) { _http = http; }

    public async Task<(bool sucesso, string mensagem, UsuarioDto? usuario)> LoginAsync(LoginDTO login)
    {
        var resp = await _http.PostAsJsonAsync("api/login", login);
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadFromJsonAsync<ApiResponse>();
            return (false, err?.mensagem ?? "Erro", null);
        }
        var json = await resp.Content.ReadFromJsonAsync<LoginResp>();
        return (json!.sucesso, json!.mensagem, json!.usuario);
    }

    public async Task<(bool sucesso, string mensagem)> RegisterAsync(RegistroDTO registro)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync("api/registrar", registro);

            var raw = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrWhiteSpace(raw))
                {
                    try
                    {
                        var err = JsonSerializer.Deserialize<ApiResponse>(raw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        return (err?.sucesso ?? false, err?.mensagem ?? $"Erro: {(int)resp.StatusCode}");
                    }
                    catch
                    {
                        return (false, $"Erro: {(int)resp.StatusCode} - {raw}");
                    }
                }

                return (false, $"Erro: {(int)resp.StatusCode} - resposta vazia");
            }

            if (string.IsNullOrWhiteSpace(raw))
            {
                return (true, "Operação realizada com sucesso (sem mensagem do servidor).");
            }

            // parse normal
            var obj = JsonSerializer.Deserialize<ApiResponse>(raw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return (obj?.sucesso ?? true, obj?.mensagem ?? "Operação realizada com sucesso.");
        }
        catch (Exception ex)
        {
            return (false, $"Falha na requisição: {ex.Message}");
        }
    }

    record ApiResponse(bool sucesso, string mensagem);
    record LoginResp(bool sucesso, string mensagem, UsuarioDto? usuario);
}

public record UsuarioDto(int Id, string NomeUsuario, string? Email);
