using CB.BackDefault.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
            await HandleDomainException(context, correlationId, ex);
        }
        catch (Exception ex)
        {
            await HandleUnknownException(context, correlationId, ex);
        }
    }

    private async Task HandleDomainException(HttpContext context, string? correlationId, DomainException ex)
    {
        _logger.LogWarning(ex, "Domain error");

        var problem = new ProblemDetails
        {
            Title = "{{correlationId}} Erro de domínio",
            Detail = ex.Message,
            Status = ex.StatusCode,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = ex.StatusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }

    private async Task HandleUnknownException(HttpContext context, string? correlationId, Exception ex)
    {
        _logger.LogError(ex, "Erro inesperado");

        var problem = new ProblemDetails
        {
            Title = "{{correlationId}} Erro interno do servidor",
            Detail = "Ocorreu um erro inesperado.",
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }

}