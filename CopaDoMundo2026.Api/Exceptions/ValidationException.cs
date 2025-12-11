using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Exceptions
{
    public class ValidationException : BaseException
    {
        public Dictionary<string, List<string>> Errors { get; }

        public ValidationException(Dictionary<string, List<string>> errors)
            : base("Erro de validação", 422, "VALIDATION_ERROR")
        {
            Errors = errors;
        }

        public ValidationException(string field, string message)
            : base("Erro de validação", 422, "VALIDATION_ERROR")
        {
            Errors = new Dictionary<string, List<string>>
            {
                { field, new List<string> { message } }
            };
        }
    }
}
