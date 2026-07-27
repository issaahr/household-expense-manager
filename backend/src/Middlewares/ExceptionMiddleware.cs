using System.Net;
using HouseholdExpenseManager.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpenseManager.Api.Middlewares;

/// <summary>
/// Middleware responsável por capturar exceções não tratadas durante o
/// processamento da requisição e retornar uma resposta padronizada.
/// </summary>
public sealed class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger,
    IHostEnvironment environment)
{
    /// <summary>
    /// Executa o próximo middleware da pipeline e trata exceções não capturadas.
    /// </summary>
    /// <param name="context">Contexto HTTP da requisição.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException exception)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                "Recurso não encontrado.",
                exception.Message
            );
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ocorreu uma exceção não tratada durante o processamento da requisição."
            );

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Erro interno do servidor.",
                environment.IsDevelopment()
                    ? exception.Message
                    : null
            );
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string title,
        string? detail)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        ProblemDetails problem = new()
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        await context.Response.WriteAsJsonAsync(problem);
    }
}
