using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string resource, object id)
            : base($"{resource} com ID '{id}' não foi encontrado.", 404, "NOT_FOUND")
        {
        }

        public NotFoundException(string message)
            : base(message, 404, "NOT_FOUND")
        {
        }
    }
}
