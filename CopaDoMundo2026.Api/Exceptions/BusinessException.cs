using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message, string errorCode = "BUSINESS_ERROR")
            : base(message, 400, errorCode)
        {
        }
    }
}
