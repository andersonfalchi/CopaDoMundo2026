using CopaDoMundo.Models.Models;
using CopaMundo2026.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Services
{
    public class JogoService
    {
        private readonly AppDbContext _context;

        public JogoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Jogo>> ObterTodosJogosAsync()
        {
            return await _context.Jogos
                .OrderBy(j => j.DataHora)
                .ToListAsync();
        }

        public async Task<Jogo?> ObterJogoPorIdAsync(int id)
        {
            return await _context.Jogos.FindAsync(id);
        }

        public async Task<List<Jogo>> ObterJogosPorRodadaAsync(string rodada)
        {
            return await _context.Jogos
                .Where(j => j.Rodada == rodada)
                .OrderBy(j => j.DataHora)
                .ToListAsync();
        }

        public async Task<List<Jogo>> ObterJogosPorGrupoAsync(string grupo)
        {
            return await _context.Jogos
                .Where(j => j.Grupo == grupo)
                .OrderBy(j => j.DataHora)
                .ToListAsync();
        }

        public async Task<List<Jogo>> ObterJogosPorFaseAsync(int fase)
        {
            return await _context.Jogos
                .Where(j => (int)j.Fase == fase)
                .OrderBy(j => j.DataHora)
                .ToListAsync();
        }

        public async Task<List<Jogo>> ObterProximosJogosAsync(int quantidade = 10)
        {
            var agora = DateTime.UtcNow;
            return await _context.Jogos
                .Where(j => j.DataHora > agora && j.QtdGolsSelecaoA == null && j.QtdGolsSelecaoB == null)
                .OrderBy(j => j.DataHora)
                .Take(quantidade)
                .ToListAsync();
        }

        public async Task<List<Jogo>> ObterJogosFinalizadosAsync()
        {
            return await _context.Jogos
                .Where(j => j.QtdGolsSelecaoA != null && j.QtdGolsSelecaoB != null)
                .OrderByDescending(j => j.DataHora)
                .ToListAsync();
        }

        public async Task<(bool sucesso, string mensagem, Jogo? jogo)> AtualizarResultadoAsync(int id, int golsA, int golsB)
        {
            var jogo = await _context.Jogos.FindAsync(id);

            if (jogo == null)
            {
                return (false, $"Jogo com ID {id} não encontrado", null);
            }

            if (golsA < 0 || golsB < 0)
            {
                return (false, "Quantidade de gols não pode ser negativa", null);
            }

            jogo.QtdGolsSelecaoA = golsA;
            jogo.QtdGolsSelecaoB = golsB;

            await _context.SaveChangesAsync();

            return (true, $"Resultado atualizado: {jogo.SelecaoA} {golsA} x {golsB} {jogo.SelecaoB}", jogo);
        }

        public async Task<(bool sucesso, string mensagem, Jogo? jogo)> CriarJogoAsync(Jogo novoJogo)
        {
            if (string.IsNullOrWhiteSpace(novoJogo.SelecaoA) || string.IsNullOrWhiteSpace(novoJogo.SelecaoB))
            {
                return (false, "Seleções não podem estar vazias", null);
            }

            if (novoJogo.DataHora < DateTime.UtcNow)
            {
                return (false, "Data do jogo não pode ser no passado", null);
            }

            _context.Jogos.Add(novoJogo);
            await _context.SaveChangesAsync();

            return (true, "Jogo criado com sucesso", novoJogo);
        }

        public async Task<(bool sucesso, string mensagem)> DeletarJogoAsync(int id)
        {
            var jogo = await _context.Jogos.FindAsync(id);

            if (jogo == null)
            {
                return (false, $"Jogo com ID {id} não encontrado");
            }

            // Verificar se existem palpites para este jogo
            var existemPalpites = await _context.Palpites.AnyAsync(p => p.JogoId == id);

            if (existemPalpites)
            {
                return (false, "Não é possível deletar jogo que possui palpites");
            }

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();

            return (true, "Jogo deletado com sucesso");
        }

        public async Task<bool> JogoJaComeçouAsync(int jogoId)
        {
            var jogo = await _context.Jogos.FindAsync(jogoId);

            if (jogo == null)
                return true; // Se não encontrou, considera como começado para bloquear

            return jogo.DataHora <= DateTime.UtcNow;
        }

        public async Task<bool> JogoFinalizadoAsync(int jogoId)
        {
            var jogo = await _context.Jogos.FindAsync(jogoId);

            if (jogo == null)
                return false;

            return jogo.QtdGolsSelecaoA != null && jogo.QtdGolsSelecaoB != null;
        }
    }
}