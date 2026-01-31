using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class CoordenadorSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var coordenadores = new List<Coordenador>
            {
                new Coordenador { Nome = "Fellipe Lima" },
                new Coordenador { Nome = "Quelen Torres" },
                new Coordenador { Nome = "Daniele Pavin" },
                new Coordenador { Nome = "Valmir Almenara" },
                new Coordenador { Nome = "Sílvio Buria" },
            };

            var dbSet = context.Set<Coordenador>();
            foreach (var coordenador in coordenadores)
            {
                if (!await dbSet.AnyAsync(c => c.Nome == coordenador.Nome))
                {
                    dbSet.Add(coordenador);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
