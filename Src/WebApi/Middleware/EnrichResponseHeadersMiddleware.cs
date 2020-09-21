using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SafeStak.Deltas.WebApi.Middleware
{
    public class EnrichResponseHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public EnrichResponseHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            if (context.Request.Headers.TryGetValue("Operation-ID", out var operationId) &&
                !string.IsNullOrWhiteSpace(operationId))
            {
                context.TraceIdentifier = operationId;
            }
            else
            {
                context.TraceIdentifier = Guid.NewGuid().ToString();
            }

            context.Response.OnStarting(() => {
                stopwatch.Stop();
                context.Response.Headers.Add("Server-Timing", $"ttpm;dur={stopwatch.ElapsedMilliseconds}");
                context.Response.Headers.Add("Operation-ID", context.TraceIdentifier);
                return Task.CompletedTask;
            });

            return _next(context);
        }
    }
}