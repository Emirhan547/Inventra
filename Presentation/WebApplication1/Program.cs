using Inventra.WebUI.Handlers;
using Inventra.WebUI.Services.AuditLogServices;
using Inventra.WebUI.Services.AuthServices;
using Inventra.WebUI.Services.CategoryServices;
using Inventra.WebUI.Services.DashboardServices;
using Inventra.WebUI.Services.HealthServices;
using Inventra.WebUI.Services.NotificationServices;
using Inventra.WebUI.Services.ProductServices;
using Inventra.WebUI.Services.ProfileServices;
using Inventra.WebUI.Services.PurchaseOrderServices;
using Inventra.WebUI.Services.StockMovementServices;
using Inventra.WebUI.Services.StockServices;
using Inventra.WebUI.Services.SupplierServices;
using Inventra.WebUI.Services.UserServices;
using Inventra.WebUI.Services.WarehouseServices;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";

        options.AccessDeniedPath =
            "/Auth/AccessDenied";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<
    IProfileService,
    ProfileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<
    IProductService,
    ProductService>();
builder.Services.AddScoped<
    IWarehouseService,
    WarehouseService>();
builder.Services.AddScoped<IStockService,StockService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<
    IStockMovementService,
    StockMovementService>();
builder.Services.AddScoped<
    IPurchaseOrderService,
    PurchaseOrderService>();
builder.Services.AddScoped<
    IDashboardService,
    DashboardService>();
builder.Services.AddScoped<
    IUserService,
    UserService>();
builder.Services.AddScoped<
    INotificationService,
    NotificationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<
    IAuditLogService,
    AuditLogService>();
builder.Services.AddScoped<
    IHealthService,
    HealthService>();
builder.Services.AddHttpClient(
    "InventraApi",
    client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7041/");
    })
    .AddHttpMessageHandler<JwtTokenHandler>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<JwtTokenHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
