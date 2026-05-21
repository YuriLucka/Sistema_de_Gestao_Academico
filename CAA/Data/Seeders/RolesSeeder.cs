using CAA.Models;
using Microsoft.AspNetCore.Identity;

namespace CAA.Data.Seeders
{
    /// <summary>
    /// Seeder respons�vel por garantir a exist�ncia das roles (perfis) do sistema.
    /// </summary>
    public static class RolesSeeder
    {
        /// <summary>
        /// Garante que todas as roles necess�rias estejam criadas no banco de dados.
        /// </summary>
        /// <param name="serviceProvider">Service provider para resolu��o de depend�ncias</param>
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
                "Fichas M�dicas",
                "Agendas & Calend�rios",
                "Central de Contatos",
                "Cursos",
                "Matr�culas",
                "Est�gios",
                "Documentos Institucionais",
                "Links �teis",
                "ProUni"
            };
            // Cria cada role caso ainda n�o exista
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
