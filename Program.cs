using Box.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Context;
using Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

#region Configuracion de la base de datos SQLite
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlite("Data Source=MyDB.sqlite"));
builder.Services.AddScoped<IMyDbContext, MyDbContext>();
#endregion

#region Servicios
builder.Services.AddScoped<IFacturaServices,FacturaServices>();
builder.Services.AddScoped<IProductoServices,ProductoServices>();
builder.Services.AddScoped<IProveedorServices,ProveedorServices>();
builder.Services.AddScoped<ICategoriaServices,CategoriaServices>();
builder.Services.AddScoped<IClienteServices, ClienteServices>();
builder.Services.AddScoped<IPagoServices, PagoServices>();
builder.Services.AddScoped<ICuadrarCajaServices, CuadrarCajaServices>();
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/";     // Ruta a tu página de login
    // options.AccessDeniedPath = "/";  // Ruta opcional para acceso denegado
});

builder.Services.AddAuthorization();
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AuthenticatedUser", policy => policy.RequireAuthenticatedUser());
// });
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddSingleton<UserAccountService>();
builder.Services.AddScoped<IUserAccountService,UserAccountService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

// Asegúrate de que estos middleware estén en este orden exacto
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    try
    {
        if (db.Database.EnsureCreated())
        {
            // La base de datos se ha creado (o ya existe)
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear la base de datos: {ex.Message}");
        // Puedes agregar m�s manejo de errores seg�n tus necesidades
    }
}

app.Run();
