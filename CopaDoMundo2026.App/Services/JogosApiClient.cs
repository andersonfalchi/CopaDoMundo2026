using System.Net.Http.Json;

namespace CopaDoMundo2026.Services
{
    public class JogosApiClient
    {
        private readonly HttpClient _http;

        public JogosApiClient(HttpClient http)
        {
            _http = http;
        }

        // ==================== JOGOS ====================

        public async Task<(bool sucesso, string mensagem, List<JogoDto>? jogos)> ObterJogosPorRodadaAsync(string rodada)
        {
            try
            {
                var jogos = await _http.GetFromJsonAsync<List<JogoDto>>($"api/jogos/rodada/{rodada}");
                return (true, "Jogos carregados", jogos);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao carregar jogos: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem, List<JogoDto>? jogos)> ObterTodosJogosAsync()
        {
            try
            {
                var jogos = await _http.GetFromJsonAsync<List<JogoDto>>("api/jogos");
                return (true, "Jogos carregados", jogos);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao carregar jogos: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem, List<JogoDto>? jogos)> ObterJogosPorGrupoAsync(string grupo)
        {
            try
            {
                var jogos = await _http.GetFromJsonAsync<List<JogoDto>>($"api/jogos/grupo/{grupo}");
                return (true, "Jogos carregados", jogos);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao carregar jogos: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem, List<JogoDto>? jogos)> ObterProximosJogosAsync()
        {
            try
            {
                var jogos = await _http.GetFromJsonAsync<List<JogoDto>>("api/jogos/proximos");
                return (true, "Jogos carregados", jogos);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao carregar jogos: {ex.Message}", null);
            }
        }

        // ==================== PALPITES ====================

        public async Task<(bool sucesso, string mensagem, List<PalpiteDto>? palpites)> ObterPalpitesUsuarioAsync(int usuarioId)
        {
            try
            {
                var palpites = await _http.GetFromJsonAsync<List<PalpiteDto>>($"api/palpites/usuario/{usuarioId}");
                return (true, "Palpites carregados", palpites);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao carregar palpites: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem, SalvarLoteResponse? resultado)> SalvarPalpitesLoteAsync(List<PalpiteLoteDto> palpites)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("api/palpites/salvar-lote", palpites);

                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadFromJsonAsync<ApiResponse>();
                    return (false, err?.mensagem ?? "Erro ao salvar palpites", null);
                }

                var resultado = await resp.Content.ReadFromJsonAsync<SalvarLoteResponse>();
                return (true, $"{resultado?.Sucesso ?? 0} palpite(s) salvos com sucesso!", resultado);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao salvar palpites: {ex.Message}", null);
            }
        }

        public async Task<(bool sucesso, string mensagem)> CriarPalpiteAsync(PalpiteLoteDto palpite)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("api/palpites", palpite);

                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadFromJsonAsync<ApiResponse>();
                    return (false, err?.mensagem ?? "Erro ao criar palpite");
                }

                var obj = await resp.Content.ReadFromJsonAsync<ApiResponse>();
                return (obj?.sucesso ?? true, obj?.mensagem ?? "Palpite criado com sucesso");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar palpite: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem)> AtualizarPalpiteAsync(int id, PalpiteLoteDto palpite)
        {
            try
            {
                var resp = await _http.PutAsJsonAsync($"api/palpites/{id}", palpite);

                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadFromJsonAsync<ApiResponse>();
                    return (false, err?.mensagem ?? "Erro ao atualizar palpite");
                }

                var obj = await resp.Content.ReadFromJsonAsync<ApiResponse>();
                return (obj?.sucesso ?? true, obj?.mensagem ?? "Palpite atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar palpite: {ex.Message}");
            }
        }

        public async Task<(bool sucesso, string mensagem, List<RankingDto>? ranking)> ObterRankingAsync()
        {
            try
            {
                var ranking = await _http.GetFromJsonAsync<List<RankingDto>>("api/palpites/ranking");
                return (true, "Ranking carregado", ranking);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao carregar ranking: {ex.Message}", null);
            }
        }

        record ApiResponse(bool sucesso, string mensagem);
    }

    public record JogoDto(
        int Id,
        string Rodada,
        string Grupo,
        string Estadio,
        DateTime DataHora,
        string SelecaoA,
        string BandeiraSelecaoA,
        string SelecaoB,
        string BandeiraSelecaoB,
        int? QtdGolsSelecaoA,
        int? QtdGolsSelecaoB
    );

    public record PalpiteDto(
        int Id,
        int UsuarioId,
        int JogoId,
        int QtdGolsSelecaoA,
        int QtdGolsSelecaoB,
        JogoDto? Jogo
    );

    public record PalpiteLoteDto(
        int UsuarioId,
        int JogoId,
        int QtdGolsSelecaoA,
        int QtdGolsSelecaoB
    );

    public record SalvarLoteResponse(
        int Total,
        int Sucesso,
        int Falhas,
        List<ResultadoPalpiteDto> Resultados
    );

    public record ResultadoPalpiteDto(
        int JogoId,
        int? PalpiteId,
        bool Sucesso,
        string Mensagem
    );

    public record RankingDto(
        int UsuarioId,
        string NomeUsuario,
        int TotalPontos,
        int TotalPalpites,
        int PalpitesExatos,
        int Vencedores
    );
}
