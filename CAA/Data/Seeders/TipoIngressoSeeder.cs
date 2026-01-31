using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class TipoIngressoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var tipos = new List<string>
            {
                "Vestibular Presencial",
                "Vestibular Online",
                "ENEM",
                "FIES",
                "PROUNI"
            };

            var dbSet = context.Set<TipoIngresso>();
            foreach (var nome in tipos)
            {
                if (!await dbSet.AnyAsync(t => t.Nome == nome))
                {
                    dbSet.Add(new TipoIngresso { Nome = nome });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
