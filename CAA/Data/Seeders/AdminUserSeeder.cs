using CAA.Models;
using Microsoft.AspNetCore.Identity;

namespace CAA.Data.Seeders
{
    /// <summary>
    /// Seeder responsável por garantir a existência dos usuários administradores padrão.
    /// </summary>
    public static class AdminUserSeeder
    {
        /// <summary>
        /// Garante a criação dos usuários administradores principais e suas roles.
        /// </summary>
        /// <param name="serviceProvider">Service provider para resolução de dependências</param>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Informações dos administradores
            const string adminEmail = "admin@anhembisorocaba.com.br";
            const string adminPassword = "Admin@123";
            const string adminRoleName = "Admin";
            const string devAdminEmail = "admin@exodusit.com.br";

            // Lista de roles
            var allRoles = new[]
            {
                adminRoleName,
                "Colaboradores",
                "Logs & Auditoria",
                "Parametros",
                "Recados",
                "Fichas Médicas",
                "Agendas & Calendários",
                "Central de Contatos",
                "Cursos",
                "Matrículas",
                "Estágios",
                "Documentos Institucionais",
                "Links Úteis"
            };

            // Garante que todas as roles existem
            foreach (var role in allRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Usuário admin principal
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                // Cria o usuário admin principal
                var adminUser = new Usuario
                {
                    UserName = adminEmail, // Username igual ao e-mail
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nome = "ANHEMBI",
                    Sobrenome = "SOROCABA",
                    Ativo = true, // Ativo sempre true
                    Cargo = "ADMINISTRADOR",
                    Departamento = "ADMINISTRAÇÃO"
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    foreach (var role in allRoles)
                        await userManager.AddToRoleAsync(adminUser, role);
                }
            }
            else
            {
                // Garante que o admin já existente tenha todas as roles e propriedades obrigatórias
                existingAdmin.UserName = adminEmail;
                existingAdmin.Ativo = true;
                existingAdmin.Cargo = string.IsNullOrWhiteSpace(existingAdmin.Cargo) ? "ADMINISTRADOR" : existingAdmin.Cargo;
                existingAdmin.Departamento = string.IsNullOrWhiteSpace(existingAdmin.Departamento) ? "ADMINISTRAÇÃO" : existingAdmin.Departamento;
                await userManager.UpdateAsync(existingAdmin);

                foreach (var role in allRoles)
                {
                    if (!await userManager.IsInRoleAsync(existingAdmin, role))
                        await userManager.AddToRoleAsync(existingAdmin, role);
                }
            }

            // Usuário admin da empresa desenvolvedora
            var existingDevAdmin = await userManager.FindByEmailAsync(devAdminEmail);
            if (existingDevAdmin == null)
            {
                // Cria o usuário admin da empresa desenvolvedora
                var devAdminUser = new Usuario
                {
                    UserName = devAdminEmail, // Username igual ao e-mail
                    Email = devAdminEmail,
                    EmailConfirmed = true,
                    Nome = "EXODUS",
                    Sobrenome = "IT",
                    Ativo = true, // Ativo sempre true
                    Cargo = "ADMINISTRADOR DEV",
                    Departamento = "TI"
                };
                var result = await userManager.CreateAsync(devAdminUser, adminPassword);
                if (result.Succeeded)
                {
                    foreach (var role in allRoles)
                        await userManager.AddToRoleAsync(devAdminUser, role);
                }
            }
            else
            {
                // Garante que o dev admin já existente tenha todas as roles e propriedades obrigatórias
                existingDevAdmin.UserName = devAdminEmail;
                existingDevAdmin.Ativo = true;
                existingDevAdmin.Cargo = string.IsNullOrWhiteSpace(existingDevAdmin.Cargo) ? "ADMINISTRADOR DEV" : existingDevAdmin.Cargo;
                existingDevAdmin.Departamento = string.IsNullOrWhiteSpace(existingDevAdmin.Departamento) ? "TI" : existingDevAdmin.Departamento;
                await userManager.UpdateAsync(existingDevAdmin);

                foreach (var role in allRoles)
                {
                    if (!await userManager.IsInRoleAsync(existingDevAdmin, role))
                        await userManager.AddToRoleAsync(existingDevAdmin, role);
                }
            }

            // Usuários administradores adicionais
            var additionalAdmins = new[]
            {
                new { Email = "gabriel@admin.com.br", Nome = "GABRIEL", Sobrenome = "ADMIN" },
                new { Email = "guilherme@admin.com.br", Nome = "GUILHERME", Sobrenome = "ADMIN" },
                new { Email = "joao@admin.com.br", Nome = "JOAO", Sobrenome = "ADMIN" },
                new { Email = "ramon@admin.com.br", Nome = "RAMON", Sobrenome = "ADMIN" },
                new { Email = "lucas@admin.com.br", Nome = "LUCAS", Sobrenome = "ADMIN" },
                new { Email = "yurilucka@hotmail.com", Nome = "YURI", Sobrenome = "ADMIN" }
            };

            foreach (var admin in additionalAdmins)
            {
                var existingAdminUser = await userManager.FindByEmailAsync(admin.Email);
                if (existingAdminUser == null)
                {
                    var newAdminUser = new Usuario
                    {
                        UserName = admin.Email,
                        Email = admin.Email,
                        EmailConfirmed = true,
                        Nome = admin.Nome,
                        Sobrenome = admin.Sobrenome,
                        Ativo = true,
                        Cargo = "ADMINISTRADOR",
                        Departamento = "ADMINISTRAÇÃO"
                    };
                    var result = await userManager.CreateAsync(newAdminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        foreach (var role in allRoles)
                            await userManager.AddToRoleAsync(newAdminUser, role);
                    }
                }
                else
                {
                    existingAdminUser.UserName = admin.Email;
                    existingAdminUser.Ativo = true;
                    existingAdminUser.Cargo = string.IsNullOrWhiteSpace(existingAdminUser.Cargo) ? "ADMINISTRADOR" : existingAdminUser.Cargo;
                    existingAdminUser.Departamento = string.IsNullOrWhiteSpace(existingAdminUser.Departamento) ? "ADMINISTRAÇÃO" : existingAdminUser.Departamento;
                    await userManager.UpdateAsync(existingAdminUser);

                    foreach (var role in allRoles)
                    {
                        if (!await userManager.IsInRoleAsync(existingAdminUser, role))
                            await userManager.AddToRoleAsync(existingAdminUser, role);
                    }
                }
            }
        }
    }
}
