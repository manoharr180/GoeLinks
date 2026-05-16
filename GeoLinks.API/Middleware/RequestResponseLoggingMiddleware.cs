using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GeoLinks.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            var requestBody = await GetRequestBodyAsync(context.Request);
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                var responseContent = await GetResponseBodyAsync(context.Response);
                var duration = DateTime.UtcNow - startTime;

                _logger.LogInformation(
                    "HTTP {Method} {Path} responded with {StatusCode} in {DurationMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    duration.TotalMilliseconds
                );

                if (context.Request.Method != "GET" && !string.IsNullOrEmpty(requestBody))
                {
                    _logger.LogDebug("Request body: {RequestBody}", requestBody);
                }

                if (context.Response.StatusCode >= 400 && !string.IsNullOrEmpty(responseContent))
                {
                    _logger.LogWarning("Response body: {ResponseBody}", responseContent);
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private static async Task<string> GetRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, false, 1024, true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body.Length > 5000 ? body.Substring(0, 5000) + "..." : body;
            }
        }

        private static async Task<string> GetResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(response.Body, Encoding.UTF8, false, 1024, true))
            {
                var body = await reader.ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                return body.Length > 5000 ? body.Substring(0, 5000) + "..." : body;
            }
        }
    }
}
