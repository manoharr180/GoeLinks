using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace GeoLinks.API.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].ToString();

            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = System.Guid.NewGuid().ToString();
            }

            context.Items[CorrelationIdHeaderName] = correlationId;
            context.Response.Headers[CorrelationIdHeaderName] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            using (LogContext.PushProperty("RequestPath", context.Request.Path))
            using (LogContext.PushProperty("RequestMethod", context.Request.Method))
            {
                await _next(context);
            }
        }
    }
}
