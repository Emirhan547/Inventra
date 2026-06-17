using Inventra.API.Endpoints;
using Inventra.API.Extensions;
using Inventra.Application.Extensions;
using Inventra.Application.Options;
using Inventra.Infrastructure.AI;
using Inventra.Infrastructure.Extensions;
using Inventra.Infrastructure.Identity;
using Inventra.Infrastructure.SignalR;
using Inventra.Persistence.Context;
using Inventra.Persistence.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(
        context.Configuration);
});
builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(
        "JwtSettings"));

builder.Services.AddJwtAuthentication(
    builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInventraAuthorization();

builder.Services.AddApplicationExt();

builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices();

builder.Services.AddMassTransitServices(
    builder.Configuration);

builder.Services.AddPersistenceServices(
    builder.Configuration);

builder.Services
    .AddIdentityCore<AppUser>(options => { })
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<InventraDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<InventraDbContext>(
        name: "sqlserver");

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "SignalR",
        policy =>
        {
            policy
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseGlobalExceptionMiddleware();

app.UseCorrelationId();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("SignalR");

app.UseAuthentication();

app.UseUserLogContext();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<
                RoleManager<AppRole>>();

    await RoleSeeder.SeedAsync(
        roleManager);
}

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = async (
            context,
            report) =>
        {
            var result = new
            {
                Status =
                    report.Status.ToString(),

                Checks =
                    report.Entries.Select(x =>
                        new
                        {
                            Name = x.Key,

                            Status =
                                x.Value.Status
                                    .ToString(),

                            Description =
                                x.Value.Description
                        })
            };

            context.Response.ContentType =
                "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    result));
        }
    });

app.MapEndpoints();

app.MapHub<NotificationHub>(
    "/hubs/notifications");

app.Run();