using Inventra.API.Endpoints;
using Inventra.API.Extensions;
using Inventra.Application.Extensions;
using Inventra.Application.Options;
using Inventra.Infrastructure.Extensions;
using Inventra.Infrastructure.Identity;
using Inventra.Infrastructure.SignalR;
using Inventra.Persistence.Context;
using Inventra.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
builder.Services .AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInventraAuthorization();
builder.Services.AddApplicationExt();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices();
builder.Services
    .AddMassTransitServices(
        builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services
    .AddIdentityCore<AppUser>(options => { })
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<InventraDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddSignalR();
builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalR", policy =>
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

    await RoleSeeder
        .SeedAsync(roleManager);
}

app.MapEndpoints();
app.MapHub<NotificationHub>(
    "/hubs/notifications");
app.Run();