using CopaMundo2026.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo.Models.Models
{
    public class Palpite
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public int JogoId { get; set; }

        public virtual Jogo Jogo { get; set; }

        public int QtdGolsSelecaoA { get; set; }

        public int QtdGolsSelecaoB { get; set; }

        [NotMapped]
        public int PontosVencedor { get; set; }

        [NotMapped]
        public int PontosPlacar { get; set; }

        [NotMapped]
        public int PontuacaoTotal => PontosVencedor + PontosPlacar; 
    }
}
