using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo.Models.Models
{
    public static class JogoEnum
    {
        public enum ResultadoEnum : sbyte
        {
            Empate = 0,
            VitoriaTimeA = 1,
            VitoriaTimeB = 2,
        }

        public enum PartidaEnum : sbyte
        {
            FaseDeGrupos = 0,
            OitavasDeFinal = 1,
            QuartasDeFinal = 2,
            SemiFinal = 3,
            Final = 4,
            Completo = 5
        }
    }
}
