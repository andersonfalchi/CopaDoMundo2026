using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected BaseException(string message, int statusCode, string errorCode, Exception inner = null)
            : base(message, inner)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
