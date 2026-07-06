using ControleGastos.Application;
using ControleGastos.Infrastructure;

namespace ControleGastos.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        return services;
    }
}