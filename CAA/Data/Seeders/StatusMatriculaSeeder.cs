using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class StatusMatriculaSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var statusList = new List<string>
            {
                "CURSANDO",
                "CANCELADO",
                "TRANCADO",
                "TRANSFERIDO",
                "RESERVA DE VAGA"
            };

            var dbSet = context.Set<StatusMatricula>();
            foreach (var nome in statusList)
            {
                if (!await dbSet.AnyAsync(s => s.Nome == nome))
                {
                    dbSet.Add(new StatusMatricula { Nome = nome });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
