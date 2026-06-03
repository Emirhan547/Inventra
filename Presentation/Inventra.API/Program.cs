using Inventra.Persistence.Extensions;
using Scalar.AspNetCore;
using Inventra.API.Extensions;
using Inventra.API.Endpoints;
using Inventra.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationExt();
builder.Services.AddOpenApi();

builder.Services.AddPersistenceServices(
    builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();