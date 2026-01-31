using CAA.Data;
using CAA.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class EstagioSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Estagio.Any())
            {
                var empresas = context.Empresa.Take(3).ToList();
                var cursos = context.Curso.Take(3).ToList();
                var tiposContrato = context.TipoContratoEstagio.Take(3).ToList();

                if (!empresas.Any() || !cursos.Any() || !tiposContrato.Any())
                    return;

                context.Estagio.AddRange(
                    new Estagio
                    {
                        RA = "20230001",
                        Nome = "João Estagiário",
                        CursoId = cursos[0].CursoId,
                        TipoContratoEstagioId = tiposContrato[0].TipoContratoEstagioId,
                        EmpresaId = empresas[0].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-12),
                        VigenciaTermino = DateTime.Now.AddMonths(-6),
                        Apolice = "APO123456",
                        Seguradora = "Porto Seguro"
                    },
                    new Estagio
                    {
                        RA = "20230002",
                        Nome = "Maria Estagiária",
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[1 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[1 % empresas.Count].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now.AddMonths(-10),
                        VigenciaTermino = DateTime.Now.AddMonths(-4),
                        Apolice = "APO654321",
                        Seguradora = "SulAmérica"
                    },
                    new Estagio
                    {
                        RA = "20230003",
                        Nome = "Carlos Souza",
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[2 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[2 % empresas.Count].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-8),
                        VigenciaTermino = DateTime.Now.AddMonths(-2),
                        Apolice = "APO789123",
                        Seguradora = "Bradesco"
                    },
                    new Estagio
                    {
                        RA = "20230004",
                        Nome = "Ana Paula Lima",
                        CursoId = cursos[0].CursoId,
                        TipoContratoEstagioId = tiposContrato[0].TipoContratoEstagioId,
                        EmpresaId = empresas[1 % empresas.Count].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now.AddMonths(-6),
                        VigenciaTermino = DateTime.Now,
                        Apolice = "APO321987",
                        Seguradora = "Porto Seguro"
                    },
                    new Estagio
                    {
                        RA = "20230005",
                        Nome = "Bruno Martins",
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[1 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[2 % empresas.Count].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-5),
                        VigenciaTermino = DateTime.Now.AddMonths(1),
                        Apolice = "APO456789",
                        Seguradora = "SulAmérica"
                    },
                    new Estagio
                    {
                        RA = "20230006",
                        Nome = "Carla Mendes",
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[2 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[0].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now.AddMonths(-4),
                        VigenciaTermino = DateTime.Now.AddMonths(2),
                        Apolice = "APO654987",
                        Seguradora = "Bradesco"
                    },
                    new Estagio
                    {
                        RA = "20230007",
                        Nome = "Diego Ferreira",
                        CursoId = cursos[0].CursoId,
                        TipoContratoEstagioId = tiposContrato[0].TipoContratoEstagioId,
                        EmpresaId = empresas[2 % empresas.Count].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-3),
                        VigenciaTermino = DateTime.Now.AddMonths(3),
                        Apolice = "APO852963",
                        Seguradora = "Porto Seguro"
                    },
                    new Estagio
                    {
                        RA = "20230008",
                        Nome = "Eduarda Souza",
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[1 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[0].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now.AddMonths(-2),
                        VigenciaTermino = DateTime.Now.AddMonths(4),
                        Apolice = "APO963258",
                        Seguradora = "SulAmérica"
                    },
                    new Estagio
                    {
                        RA = "20230009",
                        Nome = "Felipe Ramos",
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[2 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[1 % empresas.Count].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-1),
                        VigenciaTermino = DateTime.Now.AddMonths(5),
                        Apolice = "APO147258",
                        Seguradora = "Bradesco"
                    },
                    new Estagio
                    {
                        RA = "20230010",
                        Nome = "Gabriela Lima",
                        CursoId = cursos[0].CursoId,
                        TipoContratoEstagioId = tiposContrato[0].TipoContratoEstagioId,
                        EmpresaId = empresas[2 % empresas.Count].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now,
                        VigenciaTermino = DateTime.Now.AddMonths(6),
                        Apolice = "APO369258",
                        Seguradora = "Porto Seguro"
                    },
                    new Estagio
                    {
                        RA = "20230011",
                        Nome = "Lucas Pereira",
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[1 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[1 % empresas.Count].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-7),
                        VigenciaTermino = DateTime.Now.AddMonths(-1),
                        Apolice = "APO741852",
                        Seguradora = "SulAmérica"
                    },
                    new Estagio
                    {
                        RA = "20230012",
                        Nome = "Patrícia Gomes",
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[2 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[2 % empresas.Count].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now.AddMonths(-9),
                        VigenciaTermino = DateTime.Now.AddMonths(-3),
                        Apolice = "APO258369",
                        Seguradora = "Bradesco"
                    },
                    new Estagio
                    {
                        RA = "20230013",
                        Nome = "Renato Silva",
                        CursoId = cursos[0].CursoId,
                        TipoContratoEstagioId = tiposContrato[0].TipoContratoEstagioId,
                        EmpresaId = empresas[0].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-11),
                        VigenciaTermino = DateTime.Now.AddMonths(-5),
                        Apolice = "APO159357",
                        Seguradora = "Porto Seguro"
                    },
                    new Estagio
                    {
                        RA = "20230014",
                        Nome = "Sofia Almeida",
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[1 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[1 % empresas.Count].EmpresaId,
                        Integradora = "IEL",
                        VigenciaInicio = DateTime.Now.AddMonths(-6),
                        VigenciaTermino = DateTime.Now.AddMonths(2),
                        Apolice = "APO357951",
                        Seguradora = "SulAmérica"
                    },
                    new Estagio
                    {
                        RA = "20230015",
                        Nome = "Thiago Costa",
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        TipoContratoEstagioId = tiposContrato[2 % tiposContrato.Count].TipoContratoEstagioId,
                        EmpresaId = empresas[2 % empresas.Count].EmpresaId,
                        Integradora = "CIEE",
                        VigenciaInicio = DateTime.Now.AddMonths(-2),
                        VigenciaTermino = DateTime.Now.AddMonths(8),
                        Apolice = "APO456123",
                        Seguradora = "Bradesco"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}