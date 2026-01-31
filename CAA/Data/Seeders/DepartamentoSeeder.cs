using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class DepartamentoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var departamentos = new List<string>
            {
                "CAA",
                "SECRETARIA",
                "MARKETING",
                "DIRETORIA",
                "FINANCEIRO"
            };

            var dbSet = context.Set<Departamento>();
            foreach (var nome in departamentos)
            {
                if (!await dbSet.AnyAsync(d => d.Nome == nome))
                {
                    dbSet.Add(new Departamento { Nome = nome });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
