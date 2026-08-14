using System.Diagnostics;


namespace SmartStore.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private RequestDelegate next;
        private ILogger<RequestLoggingMiddleware> logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            logger.LogInformation(
                "[REQUEST] {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await next(context);

            stopwatch.Stop();

            logger.LogInformation(
                "[RESPONSE] {StatusCode} - {ElapsedMilliseconds} ms",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
