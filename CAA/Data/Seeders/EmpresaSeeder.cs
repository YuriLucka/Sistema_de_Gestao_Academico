using CAA.Data;
using CAA.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class EmpresaSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Empresa.Any())
            {
                context.Empresa.AddRange(
                    new Empresa { RazaoSocial = "Alpha Tecnologia Ltda", NomeFantasia = "AlphaTech", CNPJ = "12.345.678/0001-90" },
                    new Empresa { RazaoSocial = "Beta Soluções S.A.", NomeFantasia = "BetaSol", CNPJ = "23.456.789/0001-01" },
                    new Empresa { RazaoSocial = "Gamma Serviços ME", NomeFantasia = "GammaServ", CNPJ = "34.567.890/0001-12" },
                    new Empresa { RazaoSocial = "Delta Comércio EIRELI", NomeFantasia = "DeltaCom", CNPJ = "45.678.901/0001-23" },
                    new Empresa { RazaoSocial = "Epsilon Indústria Ltda", NomeFantasia = "EpsilonInd", CNPJ = "56.789.012/0001-34" },
                    new Empresa { RazaoSocial = "Zeta Consultoria S/S", NomeFantasia = "ZetaCons", CNPJ = "67.890.123/0001-45" },
                    new Empresa { RazaoSocial = "Eta Engenharia Ltda", NomeFantasia = "EtaEng", CNPJ = "78.901.234/0001-56" },
                    new Empresa { RazaoSocial = "Theta Educacional S.A.", NomeFantasia = "ThetaEdu", CNPJ = "89.012.345/0001-67" },
                    new Empresa { RazaoSocial = "Iota Logística ME", NomeFantasia = "IotaLog", CNPJ = "90.123.456/0001-78" },
                    new Empresa { RazaoSocial = "Kappa Alimentação Ltda", NomeFantasia = "KappaFood", CNPJ = "01.234.567/0001-89" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}