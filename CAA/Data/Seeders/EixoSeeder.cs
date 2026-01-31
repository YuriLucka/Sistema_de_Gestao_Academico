using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAA.Data.Seeders
{
    public static class EixoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var eixos = new List<Eixo>
            {
                new Eixo { Nome = "Arquitetura e Urbanismo" },
                new Eixo { Nome = "Comunicação e Design" },
                new Eixo { Nome = "Direito" },
                new Eixo { Nome = "Engenharia" },
                new Eixo { Nome = "Moda e Beleza" },
                new Eixo { Nome = "Negócios" },
                new Eixo { Nome = "Tecnologia da Informação" }
            };

            var dbSet = context.Set<Eixo>();
            foreach (var eixo in eixos)
            {
                if (!await dbSet.AnyAsync(e => e.Nome == eixo.Nome))
                {
                    dbSet.Add(eixo);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
