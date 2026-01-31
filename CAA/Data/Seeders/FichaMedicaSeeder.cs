using CAA.Data;
using CAA.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class FichaMedicaSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.FichaMedica.Any())
            {
                context.FichaMedica.AddRange(
                    new FichaMedica
                    {
                        NomeCompleto = "João da Silva",
                        RA = "20230001",
                        Diabetes = false,
                        CriseConvulsiva = false,
                        Taquicardia = false,
                        Bronquite = true,
                        Rinite = true,
                        Sinusite = false,
                        OutrosProblemasCronicos = null,
                        Medicamentos = null,
                        AlergiaMedicamentos = "Dipirona",
                        AlergiaInsetos = null,
                        AlergiaAlimentos = null,
                        OutrasAlergias = null,
                        TratamentoMedico = null,
                        ConvenioMedico = "Unimed",
                        DefCegueira = false,
                        DefBaixaVisao = false,
                        DefSurdocegueira = false,
                        DefSurdez = false,
                        DefAuditiva = false,
                        DefFisica = false,
                        DefMultipla = false,
                        DefIntelectual = false,
                        OutrasDeficiencias = null,
                        InformacoesAdicionais = null,
                        NomeContato1 = "Maria Silva",
                        Contato1 = "(11) 99999-0001",
                        NomeContato2 = "Carlos Silva",
                        Contato2 = "(11) 99999-0002",
                        NomeContato3 = null,
                        Contato3 = null,
                        DataPreenchimento = DateTime.Now
                    },
                    new FichaMedica
                    {
                        NomeCompleto = "Ana Paula Lima",
                        RA = "20230002",
                        Diabetes = true,
                        CriseConvulsiva = false,
                        Taquicardia = false,
                        Bronquite = false,
                        Rinite = false,
                        Sinusite = false,
                        OutrosProblemasCronicos = "Hipertensão",
                        Medicamentos = "Insulina",
                        AlergiaMedicamentos = null,
                        AlergiaInsetos = "Abelha",
                        AlergiaAlimentos = null,
                        OutrasAlergias = null,
                        TratamentoMedico = "Acompanhamento endocrinológico",
                        ConvenioMedico = "Bradesco Saúde",
                        DefCegueira = false,
                        DefBaixaVisao = false,
                        DefSurdocegueira = false,
                        DefSurdez = false,
                        DefAuditiva = false,
                        DefFisica = false,
                        DefMultipla = false,
                        DefIntelectual = false,
                        OutrasDeficiencias = null,
                        InformacoesAdicionais = null,
                        NomeContato1 = "Paulo Lima",
                        Contato1 = "(11) 99999-0003",
                        NomeContato2 = null,
                        Contato2 = null,
                        NomeContato3 = null,
                        Contato3 = null,
                        DataPreenchimento = DateTime.Now
                    },
                    new FichaMedica
                    {
                        NomeCompleto = "Carlos Souza",
                        RA = "20230003",
                        Diabetes = false,
                        CriseConvulsiva = true,
                        Taquicardia = false,
                        Bronquite = false,
                        Rinite = false,
                        Sinusite = true,
                        OutrosProblemasCronicos = null,
                        Medicamentos = "Gardenal",
                        AlergiaMedicamentos = null,
                        AlergiaInsetos = null,
                        AlergiaAlimentos = "Amendoim",
                        OutrasAlergias = null,
                        TratamentoMedico = "Neurologista",
                        ConvenioMedico = null,
                        DefCegueira = false,
                        DefBaixaVisao = false,
                        DefSurdocegueira = false,
                        DefSurdez = false,
                        DefAuditiva = false,
                        DefFisica = false,
                        DefMultipla = false,
                        DefIntelectual = false,
                        OutrasDeficiencias = null,
                        InformacoesAdicionais = "Evitar atividades físicas intensas.",
                        NomeContato1 = "Fernanda Souza",
                        Contato1 = "(11) 99999-0004",
                        NomeContato2 = null,
                        Contato2 = null,
                        NomeContato3 = null,
                        Contato3 = null,
                        DataPreenchimento = DateTime.Now
                    },
                    new FichaMedica
                    {
                        NomeCompleto = "Bruna Martins",
                        RA = "20230004",
                        Diabetes = false,
                        CriseConvulsiva = false,
                        Taquicardia = true,
                        Bronquite = false,
                        Rinite = false,
                        Sinusite = false,
                        OutrosProblemasCronicos = null,
                        Medicamentos = null,
                        AlergiaMedicamentos = null,
                        AlergiaInsetos = null,
                        AlergiaAlimentos = null,
                        OutrasAlergias = "Poeira",
                        TratamentoMedico = null,
                        ConvenioMedico = "SulAmérica",
                        DefCegueira = false,
                        DefBaixaVisao = false,
                        DefSurdocegueira = false,
                        DefSurdez = false,
                        DefAuditiva = false,
                        DefFisica = false,
                        DefMultipla = false,
                        DefIntelectual = false,
                        OutrasDeficiencias = null,
                        InformacoesAdicionais = null,
                        NomeContato1 = "Marcos Martins",
                        Contato1 = "(11) 99999-0005",
                        NomeContato2 = "Juliana Martins",
                        Contato2 = "(11) 99999-0006",
                        NomeContato3 = null,
                        Contato3 = null,
                        DataPreenchimento = DateTime.Now
                    },
                    new FichaMedica
                    {
                        NomeCompleto = "Eduarda Souza",
                        RA = "20230005",
                        Diabetes = false,
                        CriseConvulsiva = false,
                        Taquicardia = false,
                        Bronquite = false,
                        Rinite = true,
                        Sinusite = true,
                        OutrosProblemasCronicos = "Asma leve",
                        Medicamentos = "Aerolin",
                        AlergiaMedicamentos = null,
                        AlergiaInsetos = null,
                        AlergiaAlimentos = null,
                        OutrasAlergias = null,
                        TratamentoMedico = null,
                        ConvenioMedico = null,
                        DefCegueira = false,
                        DefBaixaVisao = false,
                        DefSurdocegueira = false,
                        DefSurdez = false,
                        DefAuditiva = false,
                        DefFisica = false,
                        DefMultipla = false,
                        DefIntelectual = false,
                        OutrasDeficiencias = null,
                        InformacoesAdicionais = null,
                        NomeContato1 = "Rafael Souza",
                        Contato1 = "(11) 99999-0007",
                        NomeContato2 = null,
                        Contato2 = null,
                        NomeContato3 = null,
                        Contato3 = null,
                        DataPreenchimento = DateTime.Now
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
