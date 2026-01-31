using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class CargoDepartamentoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Buscar cargos
            var atendente = await context.Set<Cargo>().FirstOrDefaultAsync(c => c.Nome == "ATENDENTE");
            var gestor = await context.Set<Cargo>().FirstOrDefaultAsync(c => c.Nome == "GESTOR");
            var diretor = await context.Set<Cargo>().FirstOrDefaultAsync(c => c.Nome == "DIRETOR");
            var secretaria = await context.Set<Cargo>().FirstOrDefaultAsync(c => c.Nome == "SECRETÁRIA");

            // Buscar departamentos
            var caa = await context.Set<Departamento>().FirstOrDefaultAsync(d => d.Nome == "CAA");
            var secretariaDept = await context.Set<Departamento>().FirstOrDefaultAsync(d => d.Nome == "SECRETARIA");
            var marketing = await context.Set<Departamento>().FirstOrDefaultAsync(d => d.Nome == "MARKETING");
            var diretoria = await context.Set<Departamento>().FirstOrDefaultAsync(d => d.Nome == "DIRETORIA");
            var financeiro = await context.Set<Departamento>().FirstOrDefaultAsync(d => d.Nome == "FINANCEIRO");

            var vinculos = new List<CargoDepartamento>();

            // Atendente
            if (atendente != null && caa != null)
                vinculos.Add(new CargoDepartamento { CargoId = atendente.CargoId, DepartamentoId = caa.DepartamentoId });
            if (atendente != null && secretariaDept != null)
                vinculos.Add(new CargoDepartamento { CargoId = atendente.CargoId, DepartamentoId = secretariaDept.DepartamentoId });
            if (atendente != null && marketing != null)
                vinculos.Add(new CargoDepartamento { CargoId = atendente.CargoId, DepartamentoId = marketing.DepartamentoId });
            if (atendente != null && financeiro != null)
                vinculos.Add(new CargoDepartamento { CargoId = atendente.CargoId, DepartamentoId = financeiro.DepartamentoId });

            // Gestor
            if (gestor != null && caa != null)
                vinculos.Add(new CargoDepartamento { CargoId = gestor.CargoId, DepartamentoId = caa.DepartamentoId });
            if (gestor != null && secretariaDept != null)
                vinculos.Add(new CargoDepartamento { CargoId = gestor.CargoId, DepartamentoId = secretariaDept.DepartamentoId });
            if (gestor != null && marketing != null)
                vinculos.Add(new CargoDepartamento { CargoId = gestor.CargoId, DepartamentoId = marketing.DepartamentoId });
            if (gestor != null && financeiro != null)
                vinculos.Add(new CargoDepartamento { CargoId = gestor.CargoId, DepartamentoId = financeiro.DepartamentoId });

            // Diretor
            if (diretor != null && diretoria != null)
                vinculos.Add(new CargoDepartamento { CargoId = diretor.CargoId, DepartamentoId = diretoria.DepartamentoId });

            // Secretaria
            if (secretaria != null && secretariaDept != null)
                vinculos.Add(new CargoDepartamento { CargoId = secretaria.CargoId, DepartamentoId = secretariaDept.DepartamentoId });

            var dbSet = context.Set<CargoDepartamento>();
            foreach (var vinculo in vinculos)
            {
                if (!await dbSet.AnyAsync(x => x.CargoId == vinculo.CargoId && x.DepartamentoId == vinculo.DepartamentoId))
                {
                    dbSet.Add(vinculo);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
