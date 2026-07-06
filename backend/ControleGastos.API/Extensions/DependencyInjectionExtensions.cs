using ControleGastos.Application.Intefaces;
using ControleGastos.Application.Interfaces;
using ControleGastos.Application.Services;
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

        // Repositórios
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();

        // App Services
        services.AddScoped<IPessoaAppService, PessoaAppService>();
        services.AddScoped<ITransacaoAppService, TransacaoAppService>();

        return services;
    }
}