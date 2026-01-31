using CAA.Models;
using Microsoft.AspNetCore.Identity;

namespace CAA.Data.Seeders
{
    /// <summary>
    /// Seeder responsável por criar usuários do departamento CAA, cargo Atendente.
    /// </summary>
    public static class UsuarioSeeder
    {
        /// <summary>
        /// Garante a criação dos usuários do departamento CAA, cargo Atendente, com todas as roles exceto Admin.
        /// </summary>
        /// <param name="serviceProvider">Service provider para resolução de dependências</param>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Informações dos usuários CAA
            var usuarios = new[]
            {
                new { Email = "kaue.rodrigues@anhembisorocaba.com.br", Nome = "KAUÊ", Sobrenome = "RODRIGUES" },
                new { Email = "danusa.camargo@anhembisorocaba.com.br", Nome = "DANUSA", Sobrenome = "CAMARGO" },
                new { Email = "ana.tavares@anhembisorocaba.com.br", Nome = "ANA", Sobrenome = "TAVARES" },
                new { Email = "luciano.santos@anhembisorocaba.com.br", Nome = "LUCIANO", Sobrenome = "SANTOS" }
            };
            const string userPassword = "Anhembi@2025";
            const string cargo = "ATENDENTE";
            const string departamento = "CAA";

            // Roles sem Admin
            var roles = new[]
            {
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
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            foreach (var u in usuarios)
            {
                var userCargo = u.Email == "luciano.santos@anhembisorocaba.com.br" ? "GESTOR" : cargo;
                var userRoles = u.Email == "luciano.santos@anhembisorocaba.com.br"
                    ? roles
                    : new[] { "Cursos", "Fichas Médicas", "Central de Contatos", "Links Úteis", "Documentos Institucionais" };

                var existingUser = await userManager.FindByEmailAsync(u.Email);
                if (existingUser == null)
                {
                    var user = new Usuario
                    {
                        UserName = u.Email,
                        Email = u.Email,
                        EmailConfirmed = true,
                        Nome = u.Nome,
                        Sobrenome = u.Sobrenome,
                        Ativo = true,
                        Cargo = userCargo,
                        Departamento = departamento
                    };
                    var result = await userManager.CreateAsync(user, userPassword);
                    if (result.Succeeded)
                    {
                        foreach (var role in userRoles)
                            await userManager.AddToRoleAsync(user, role);
                    }
                }
                else
                {
                    existingUser.UserName = u.Email;
                    existingUser.Ativo = true;
                    existingUser.Cargo = userCargo;
                    existingUser.Departamento = string.IsNullOrWhiteSpace(existingUser.Departamento) ? departamento : existingUser.Departamento;
                    await userManager.UpdateAsync(existingUser);

                    foreach (var role in userRoles)
                    {
                        if (!await userManager.IsInRoleAsync(existingUser, role))
                            await userManager.AddToRoleAsync(existingUser, role);
                    }
                }
            }
        }
    }
}
