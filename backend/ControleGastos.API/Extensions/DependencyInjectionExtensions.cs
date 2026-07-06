using ControleGastos.Application.UseCases.Pessoa.Criar;
using ControleGastos.Application.UseCases.Pessoa.Deletar;
using ControleGastos.Application.UseCases.Pessoa.Listar;
using ControleGastos.Application.UseCases.Pessoa.Totais;
using ControleGastos.Application.UseCases.Transacao.Criar;
using ControleGastos.Application.UseCases.Transacao.Listar;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using ControleGastos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Extensions;

/// <summary>
/// Centraliza o registro de todas as dependências da aplicação,
/// mantendo o Program.cs limpo e com responsabilidade única.
/// </summary>
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("A connection string 'DefaultConnection' não foi encontrada.");

        // Banco de dados
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Repositórios — camada de infraestrutura
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();

        // Use Cases — Pessoa
        // Cada handler representa um único caso de uso (SRP do SOLID).
        // O controller depende da interface, não da implementação (DIP do SOLID).
        services.AddScoped<ICriarPessoaHandler, CriarPessoaHandler>();
        services.AddScoped<IListarPessoasHandler, ListarPessoasHandler>();
        services.AddScoped<IDeletarPessoaHandler, DeletarPessoaHandler>();
        services.AddScoped<IObterTotaisHandler, ObterTotaisHandler>();

        // Use Cases — Transação
        services.AddScoped<ICriarTransacaoHandler, CriarTransacaoHandler>();
        services.AddScoped<IListarTransacoesHandler, ListarTransacoesHandler>();

        return services;
    }
}