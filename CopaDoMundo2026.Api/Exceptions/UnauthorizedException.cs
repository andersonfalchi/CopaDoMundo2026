using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = "Não autorizado")
            : base(message, 401, "UNAUTHORIZED")
        {
        }
    }
}
