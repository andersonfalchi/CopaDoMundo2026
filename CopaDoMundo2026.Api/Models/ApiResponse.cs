using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ErrorDetails Error { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, string errorCode,
            Dictionary<string, List<string>> validationErrors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Error = new ErrorDetails
                {
                    Message = message,
                    ErrorCode = errorCode,
                    ValidationErrors = validationErrors
                }
            };
        }
    }

    public class ErrorDetails
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public Dictionary<string, List<string>> ValidationErrors { get; set; }
    }
}
