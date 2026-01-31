using CAA.Data;
using CAA.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class ContatoExternoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.ContatoExterno.Any())
            {
                context.ContatoExterno.AddRange(
                    new ContatoExterno
                    {
                        Nome = "João da Silva",
                        Email = "joao.silva@empresa.com",
                        Telefone = "(11) 98888-1111",
                    },
                    new ContatoExterno
                    {
                        Nome = "Maria Oliveira",
                        Email = "maria.oliveira@empresa.com",
                        Telefone = "(11) 97777-2222",
                    },
                    new ContatoExterno
                    {
                        Nome = "Carlos Souza",
                        Email = "carlos.souza@empresa.com",
                        Telefone = "(11) 96666-3333",
                    },
                    new ContatoExterno
                    {
                        Nome = "Ana Paula Lima",
                        Email = "ana.lima@empresa.com",
                        Telefone = "(11) 95555-4444",
                    },
                    new ContatoExterno
                    {
                        Nome = "Bruno Martins",
                        Email = "bruno.martins@empresa.com",
                        Telefone = "(11) 94444-5555",
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}