using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAA.Data.Seeders
{
    public static class LinkSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // Substitua ApplicationDbContext pelo nome real do seu contexto
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var links = new List<Link>
            {
                new Link { Nome = "GDAE", Url = "https://sed.educacao.sp.gov.br/SedCon/ConsultaPublica/Index" },
                new Link { Nome = "PROUNI", Url = "https://acessounico.mec.gov.br/prouni" },
                new Link { Nome = "ENEM", Url = "https://enem.inep.gov.br/participante/" },
                new Link { Nome = "FILAH", Url = "https://enem.inep.gov.br/participante/" },
                new Link { Nome = "MATRICULAS 2026/2", Url = "https://enem.inep.gov.br/participante/" },
                new Link { Nome = "ABARIS", Url = "https://enem.inep.gov.br/participante/" },
                new Link { Nome = "ABARIS DIGITALIZAÇÃO", Url = "https://enem.inep.gov.br/participante/" },
                new Link { Nome = "URBES", Url = "https://www.urbes.com.br/transporte/estudantes/aluno" },
                new Link { Nome = "EMPRESAS PARCEIRAS", Url = "https://www.urbes.com.br/transporte/estudantes/aluno" },
                new Link { Nome = "INTERESSADOS", Url = "https://www.urbes.com.br/transporte/estudantes/aluno" },
                new Link { Nome = "PÓS PAGO", Url = "https://www.urbes.com.br/transporte/estudantes/aluno" },
                new Link { Nome = "INDICAÇÃO AMIGO", Url = "https://www.urbes.com.br/transporte/estudantes/aluno" },
                new Link { Nome = "BURH", Url = "https://athon.burh.com.br/login" }
            };

            var dbSet = context.Set<Link>();

            foreach (var link in links)
            {
                if (!await dbSet.AnyAsync(l => l.Nome == link.Nome && l.Url == link.Url))
                {
                    dbSet.Add(link);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}