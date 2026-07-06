using ControleGastos.Application.UseCases.Pessoa.Criar;
using ControleGastos.Application.UseCases.Pessoa.Deletar;
using ControleGastos.Application.UseCases.Pessoa.Listar;
using ControleGastos.Application.UseCases.Pessoa.Totais;
using ControleGastos.Application.UseCases.Transacao.Criar;
using ControleGastos.Application.UseCases.Transacao.Listar;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        return services;
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // Pessoa
        services.AddScoped<ICriarPessoaHandler, CriarPessoaHandler>();
        services.AddScoped<IListarPessoasHandler, ListarPessoasHandler>();
        services.AddScoped<IDeletarPessoaHandler, DeletarPessoaHandler>();
        services.AddScoped<IObterTotaisHandler, ObterTotaisHandler>();

        // Transação
        services.AddScoped<ICriarTransacaoHandler, CriarTransacaoHandler>();
        services.AddScoped<IListarTransacoesHandler, ListarTransacoesHandler>();
    }
}