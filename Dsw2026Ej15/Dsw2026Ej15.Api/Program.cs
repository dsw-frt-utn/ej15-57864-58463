using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

builder.Services.AddControllers();


builder.Services.AddHealthChecks();

var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();


app.MapHealthChecks("/health-check");

app.UseAuthorization();
app.MapControllers();

app.Run();