using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using ControleGastos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("A connection string 'DefaultConnection' não foi encontrada.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
    }
}