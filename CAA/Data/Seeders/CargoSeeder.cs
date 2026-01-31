using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class CargoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var cargos = new List<string>
            {
                "ATENDENTE",
                "GESTOR",
                "DIRETOR",
                "SECRETÁRIA"
            };

            var dbSet = context.Set<Cargo>();
            foreach (var nome in cargos)
            {
                if (!await dbSet.AnyAsync(c => c.Nome == nome))
                {
                    dbSet.Add(new Cargo { Nome = nome });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
