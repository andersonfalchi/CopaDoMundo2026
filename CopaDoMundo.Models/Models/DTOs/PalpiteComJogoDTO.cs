using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo.Models.Models.DTOs
{
    public class PalpiteComJogoDTO
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public int QtdGolsSelecaoA { get; set; }
        public int QtdGolsSelecaoB { get; set; }
        public JogoDTO Jogo { get; set; } = null!;
    }
}
