using CB.BackDefault.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace CB.BackDefault.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Items[CorrelationIdMiddleware.ItemKey]?.ToString();
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "[{CorrelationId}] Erro de domínio");

            await WriteResponse(context, HttpStatusCode.BadRequest, ex.Message, correlationId);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "[{CorrelationId}] Acesso não autorizado");

            await WriteResponse(context, HttpStatusCode.Unauthorized, "Não autorizado", correlationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[{CorrelationId}] Erro interno");

            await WriteResponse(context, HttpStatusCode.InternalServerError, "Erro interno do servidor", correlationId);
        }
    }

    private static async Task WriteResponse(HttpContext context,
                                            HttpStatusCode statusCode,
                                            string message,
                                            string? correlationId)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            success = false,
            error = message,
            correlationId = correlationId

        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}