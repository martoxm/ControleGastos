using ControleGastos.API.Extensions;
using ControleGastos.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwagger();

builder.Services.AddCorsPolicy();

var app = builder.Build();

app.ApplyMigrations();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwaggerDev();

app.UseCorsPolicy();
app.UseAuthorization();
app.MapControllers();

app.Run();