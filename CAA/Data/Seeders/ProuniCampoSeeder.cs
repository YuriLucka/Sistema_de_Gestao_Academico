using CAA.Data;
using CAA.Models;
using Microsoft.EntityFrameworkCore;

namespace CAA.Data.Seeders
{
    public static class ProuniCampoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (await context.ProuniCampoDocumentos.AnyAsync())
                return;

            var campos = new List<ProuniCampoDocumento>
            {
                // Identificação
                new() { Ordem = 1,  Nome = "RG (Registro Geral / Identidade)",               TemFrenteVerso = true,  Obrigatorio = true,  Ativo = true },
                new() { Ordem = 2,  Nome = "CPF",                                             TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },
                new() { Ordem = 3,  Nome = "Certidão de Nascimento ou Casamento",             TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },

                // Residência
                new() { Ordem = 4,  Nome = "Comprovante de Residência (últimos 3 meses)",     TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },

                // Escolaridade
                new() { Ordem = 5,  Nome = "Histórico Escolar do Ensino Médio",               TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },
                new() { Ordem = 6,  Nome = "Certificado ou Diploma de Conclusão do Ens. Médio", TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },

                // ENEM
                new() { Ordem = 7,  Nome = "Boletim de Desempenho do ENEM",                  TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },

                // Documentos eleitorais
                new() { Ordem = 8,  Nome = "Título de Eleitor",                              TemFrenteVerso = true,  Obrigatorio = true,  Ativo = true },
                new() { Ordem = 9,  Nome = "Certidão de Quitação Eleitoral",                 TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },

                // Foto
                new() { Ordem = 10, Nome = "Foto 3x4 recente",                               TemFrenteVerso = false, Obrigatorio = true,  Ativo = true },

                // Documento militar (opcional — nem todos os candidatos precisam)
                new() { Ordem = 11, Nome = "Certificado de Reservista ou DAE",               TemFrenteVerso = false, Obrigatorio = false, Ativo = true },

                // Renda
                new() { Ordem = 12, Nome = "Declaração de Imposto de Renda (IRPF) — último exercício", TemFrenteVerso = false, Obrigatorio = false, Ativo = true },
                new() { Ordem = 13, Nome = "Declaração de Isento de IRPF",                   TemFrenteVerso = false, Obrigatorio = false, Ativo = true },
                new() { Ordem = 14, Nome = "Holerite / Contracheque (últimos 3 meses)",      TemFrenteVerso = false, Obrigatorio = false, Ativo = true },
                new() { Ordem = 15, Nome = "Declaração de Renda Familiar",                   TemFrenteVerso = false, Obrigatorio = false, Ativo = true },
            };

            context.ProuniCampoDocumentos.AddRange(campos);
            await context.SaveChangesAsync();
        }
    }
}
