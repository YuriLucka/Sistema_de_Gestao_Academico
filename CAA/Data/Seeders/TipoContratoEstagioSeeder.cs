using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class TipoContratoEstagioSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var tipos = new List<string>
            {
                "Plano de Atividades",
                "Relatório de Atividades",
                "Relatório de Estágio",
                "Termo Aditivo",
                "Termo de Compromisso de Estágio",
                "Termo de Rescisão"
            };

            var dbSet = context.Set<TipoContratoEstagio>();
            foreach (var nome in tipos)
            {
                if (!await dbSet.AnyAsync(t => t.Nome == nome))
                {
                    dbSet.Add(new TipoContratoEstagio { Nome = nome });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
