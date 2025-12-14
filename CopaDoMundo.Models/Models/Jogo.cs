using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo.Models.Models
{
    public class Jogo
    {
        public int Id { get; set; }

        public DateTime DataHora { get; set; }

        public string Estadio { get; set; } = string.Empty;

        public string SelecaoA { get; set; } = string.Empty;

        public string BandeiraSelecaoA { get; set; } = string.Empty;

        public string SelecaoB { get; set; } = string.Empty;

        public string BandeiraSelecaoB { get; set; } = string.Empty;

        public string Rodada { get; set; } = string.Empty;

        public string Fase { get; set; } = string.Empty; 

        public int? QtdGolsSelecaoA { get; set; }

        public int? QtdGolsSelecaoB { get; set; }
    }
}
