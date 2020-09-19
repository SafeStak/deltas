using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SafeStak.Deltas.WebApi.Exceptions
{
    public class GlobalExceptionHandlerMiddleware
    {
        public struct ErrorDetail
        {
            public ErrorDetail(string errorCode, string errorMessage)
            {
                Code = errorCode;
                Message = errorMessage;
            }
            public string Code { get; }
            public string Message { get; }
        }

        private const int DefaultEventId = 0;

        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context, ILogger<GlobalExceptionHandlerMiddleware> logger, IErrorResponseMapper errorResponseMapper)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogException(ex, logger);
                await WriteResponseAsync(context, ex, errorResponseMapper);
            }
        }

        private static async Task WriteResponseAsync(
            HttpContext context, Exception ex, IErrorResponseMapper errorResponseMapper)
        {
            var error = errorResponseMapper.MapFromException(ex);

            // TODO: factor in serialiser based on accept header
            var responseBody = JsonSerializer.Serialize(new[]{new ErrorDetail(error.ErrorCode, error.ErrorMessage)});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.HttpStatusCode;


            await context.Response.WriteAsync(responseBody);
        }

        private static void LogException(Exception ex, ILogger logger)
        {
            var loggableException = ex as ILoggableException;
            var eventId = loggableException?.EventId ?? DefaultEventId;
            //var customProperties = loggableException?.CustomProperties;

            logger.LogError(eventId, ex, ex.Message);
        }
    }
}
