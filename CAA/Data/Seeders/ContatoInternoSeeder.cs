using CAA.Data;
using CAA.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class ContatoInternoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.ContatoInterno.Any())
            {
                var primeiroDepto = context.Departamento.OrderBy(d => d.DepartamentoId).FirstOrDefault();
                var ultimoDepto = context.Departamento.OrderBy(d => d.DepartamentoId).LastOrDefault();

                if (primeiroDepto == null || ultimoDepto == null)
                    return;

                context.ContatoInterno.AddRange(
                    new ContatoInterno
                    {
                        Nome = "Recepção Central",
                        Email = "recepcao@instituicao.edu.br",
                        Telefone = "(11) 4002-8922",
                        DepartamentoId = primeiroDepto.DepartamentoId
                    },
                    new ContatoInterno
                    {
                        Nome = "Secretaria Acadêmica",
                        Email = "secretaria@instituicao.edu.br",
                        Telefone = "(11) 4002-8933",
                        DepartamentoId = ultimoDepto.DepartamentoId
                    },
                    new ContatoInterno
                    {
                        Nome = "Financeiro",
                        Email = "financeiro@instituicao.edu.br",
                        Telefone = "(11) 4002-8944",
                        DepartamentoId = primeiroDepto.DepartamentoId
                    },
                    new ContatoInterno
                    {
                        Nome = "TI - Suporte",
                        Email = "suporte.ti@instituicao.edu.br",
                        Telefone = "(11) 4002-8955",
                        DepartamentoId = ultimoDepto.DepartamentoId
                    },
                    new ContatoInterno
                    {
                        Nome = "Coordenação Pedagógica",
                        Email = "coordenacao@instituicao.edu.br",
                        Telefone = "(11) 4002-8966",
                        DepartamentoId = primeiroDepto.DepartamentoId
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}