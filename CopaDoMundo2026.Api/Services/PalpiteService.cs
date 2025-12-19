using CopaDoMundo.Models.Models;
using CopaDoMundo.Models.Models.DTOs;
using CopaDoMundo2026.Api.Functions;
using CopaMundo2026.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Services
{
    public class PalpiteService
    {
        private readonly AppDbContext _context;
        private readonly JogoService _jogoService;

        public PalpiteService(AppDbContext context, JogoService jogoService)
        {
            _context = context;
            _jogoService = jogoService;
        }

        public async Task<List<Palpite>> ObterPalpitesUsuarioAsync(int usuarioId)
        {
            return await _context.Palpites
                .Include(p => p.Jogo)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderBy(p => p.Jogo.DataHora)
                .ToListAsync();
        }

        public async Task<List<Palpite>> ObterPalpitesPorJogoAsync(int jogoId)
        {
            return await _context.Palpites
                .Include(p => p.Usuario)
                .Where(p => p.JogoId == jogoId)
                .ToListAsync();
        }

        public async Task<Palpite?> ObterPalpitePorIdAsync(int id)
        {
            return await _context.Palpites
                .Include(p => p.Jogo)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<(bool sucesso, string mensagem, Palpite? palpite)> CriarPalpiteAsync(PalpiteDto palpiteDto)
        {
            // Verificar se o jogo existe
            var jogo = await _context.Jogos.FindAsync(palpiteDto.JogoId);
            if (jogo == null)
            {
                return (false, $"Jogo com ID {palpiteDto.JogoId} não encontrado", null);
            }

            // Verificar se o jogo já começou
            if (await _jogoService.JogoJaComeçouAsync(palpiteDto.JogoId))
            {
                return (false, "Não é possível fazer palpite para jogos que já começaram", null);
            }

            // Verificar se já existe palpite
            var palpiteExistente = await _context.Palpites
                .FirstOrDefaultAsync(p => p.UsuarioId == palpiteDto.UsuarioId && p.JogoId == palpiteDto.JogoId);

            if (palpiteExistente != null)
            {
                return (false, "Já existe um palpite para este jogo. Use a rota de atualização.", null);
            }

            // Validar valores
            if (palpiteDto.QtdGolsSelecaoA < 0 || palpiteDto.QtdGolsSelecaoB < 0)
            {
                return (false, "Quantidade de gols não pode ser negativa", null);
            }

            var novoPalpite = new Palpite
            {
                UsuarioId = palpiteDto.UsuarioId,
                JogoId = palpiteDto.JogoId,
                QtdGolsSelecaoA = palpiteDto.QtdGolsSelecaoA,
                QtdGolsSelecaoB = palpiteDto.QtdGolsSelecaoB
            };

            _context.Palpites.Add(novoPalpite);
            await _context.SaveChangesAsync();

            // Recarregar com includes
            novoPalpite = await ObterPalpitePorIdAsync(novoPalpite.Id);

            return (true, "Palpite criado com sucesso", novoPalpite);
        }

        //public async Task<object> SalvarPalpitesLoteAsync(List<PalpiteDto> palpites)
        //{
        //    var resultados = new List<ResultadoPalpiteDto>();

        //    foreach (var palpiteDto in palpites)
        //    {
        //        try
        //        {
        //            // Verificar se o jogo existe
        //            var jogo = await _context.Jogos.FindAsync(palpiteDto.JogoId);
        //            if (jogo == null)
        //            {
        //                resultados.Add(new ResultadoPalpiteDto
        //                {
        //                    JogoId = palpiteDto.JogoId,
        //                    Sucesso = false,
        //                    Mensagem = $"Jogo com ID {palpiteDto.JogoId} não encontrado"
        //                });
        //                continue;
        //            }

        //            // Verificar se o jogo já começou
        //            if (await _jogoService.JogoJaComeçouAsync(palpiteDto.JogoId))
        //            {
        //                resultados.Add(new ResultadoPalpiteDto
        //                {
        //                    JogoId = palpiteDto.JogoId,
        //                    Sucesso = false,
        //                    Mensagem = "Jogo já começou"
        //                });
        //                continue;
        //            }

        //            // Validar valores
        //            if (palpiteDto.QtdGolsSelecaoA < 0 || palpiteDto.QtdGolsSelecaoB < 0)
        //            {
        //                resultados.Add(new ResultadoPalpiteDto
        //                {
        //                    JogoId = palpiteDto.JogoId,
        //                    Sucesso = false,
        //                    Mensagem = "Quantidade de gols não pode ser negativa"
        //                });
        //                continue;
        //            }

        //            // Verificar se já existe palpite
        //            var palpiteExistente = await _context.Palpites
        //                .FirstOrDefaultAsync(p => p.UsuarioId == palpiteDto.UsuarioId && p.JogoId == palpiteDto.JogoId);

        //            if (palpiteExistente != null)
        //            {
        //                // Atualizar palpite existente
        //                palpiteExistente.QtdGolsSelecaoA = palpiteDto.QtdGolsSelecaoA;
        //                palpiteExistente.QtdGolsSelecaoB = palpiteDto.QtdGolsSelecaoB;

        //                resultados.Add(new ResultadoPalpiteDto
        //                {
        //                    JogoId = palpiteDto.JogoId,
        //                    PalpiteId = palpiteExistente.Id,
        //                    Sucesso = true,
        //                    Mensagem = "Palpite atualizado"
        //                });
        //            }
        //            else
        //            {
        //                // Criar novo palpite
        //                var novoPalpite = new Palpite
        //                {
        //                    UsuarioId = palpiteDto.UsuarioId,
        //                    JogoId = palpiteDto.JogoId,
        //                    QtdGolsSelecaoA = palpiteDto.QtdGolsSelecaoA,
        //                    QtdGolsSelecaoB = palpiteDto.QtdGolsSelecaoB
        //                };

        //                _context.Palpites.Add(novoPalpite);
        //                await _context.SaveChangesAsync();

        //                resultados.Add(new ResultadoPalpiteDto
        //                {
        //                    JogoId = palpiteDto.JogoId,
        //                    PalpiteId = novoPalpite.Id,
        //                    Sucesso = true,
        //                    Mensagem = "Palpite criado"
        //                });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            resultados.Add(new ResultadoPalpiteDto
        //            {
        //                JogoId = palpiteDto.JogoId,
        //                Sucesso = false,
        //                Mensagem = $"Erro: {ex.Message}"
        //            });
        //        }
        //    }

        //await _context.SaveChangesAsync();

        //return new
        //{
        //    Total = resultados.Count,
        //    Sucesso = resultados.Count(r => r.Sucesso),
        //    Falhas = resultados.Count(r => !r.Sucesso),
        //    Resultados = resultados
        //};
        //}

        public async Task<(bool sucesso, string mensagem, Palpite? palpite)> AtualizarPalpiteAsync(int id, PalpiteDto palpiteDto)
        {
            var palpite = await _context.Palpites
                .Include(p => p.Jogo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (palpite == null)
            {
                return (false, $"Palpite com ID {id} não encontrado", null);
            }

            // Verificar se o jogo já começou
            if (await _jogoService.JogoJaComeçouAsync(palpite.JogoId))
            {
                return (false, "Não é possível alterar palpite de jogos que já começaram", null);
            }

            // Validar valores
            if (palpiteDto.QtdGolsSelecaoA < 0 || palpiteDto.QtdGolsSelecaoB < 0)
            {
                return (false, "Quantidade de gols não pode ser negativa", null);
            }

            palpite.QtdGolsSelecaoA = palpiteDto.QtdGolsSelecaoA;
            palpite.QtdGolsSelecaoB = palpiteDto.QtdGolsSelecaoB;

            await _context.SaveChangesAsync();

            // Recarregar com includes
            palpite = await ObterPalpitePorIdAsync(palpite.Id);

            return (true, "Palpite atualizado com sucesso", palpite);
        }

        public async Task<(bool sucesso, string mensagem)> DeletarPalpiteAsync(int id)
        {
            var palpite = await _context.Palpites
                .Include(p => p.Jogo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (palpite == null)
            {
                return (false, $"Palpite com ID {id} não encontrado");
            }

            // Verificar se o jogo já começou
            if (await _jogoService.JogoJaComeçouAsync(palpite.JogoId))
            {
                return (false, "Não é possível deletar palpite de jogos que já começaram");
            }

            _context.Palpites.Remove(palpite);
            await _context.SaveChangesAsync();

            return (true, "Palpite deletado com sucesso");
        }

        //public async Task<List<RankingDto>> ObterRankingAsync()
        //{
        //    // Buscar todos os palpites de jogos finalizados
        //    var palpites = await _context.Palpites
        //        .Include(p => p.Jogo)
        //        .Include(p => p.Usuario)
        //        .Where(p => p.Jogo.QtdGolsSelecaoA != null && p.Jogo.QtdGolsSelecaoB != null)
        //        .ToListAsync();

        //    // Calcular pontuação por usuário
        //    var ranking = palpites
        //        .GroupBy(p => new { p.UsuarioId, p.Usuario.NomeUsuario })
        //        .Select(g => new RankingDto
        //        {
        //            UsuarioId = g.Key.UsuarioId,
        //            NomeUsuario = g.Key.NomeUsuario,
        //            TotalPontos = g.Sum(p => CalcularPontuacao(p)),
        //            TotalPalpites = g.Count(),
        //            PalpitesExatos = g.Count(p => p.QtdGolsSelecaoA == p.Jogo.QtdGolsSelecaoA &&
        //                                          p.QtdGolsSelecaoB == p.Jogo.QtdGolsSelecaoB),
        //            Vencedores = g.Count(p => CalcularPontuacao(p) > 0)
        //        })
        //        .OrderByDescending(r => r.TotalPontos)
        //        .ThenByDescending(r => r.PalpitesExatos)
        //        .ToList();

        //    return ranking;
        //}

        private int CalcularPontuacao(Palpite palpite)
        {
            if (palpite.Jogo.QtdGolsSelecaoA == null || palpite.Jogo.QtdGolsSelecaoB == null)
                return 0;

            int pontos = 0;

            // Placar exato: 5 pontos
            if (palpite.QtdGolsSelecaoA == palpite.Jogo.QtdGolsSelecaoA &&
                palpite.QtdGolsSelecaoB == palpite.Jogo.QtdGolsSelecaoB)
            {
                pontos += 5;
            }
            else
            {
                // Acertou o vencedor ou empate: 3 pontos
                var vencedorPalpite = palpite.QtdGolsSelecaoA > palpite.QtdGolsSelecaoB ? "A" :
                                     palpite.QtdGolsSelecaoB > palpite.QtdGolsSelecaoA ? "B" : "E";
                var vencedorReal = palpite.Jogo.QtdGolsSelecaoA > palpite.Jogo.QtdGolsSelecaoB ? "A" :
                                  palpite.Jogo.QtdGolsSelecaoB > palpite.Jogo.QtdGolsSelecaoA ? "B" : "E";

                if (vencedorPalpite == vencedorReal)
                {
                    pontos += 3;
                }
            }

            return pontos;
        }
    }

    public class PalpiteDto
    {
        public int UsuarioId { get; set; }
        public int JogoId { get; set; }
        public int QtdGolsSelecaoA { get; set; }
        public int QtdGolsSelecaoB { get; set; }
    }

    public class ResultadoPalpiteDto
    {
        public int JogoId { get; set; }
        public int? PalpiteId { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }

    public class RankingDto
    {
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public int TotalPontos { get; set; }
        public int TotalPalpites { get; set; }
        public int PalpitesExatos { get; set; }
        public int Vencedores { get; set; }
    }
}
