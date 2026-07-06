using ControleGastos.Api.Extensions;
using ControleGastos.API.Extensions;
using ControleGastos.API.Middlewares;
using ControleGastos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

// Ponto de entrada da aplicação — configura serviços e pipeline

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

// Controllers — suporte a rotas e endpoints REST

builder.Services.AddControllers();

// Personaliza o retorno padrão das validações automáticas do ASP.NET Core
// Retorna todas as mensagens de erro encontradas,
// deixando a resposta mais simples e objetiva para o cliente.

builder.Services.AddCustomApiBehavior();

// Swagger — documentação interativa da API

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

// CORS — permite requisições do frontend React em desenvolvimento

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

// Build da aplicação e configuração do pipeline HTTP

var app = builder.Build();

// Migrations automáticas — aplica pendências ao iniciar a aplicação
// Garante que o banco esteja sempre atualizado sem intervenção manual

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Tratamento global de exceções não tratadas
// Retorna 400 para violações de regra de negócio e validação,
// e 500 apenas para erros internos inesperados

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Swagger — disponível apenas em ambiente de desenvolvimento

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleGastos API v1");
        options.RoutePrefix = string.Empty; // Swagger abre na raiz: http://localhost:{porta}/
    });
}

// Pipeline HTTP — ordem importa:
// 1. CORS   → antes de qualquer lógica de rota
// 2. Auth   → autorização (preparado para expansão futura)
// 3. Routes → mapeia os controllers

app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();