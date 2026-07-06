using ControleGastos.Application.Responses;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.API.Middlewares;

/// <summary>
/// Middleware responsável por capturar exceções não tratadas e retornar
/// respostas padronizadas, mantendo o Program.cs limpo e com responsabilidade única.
/// </summary>
public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is RegraDeNegocioException || exception is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new ResponseErrorDto
            {
                Erro = exception.Message,
                Status = 400
            });

            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(new ResponseErrorDto
        {
            Erro = "Ocorreu um erro interno no servidor.",
            Status = 500
        });
    }
}