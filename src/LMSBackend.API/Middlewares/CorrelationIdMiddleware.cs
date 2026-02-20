using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.API.Middlewares
{
    public class CorrelationIdMiddleware
    {
        public const string HeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next;
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers.TryGetValue(HeaderName, out var value)
                ? value.ToString()
                : Guid.NewGuid().ToString("N");

            context.Items[HeaderName] = correlationId;
            context.Response.Headers[HeaderName] = correlationId;

            using (context.RequestServices
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("Correlation")
                .BeginScope("{CorrelationId}", correlationId))
            {
                await _next(context);
            }
        }

    }
}