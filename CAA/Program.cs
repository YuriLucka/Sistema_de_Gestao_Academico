// Programa principal da aplicação ASP.NET Core Razor Pages
// Este arquivo configura os serviços, autenticação, seed de dados e pipeline HTTP

using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services; // Correto para IEmailSender
using CAA.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using CAA.Hubs;

// Criação do builder para configurar a aplicação
var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados usando a connection string do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // Exibe detalhes de erros de banco em dev

// Configuração da identidade (autenticação e autorização)
builder.Services.AddDefaultIdentity<Usuario>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Exige confirmação de e-mail para login

    // Configuração de bloqueio de conta após tentativas inválidas
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // tempo de bloqueio
    options.Lockout.MaxFailedAccessAttempts = 5; // tentativas permitidas antes do bloqueio
    options.Lockout.AllowedForNewUsers = true; // aplica para novos usuários
})
.AddRoles<IdentityRole>() // Suporte a roles (perfis)
.AddEntityFrameworkStores<ApplicationDbContext>(); // Usa o contexto para armazenar usuários

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = false;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero; // Valida o SecurityStamp a cada requisição
});

builder.Services.AddControllersWithViews(); // Suporte a controllers e views (MVC)
builder.Services.AddSignalR();

// Registro do serviço de envio de e-mail (injeção de dependência)
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Criação do app (pipeline de requisições)
var app = builder.Build();

// Seed de dados iniciais (roles e usuário admin)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Chama o método unificado para rodar todos os seeders
        await SeedDataBase.SeedAll(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao realizar o seed do banco: {ex.Message}");
    }
}

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Facilita migrações em dev
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Página de erro customizada
    // O HSTS força HTTPS em produção
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

app.UseAuthentication(); // Habilita autenticação
app.UseAuthorization(); // Habilita autorização

app.MapStaticAssets(); // Mapeia arquivos estáticos (wwwroot)

// Rota padrão para controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Mapeia páginas Razor
app.MapRazorPages()
   .WithStaticAssets();

app.MapHub<ChatHub>("/chathub");

app.Run(); // Inicia a aplicação
