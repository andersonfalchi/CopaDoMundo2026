using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Exceptions
{
    public class DatabaseException : BaseException
    {
        public DatabaseException(string message)
            : base(message, 500, "DATABASE_ERROR")
        {
        }
    }
}
