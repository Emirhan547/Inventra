using Inventra.API.Endpoints;
using Inventra.API.Extensions;
using Inventra.Application.Extensions;
using Inventra.Application.Options;
using Inventra.Infrastructure.Extensions;
using Inventra.Infrastructure.Identity;
using Inventra.Persistence.Context;
using Inventra.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(
        "JwtSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationExt();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<InventraDbContext>().AddDefaultTokenProviders();
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