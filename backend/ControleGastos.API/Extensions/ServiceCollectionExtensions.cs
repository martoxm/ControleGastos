using ControleGastos.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var erros = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        x => NormalizarChaveModelState(x.Key),
                        x => x.Value!.Errors
                            .Select(e => NormalizarMensagemErro(e.ErrorMessage))
                            .Where(m => !string.IsNullOrWhiteSpace(m))
                            .ToArray()
                    );

                return new BadRequestObjectResult(new ResponseErrorDto
                {
                    Erro = "Existem campos inválidos na requisição.",
                    Status = 400,
                    Erros = erros
                });
            };
        });

        return services;
    }

    private static string NormalizarChaveModelState(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return "CorpoDaRequisicao";

        if (key == "dto")
            return "CorpoDaRequisicao";

        if (key.StartsWith("$."))
            return char.ToUpperInvariant(key[2]) + key.Substring(3);

        return char.ToUpperInvariant(key[0]) + key.Substring(1);
    }

    private static string NormalizarMensagemErro(string mensagem)
    {
        if (string.IsNullOrWhiteSpace(mensagem))
            return mensagem;

        if (mensagem.Contains("could not be converted to System.Guid", StringComparison.OrdinalIgnoreCase))
            return "O identificador informado é inválido.";

        if (mensagem.Contains("The dto field is required", StringComparison.OrdinalIgnoreCase))
            return "O corpo da requisição é obrigatório.";

        return mensagem;
    }
}