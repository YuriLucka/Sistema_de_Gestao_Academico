using CAA.Models;
using Microsoft.AspNetCore.Identity;
using CAA.Data.Seeders; // Importa os seeders

namespace CAA.Data
{
    public class SeedDataBase
    {
        /// <summary>
        /// Executa todos os seeders necessários para popular o banco de dados.
        /// </summary>
        public static async Task SeedAll(IServiceProvider serviceProvider)
        {
            await RolesSeeder.SeedAsync(serviceProvider);
            await AdminUserSeeder.SeedAsync(serviceProvider);
            await UsuarioSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de usuários do CAA
            await LinkSeeder.SeedAsync(serviceProvider);
            await EixoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de Eixo
            await CursoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de Curso
            await TipoDescontoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de TipoDesconto
            await CoordenadorSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de Coordenador
            await CoordenadorEixoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de CoordenadorEixo
            await TipoContratoEstagioSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de TipoContratoEstagio
            await DepartamentoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de Departamento
            await CargoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de Cargo
            await CargoDepartamentoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de CargoDepartamento
            await StatusMatriculaSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de StatusMatricula
            await TipoIngressoSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de TipoIngresso
            await PlanoFinanceiroSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de PlanoFinanceiro
            await ContatoProfessorSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de ContatoProfessor
            await EmpresaSeeder.SeedAsync(serviceProvider); // Adiciona o seeder de Empresa
            await MatriculaSeeder.SeedAsync(serviceProvider);
            await EstagioSeeder.SeedAsync(serviceProvider);
            await FichaMedicaSeeder.SeedAsync(serviceProvider);
            await ContatoInternoSeeder.SeedAsync(serviceProvider);
            await ContatoExternoSeeder.SeedAsync(serviceProvider);
            // Adicione outros seeders aqui conforme necessário
        }
    }
}
