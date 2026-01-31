using CAA.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CAA.Data
{
    /// <summary>
    /// Contexto principal do Entity Framework para a aplicação.
    /// Herda de IdentityDbContext para integrar autenticação e autorização com o Identity.
    /// </summary>
    /// <remarks>
    /// Este contexto gerencia as entidades do Identity (usuários, roles, claims, etc) e pode ser expandido para incluir outras entidades do sistema.
    /// </remarks>
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        /// <summary>
        /// Construtor que recebe as opções de configuração do contexto.
        /// </summary>
        /// <param name="options">Opções de configuração do DbContext</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Link> Link { get; set; }
        public DbSet<ContatoInterno> ContatoInterno { get; set; }
        public DbSet<ContatoExterno> ContatoExterno { get; set; }
        public DbSet<ContatoProfessor> ContatoProfessor { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Eixo> Eixo { get; set; }
        public DbSet<Coordenador> Coordenador { get; set; }
        public DbSet<CoordenadorEixo> CoordenadorEixo { get; set; }
        public DbSet<TipoContratoEstagio> TipoContratoEstagio { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CargoDepartamento> CargoDepartamento { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<StatusMatricula> StatusMatricula { get; set; }
        public DbSet<TipoIngresso> TipoIngresso { get; set; }
        public DbSet<Matricula> Matricula { get; set; }
        public DbSet<Estagio> Estagio { get; set; }
        public DbSet<PlanoFinanceiro> PlanoFinanceiro { get; set; }
        public DbSet<Desconto> Desconto { get; set; }
        public DbSet<TipoDesconto> TipoDesconto { get; set; }
        public DbSet<FichaMedica> FichaMedica { get; set; }
        public DbSet<ParametroGeral> ParametroGeral { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Desabilita exclusão em cascata para todas as relações
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configura o tipo decimal para evitar warnings de truncamento
            modelBuilder.Entity<Desconto>()
                .Property(d => d.Valor)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PlanoFinanceiro>()
                .Property(p => p.Valor)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TipoDesconto>()
                .Property(t => t.ValorPadrao)
                .HasColumnType("decimal(18,2)");
        }
    }
}
