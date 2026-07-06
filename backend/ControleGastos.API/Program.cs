using ControleGastos.Api.Extensions;
using ControleGastos.API.Extensions;
using ControleGastos.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();// Controllers — suporte a rotas e endpoints REST

builder.Services.AddCustomApiBehavior();// Personaliza o retorno padrão das validações automáticas do ASP.NET Core

builder.Services.AddSwagger();// Swagger — documentação interativa da API

builder.Services.AddCorsPolicy();// CORS — permite requisições do frontend React em desenvolvimento

var app = builder.Build();// Build da aplicação e configuração do pipeline HTTP

app.ApplyMigrations();// Migrations automáticas — aplica pendências ao iniciar a aplicação

app.UseMiddleware<ExceptionHandlerMiddleware>();// Tratamento global de exceções não tratadas

app.UseSwaggerDev();// Swagger — disponível apenas em ambiente de desenvolvimento

// Pipeline HTTP — ordem importa:
app.UseCorsPolicy(); // 1. CORS   → antes de qualquer lógica de rota
app.UseAuthorization(); // 2. Auth   → autorização (preparado para expansão futura)
app.MapControllers();// 3. Routes → mapeia os controllers

app.Run();