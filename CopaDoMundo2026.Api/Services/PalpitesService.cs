using CopaDoMundo.Models.Models;
using CopaDoMundo.Models.Models.DTOs;
using CopaMundo2026.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Services
{
    public class PalpitesService
    {
        private readonly AppDbContext _db;

        public PalpitesService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<(bool, string, Palpite?)> CriarPalpiteAsync(
            int usuarioId,
            CriarPalpiteDTO dto)
        {
            // Validar se o jogo existe
            var jogo = await _db.Jogos.FindAsync(dto.JogoId);
            if (jogo == null)
                return (false, "Jogo não encontrado", null);

            // Validar se o jogo já começou
            if (jogo.DataHora <= DateTime.UtcNow)
                return (false, "Não é possível criar palpite para jogo que já começou", null);

            // Verificar se já existe palpite
            var palpiteExistente = await _db.Palpites
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.JogoId == dto.JogoId);

            if (palpiteExistente != null)
                return (false, "Você já tem um palpite para este jogo", null);

            var palpite = new Palpite
            {
                UsuarioId = usuarioId,
                JogoId = dto.JogoId,
                QtdGolsSelecaoA = dto.QtdGolsSelecaoA,
                QtdGolsSelecaoB = dto.QtdGolsSelecaoB
            };

            _db.Palpites.Add(palpite);
            await _db.SaveChangesAsync();

            return (true, "Palpite criado com sucesso", palpite);
        }

        public async Task<List<PalpiteComJogoDTO>> ObterPalpitesUsuarioAsync(int usuarioId)
        {
            return await _db.Palpites
                .Include(p => p.Jogo)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderBy(p => p.Jogo.DataHora)
                .Select(p => new PalpiteComJogoDTO
                {
                    Id = p.Id,
                    JogoId = p.JogoId,
                    QtdGolsSelecaoA = p.QtdGolsSelecaoA,
                    QtdGolsSelecaoB = p.QtdGolsSelecaoB,
                    Jogo = new JogoDTO
                    {
                        Id = p.Jogo.Id,
                        DataHora = p.Jogo.DataHora,
                        Estadio = p.Jogo.Estadio,
                        SelecaoA = p.Jogo.SelecaoA,
                        BandeiraSelecaoA = p.Jogo.BandeiraSelecaoA,
                        SelecaoB = p.Jogo.SelecaoB,
                        BandeiraSelecaoB = p.Jogo.BandeiraSelecaoB,
                        Fase = p.Jogo.Fase,
                        QtdGolsSelecaoA = p.Jogo.QtdGolsSelecaoA,
                        QtdGolsSelecaoB = p.Jogo.QtdGolsSelecaoB
                    }
                })
                .ToListAsync();
        }

        public async Task<(bool, string)> AtualizarPalpiteAsync(
            int palpiteId,
            int usuarioId,
            AtualizarPalpiteDTO dto)
        {
            var palpite = await _db.Palpites
                .Include(p => p.Jogo)
                .FirstOrDefaultAsync(p => p.Id == palpiteId);

            if (palpite == null)
                return (false, "Palpite não encontrado");

            if (palpite.UsuarioId != usuarioId)
                return (false, "Você não tem permissão para editar este palpite");

            if (palpite.Jogo.DataHora <= DateTime.UtcNow)
                return (false, "Não é possível editar palpite de jogo que já começou");

            palpite.QtdGolsSelecaoA = dto.QtdGolsSelecaoA;
            palpite.QtdGolsSelecaoB = dto.QtdGolsSelecaoB;

            await _db.SaveChangesAsync();

            return (true, "Palpite atualizado com sucesso");
        }
    }
}
