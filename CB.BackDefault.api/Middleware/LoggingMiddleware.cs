using System.Diagnostics;

namespace CB.BackDefault.Api.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Items[CorrelationIdMiddleware.ItemKey]?.ToString();
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "Request {CorrelationId}: {Method} {Path}",
            correlationId,
            context.Request.Method,
            context.Request.Path);

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation(
            "Response {CorrelationId}: {StatusCode} em {Elapsed}ms",
            correlationId,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}