using ControleGastos.Api.Extensions;
using ControleGastos.API.Extensions;
using ControleGastos.API.Middlewares;

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

builder.Services.AddSwagger();

// CORS — permite requisições do frontend React em desenvolvimento

builder.Services.AddCorsPolicy();

// Build da aplicação e configuração do pipeline HTTP

var app = builder.Build();

// Migrations automáticas — aplica pendências ao iniciar a aplicação

app.ApplyMigrations();

// Tratamento global de exceções não tratadas
// Retorna 400 para violações de regra de negócio e validação,
// e 500 apenas para erros internos inesperados

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Swagger — disponível apenas em ambiente de desenvolvimento

app.UseSwaggerDev();

// Pipeline HTTP — ordem importa:
// 1. CORS   → antes de qualquer lógica de rota
// 2. Auth   → autorização (preparado para expansão futura)
// 3. Routes → mapeia os controllers

app.UseCorsPolicy();
app.UseAuthorization();
app.MapControllers();

app.Run();