using CopaDoMundo2026.Api.Exceptions;
using CopaDoMundo2026.Api.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopaDoMundo2026.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(FunctionContext context, Exception exception)
        {
            var (statusCode, response) = exception switch
            {
                ValidationException validationEx => (
                    HttpStatusCode.UnprocessableEntity,
                    ApiResponse<object>.ErrorResponse(
                        validationEx.Message,
                        validationEx.ErrorCode,
                        validationEx.Errors
                    )
                ),
                NotFoundException notFoundEx => (
                    HttpStatusCode.NotFound,
                    ApiResponse<object>.ErrorResponse(
                        notFoundEx.Message,
                        notFoundEx.ErrorCode
                    )
                ),
                BusinessException businessEx => (
                    HttpStatusCode.BadRequest,
                    ApiResponse<object>.ErrorResponse(
                        businessEx.Message,
                        businessEx.ErrorCode
                    )
                ),
                UnauthorizedException unauthorizedEx => (
                    HttpStatusCode.Unauthorized,
                    ApiResponse<object>.ErrorResponse(
                        unauthorizedEx.Message,
                        unauthorizedEx.ErrorCode
                    )
                ),
                DatabaseException dbEx => (
                    HttpStatusCode.InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "Erro ao acessar o banco de dados",
                        dbEx.ErrorCode
                    )
                ),
                _ => (
                    HttpStatusCode.InternalServerError,
                    ApiResponse<object>.ErrorResponse(
                        "Erro interno do servidor",
                        "INTERNAL_ERROR"
                    )
                )
            };

            // Log detalhado do erro
            LogException(exception, context);

            // Criar response HTTP
            var httpContext = await context.GetHttpRequestDataAsync();
            if (httpContext != null)
            {
                var httpResponse = httpContext.CreateResponse(statusCode);
                await httpResponse.WriteAsJsonAsync(response);

                var invocationResult = context.GetInvocationResult();
                invocationResult.Value = httpResponse;
            }
        }

        private void LogException(Exception exception, FunctionContext context)
        {
            var functionName = context.FunctionDefinition.Name;

            if (exception is BaseException baseEx)
            {
                _logger.LogWarning(
                    exception,
                    "[{FunctionName}] {ExceptionType}: {Message} | ErrorCode: {ErrorCode}",
                    functionName,
                    exception.GetType().Name,
                    exception.Message,
                    baseEx.ErrorCode
                );
            }
            else
            {
                _logger.LogError(
                    exception,
                    "[{FunctionName}] Erro não tratado: {Message}",
                    functionName,
                    exception.Message
                );
            }
        }
    }
}
