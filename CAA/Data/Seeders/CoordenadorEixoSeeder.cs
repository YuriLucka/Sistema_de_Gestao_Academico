using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class CoordenadorEixoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Buscar coordenadores
            var fellipe = await context.Set<Coordenador>().FirstOrDefaultAsync(c => c.Nome == "Fellipe Lima");
            var quelen = await context.Set<Coordenador>().FirstOrDefaultAsync(c => c.Nome == "Quelen Torres");
            var daniele = await context.Set<Coordenador>().FirstOrDefaultAsync(c => c.Nome == "Daniele Pavin");
            var valmir = await context.Set<Coordenador>().FirstOrDefaultAsync(c => c.Nome == "Valmir Almenara");
            var silvio = await context.Set<Coordenador>().FirstOrDefaultAsync(c => c.Nome == "Sílvio Buria");

            // Buscar eixos
            var arquitetura = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Arquitetura");
            var comunicacao = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Comunicação e Design");
            var moda = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Moda e Beleza");
            var direito = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Direito");
            var engenharia = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Engenharia");
            var tecnologia = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Tecnologia da Informação");
            var negocios = await context.Set<Eixo>().FirstOrDefaultAsync(e => e.Nome == "Negócios");

            var coordenadorEixos = new List<CoordenadorEixo>();

            if (fellipe != null && arquitetura != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = fellipe.CoordenadorId, EixoId = arquitetura.EixoId });

            if (quelen != null && comunicacao != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = quelen.CoordenadorId, EixoId = comunicacao.EixoId });
            
            if (quelen != null && moda != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = quelen.CoordenadorId, EixoId = moda.EixoId });

            if (daniele != null && direito != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = daniele.CoordenadorId, EixoId = direito.EixoId });

            if (valmir != null && engenharia != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = valmir.CoordenadorId, EixoId = engenharia.EixoId });

            if (valmir != null && tecnologia != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = valmir.CoordenadorId, EixoId = tecnologia.EixoId });

            if (silvio != null && negocios != null)
                coordenadorEixos.Add(new CoordenadorEixo { CoordenadorId = silvio.CoordenadorId, EixoId = negocios.EixoId });

            var dbSet = context.Set<CoordenadorEixo>();
            foreach (var ce in coordenadorEixos)
            {
                if (!await dbSet.AnyAsync(x => x.CoordenadorId == ce.CoordenadorId && x.EixoId == ce.EixoId))
                {
                    dbSet.Add(ce);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
