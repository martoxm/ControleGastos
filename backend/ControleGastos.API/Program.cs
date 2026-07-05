using ControleGastos.Application.DTOs;
using ControleGastos.Application.Services;
using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using ControleGastos.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

// =============================================================
// Ponto de entrada da aplicação — configura serviços e pipeline
// =============================================================

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Banco de dados — SQLite via connection string do appsettings.json
// Lança exceção clara se a connection string não estiver configurada
// ---------------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("A connection string 'DefaultConnection' não foi encontrada.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// ---------------------------------------------------------------
// Repositórios — Scoped: uma instância por requisição HTTP
// Seguindo o princípio de inversão de dependência (DIP do SOLID)
// ---------------------------------------------------------------
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// ---------------------------------------------------------------
// Serviços da camada de aplicação — orquestram as regras de negócio
// ---------------------------------------------------------------
builder.Services.AddScoped<PessoaAppService>();
builder.Services.AddScoped<TransacaoAppService>();

// ---------------------------------------------------------------
// Controllers — suporte a rotas e endpoints REST
// ---------------------------------------------------------------
builder.Services.AddControllers();

// ---------------------------------------------------------------
// Personaliza o retorno padrão das validações automáticas do ASP.NET Core
// Retorna apenas a primeira mensagem de erro encontrada,
// deixando a resposta mais simples e objetiva para o cliente.
// ---------------------------------------------------------------
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var primeiroErro = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x => x.ErrorMessage)
            .FirstOrDefault();

        return new BadRequestObjectResult(new ResponseErrorDto
        {
            Erro = primeiroErro ?? "Dados inválidos informados.",
            Status = 400
        });
    };
});

// ---------------------------------------------------------------
// Swagger — documentação interativa da API
// ---------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ControleGastos API",
        Version = "v1",
        Description = "API para gerenciamento de pessoas e transações financeiras."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// ---------------------------------------------------------------
// CORS — permite requisições do frontend React em desenvolvimento
// ---------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173"
              )
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// =============================================================
// Build da aplicação e configuração do pipeline HTTP
// =============================================================
var app = builder.Build();

// ---------------------------------------------------------------
// Migrations automáticas — aplica pendências ao iniciar a aplicação
// Garante que o banco esteja sempre atualizado sem intervenção manual
// ---------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// ---------------------------------------------------------------
// Tratamento global de exceções não tratadas
// Retorna 400 para violações de regra de negócio e validação,
// e 500 apenas para erros internos inesperados
// ---------------------------------------------------------------
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionFeature?.Error;

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
    });
});

// ---------------------------------------------------------------
// Swagger — disponível apenas em ambiente de desenvolvimento
// ---------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleGastos API v1");
        options.RoutePrefix = string.Empty; // Swagger abre na raiz: http://localhost:{porta}/
    });
}

// ---------------------------------------------------------------
// Pipeline HTTP — ordem importa:
// 1. HTTPS  → redireciona HTTP para HTTPS
// 2. CORS   → antes de qualquer lógica de rota
// 3. Auth   → autorização (preparado para expansão futura)
// 4. Routes → mapeia os controllers
// ---------------------------------------------------------------
app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();