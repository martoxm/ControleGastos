using ControleGastos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Extensions;

public static class MigrationExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        // Migrations automáticas — aplica pendências ao iniciar a aplicação
        // Garante que o banco esteja sempre atualizado sem intervenção manual

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        return app;
    }
}