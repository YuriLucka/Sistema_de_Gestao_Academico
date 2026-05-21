// Programa principal da aplica��o ASP.NET Core Razor Pages
// Este arquivo configura os servi�os, autentica��o, seed de dados e pipeline HTTP

using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services; // Correto para IEmailSender
using CAA.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using CAA.Hubs;

// Cria��o do builder para configurar a aplica��o
var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados usando a connection string do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // Exibe detalhes de erros de banco em dev

// Configura��o da identidade (autentica��o e autoriza��o)
builder.Services.AddDefaultIdentity<Usuario>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Exige confirma��o de e-mail para login

    // Configura��o de bloqueio de conta ap�s tentativas inv�lidas
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // tempo de bloqueio
    options.Lockout.MaxFailedAccessAttempts = 5; // tentativas permitidas antes do bloqueio
    options.Lockout.AllowedForNewUsers = true; // aplica para novos usu�rios
})
.AddRoles<IdentityRole>() // Suporte a roles (perfis)
.AddEntityFrameworkStores<ApplicationDbContext>(); // Usa o contexto para armazenar usu�rios

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = false;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero; // Valida o SecurityStamp a cada requisi��o
});

builder.Services.AddControllersWithViews(); // Suporte a controllers e views (MVC)
builder.Services.AddSignalR();

// Registro do servi�o de envio de e-mail (inje��o de depend�ncia)
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Cria��o do app (pipeline de requisi��es)
var app = builder.Build();

// Seed de dados iniciais (roles e usu�rio admin)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Chama o m�todo unificado para rodar todos os seeders
        await SeedDataBase.SeedAll(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao realizar o seed do banco: {ex.Message}");
    }
}

// Configura��o do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Facilita migra��es em dev
}
else
{
    app.UseExceptionHandler("/Home/Error"); // P�gina de erro customizada
    // O HSTS for�a HTTPS em produ��o
    app.UseHsts();
}

var supportedCultures = new[] { new CultureInfo("pt-BR") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseRouting(); // Habilita roteamento

app.UseAuthentication(); // Habilita autentica��o
app.UseAuthorization(); // Habilita autoriza��o

app.MapStaticAssets(); // Mapeia arquivos est�ticos (wwwroot)

// Rota padr�o para controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Mapeia p�ginas Razor
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<ChatHub>("/chathub");

app.Run(); // Inicia a aplica��o
