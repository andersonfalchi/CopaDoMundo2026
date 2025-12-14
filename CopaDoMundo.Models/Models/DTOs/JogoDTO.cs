using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo.Models.Models.DTOs
{
    public class JogoDTO
    {
        public int Id { get; set; }
        public JogoEnum.PartidaEnum Fase { get; set; } 
        public DateTime DataHora { get; set; }
        public string Estadio { get; set; } = string.Empty;
        public string SelecaoA { get; set; } = string.Empty;
        public string BandeiraSelecaoA { get; set; } = string.Empty;
        public string SelecaoB { get; set; } = string.Empty;
        public string BandeiraSelecaoB { get; set; } = string.Empty;
        public int? QtdGolsSelecaoA { get; set; }
        public int? QtdGolsSelecaoB { get; set; }
    }
}
