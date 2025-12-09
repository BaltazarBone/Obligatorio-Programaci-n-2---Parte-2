using Microsoft.EntityFrameworkCore;
using ObligatorioParte2Razor.Dominio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Configure EF Core (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Necesario para login con sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Activar el middleware de sesión
app.UseSession();

app.MapRazorPages();

app.Run();
