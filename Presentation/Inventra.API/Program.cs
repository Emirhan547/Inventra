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
    builder.Configuration.GetSection("JwtSettings"));
builder.Services .AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInventraAuthorization();
builder.Services.AddApplicationExt();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services
    .AddIdentityCore<AppUser>(options => { })
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<InventraDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();

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
app.Run();