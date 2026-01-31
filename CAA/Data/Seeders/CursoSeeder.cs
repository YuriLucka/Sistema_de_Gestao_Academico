using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAA.Data.Seeders
{
    public static class CursoSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var cursos = new List<(string Eixo, Titulacao Titulacao, string Nome, int QtdSemestres)>
            {
                ("Negócios", Titulacao.Bacharelado, "Administração", 8),
                ("Tecnologia da Informação", Titulacao.Tecnologo, "Análise e Desenvolvimento de Sistemas", 5),
                ("Arquitetura e Urbanismo", Titulacao.Bacharelado, "Arquitetura e Urbanismo", 10),
                ("Negócios", Titulacao.Bacharelado, "Ciências Contábeis", 8),
                ("Negócios", Titulacao.Bacharelado, "Ciências Econômicas", 8),
                ("Negócios", Titulacao.Tecnologo, "Comércio Exterior", 4),
                ("Comunicação e Design", Titulacao.Bacharelado, "Design – Comunicação visual", 8),
                ("Moda e Beleza", Titulacao.Bacharelado, "Design de Moda", 8),
                ("Moda e Beleza", Titulacao.Tecnologo, "Design de Moda", 4),
                ("Direito", Titulacao.Bacharelado, "Direito", 10),
                ("Engenharia", Titulacao.Bacharelado, "Engenharia Civil", 10),
                ("Engenharia", Titulacao.Bacharelado, "Engenharia de Computação", 10),
                ("Engenharia", Titulacao.Bacharelado, "Engenharia de Produção", 10),
                ("Engenharia", Titulacao.Bacharelado, "Engenharia Mecânica", 10),
                ("Moda e Beleza", Titulacao.Tecnologo, "Estética e Cosmética", 6),
                ("Comunicação e Design", Titulacao.Tecnologo, "Fotografia", 4),
                ("Negócios", Titulacao.Tecnologo, "Gestão Comercial", 4),
                ("Negócios", Titulacao.Tecnologo, "Gestão da Qualidade", 4),
                ("Negócios", Titulacao.Tecnologo, "Gestão de Marketing", 4),
                ("Negócios", Titulacao.Tecnologo, "Gestão de Recursos Humanos", 4),
                ("Negócios", Titulacao.Tecnologo, "Gestão Financeira", 4),
                ("Comunicação e Design", Titulacao.Bacharelado, "Jornalismo", 8),
                ("Negócios", Titulacao.Tecnologo, "Logística", 4),
                ("Negócios", Titulacao.Tecnologo, "Processos Gerenciais", 4),
                ("Comunicação e Design", Titulacao.Tecnologo, "Produção Audiovisual", 4),
                ("Comunicação e Design", Titulacao.Tecnologo, "Produção Fonográfica", 4),
                ("Comunicação e Design", Titulacao.Bacharelado, "Publicidade e Propaganda", 8),
                ("Negócios", Titulacao.Bacharelado, "Relações Internacionais", 8)
            };

            foreach (var (eixoNome, titulacao, cursoNome, qtdSemestres) in cursos)
            {
                var eixo = await context.Eixo.FirstOrDefaultAsync(e => e.Nome == eixoNome);
                if (eixo == null) continue; // Garante que o eixo existe

                var exists = await context.Curso.AnyAsync(c => c.Nome == cursoNome && c.Titulacao == titulacao && c.EixoId == eixo.EixoId);
                if (!exists)
                {
                    context.Curso.Add(new Curso
                    {
                        Nome = cursoNome,
                        Titulacao = titulacao,
                        EixoId = eixo.EixoId,
                        QtdSemestres = qtdSemestres
                    });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
