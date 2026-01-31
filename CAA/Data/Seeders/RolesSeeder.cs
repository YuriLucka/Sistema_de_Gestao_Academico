using CAA.Models;
using Microsoft.AspNetCore.Identity;

namespace CAA.Data.Seeders
{
    /// <summary>
    /// Seeder responsável por garantir a existência das roles (perfis) do sistema.
    /// </summary>
    public static class RolesSeeder
    {
        /// <summary>
        /// Garante que todas as roles necessárias estejam criadas no banco de dados.
        /// </summary>
        /// <param name="serviceProvider">Service provider para resolução de dependências</param>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[]
            {
                "Admin",
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
            // Cria cada role caso ainda não exista
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
