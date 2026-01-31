using CAA.Data;
using CAA.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Data.Seeders
{
    public static class MatriculaSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Matricula.Any())
            {
                var eixos = context.Eixo.Take(3).ToList();
                var cursos = context.Curso.Take(3).ToList();
                var statusList = context.StatusMatricula.Take(3).ToList();
                var tiposIngresso = context.TipoIngresso.Take(3).ToList();
                var usuarios = context.Users.Take(3).ToList();

                if (!eixos.Any() || !cursos.Any() || !statusList.Any() || !tiposIngresso.Any() || !usuarios.Any())
                    return;

                context.Matricula.AddRange(
                    new Matricula
                    {
                        NomeCompleto = "João da Silva",
                        Email = "joao.silva@email.com",
                        Celular = "(11) 99999-0001",
                        EscolaOrigem = "Escola Estadual Central",
                        Cidade = "São Paulo",
                        AnoFormacao = 2023,
                        EixoId = eixos[0].EixoId,
                        CursoId = cursos[0].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[0].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[0].TipoIngressoId,
                        UsuarioId = usuarios[0].Id,
                        Brinde = true,
                        Observacao = "Primeira matrícula.",
                        Motivacao = "Indicação de amigo.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Maria Oliveira",
                        Email = "maria.oliveira@email.com",
                        Celular = "(11) 99999-0002",
                        EscolaOrigem = "Colégio Objetivo",
                        Cidade = "Campinas",
                        AnoFormacao = 2022,
                        EixoId = eixos[1 % eixos.Count].EixoId,
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[1 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[1 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[1 % usuarios.Count].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Vestibular.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Carlos Souza",
                        Email = "carlos.souza@email.com",
                        Celular = "(11) 99999-0003",
                        EscolaOrigem = "Escola Municipal do Saber",
                        Cidade = "Ribeirão Preto",
                        AnoFormacao = 2021,
                        EixoId = eixos[2 % eixos.Count].EixoId,
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[2 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[2 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[2 % usuarios.Count].Id,
                        Brinde = true,
                        Observacao = "Transferido de outra escola.",
                        Motivacao = "Mudança de cidade.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Ana Paula Lima",
                        Email = "ana.lima@email.com",
                        Celular = "(11) 99999-0004",
                        EscolaOrigem = "Colégio Anglo",
                        Cidade = "Sorocaba",
                        AnoFormacao = 2023,
                        EixoId = eixos[0].EixoId,
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[0].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[1 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[0].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Indicação de professor.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Bruno Martins",
                        Email = "bruno.martins@email.com",
                        Celular = "(11) 99999-0005",
                        EscolaOrigem = "Escola Estadual do Futuro",
                        Cidade = "Bauru",
                        AnoFormacao = 2020,
                        EixoId = eixos[1 % eixos.Count].EixoId,
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[1 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[2 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[1 % usuarios.Count].Id,
                        Brinde = true,
                        Observacao = "Participou de feira de profissões.",
                        Motivacao = "Feira de profissões.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Carla Mendes",
                        Email = "carla.mendes@email.com",
                        Celular = "(11) 99999-0006",
                        EscolaOrigem = "Colégio Etapa",
                        Cidade = "Jundiaí",
                        AnoFormacao = 2021,
                        EixoId = eixos[2 % eixos.Count].EixoId,
                        CursoId = cursos[0].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[2 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[0].TipoIngressoId,
                        UsuarioId = usuarios[2 % usuarios.Count].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Indicação de amigo.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Diego Ferreira",
                        Email = "diego.ferreira@email.com",
                        Celular = "(11) 99999-0007",
                        EscolaOrigem = "Escola Estadual Nova Geração",
                        Cidade = "Piracicaba",
                        AnoFormacao = 2022,
                        EixoId = eixos[0].EixoId,
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[0].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[1 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[0].Id,
                        Brinde = true,
                        Observacao = "",
                        Motivacao = "Vestibular.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Eduarda Souza",
                        Email = "eduarda.souza@email.com",
                        Celular = "(11) 99999-0008",
                        EscolaOrigem = "Colégio Bandeirantes",
                        Cidade = "Limeira",
                        AnoFormacao = 2023,
                        EixoId = eixos[1 % eixos.Count].EixoId,
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[1 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[2 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[1 % usuarios.Count].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Transferência.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Felipe Ramos",
                        Email = "felipe.ramos@email.com",
                        Celular = "(11) 99999-0009",
                        EscolaOrigem = "Escola Estadual do Saber",
                        Cidade = "Araraquara",
                        AnoFormacao = 2021,
                        EixoId = eixos[2 % eixos.Count].EixoId,
                        CursoId = cursos[0].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[2 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[0].TipoIngressoId,
                        UsuarioId = usuarios[2 % usuarios.Count].Id,
                        Brinde = true,
                        Observacao = "",
                        Motivacao = "Indicação de professor.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Gabriela Lima",
                        Email = "gabriela.lima@email.com",
                        Celular = "(11) 99999-0010",
                        EscolaOrigem = "Colégio Objetivo",
                        Cidade = "São Carlos",
                        AnoFormacao = 2020,
                        EixoId = eixos[0].EixoId,
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[0].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[1 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[0].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Vestibular.",
                        DataMatricula = DateTime.Now
                    }, new Matricula
                    {
                        NomeCompleto = "Helena Castro",
                        Email = "helena.castro@email.com",
                        Celular = "(11) 99999-0011",
                        EscolaOrigem = "Colégio São José",
                        Cidade = "Franca",
                        AnoFormacao = 2022,
                        EixoId = eixos[1 % eixos.Count].EixoId,
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[1 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[2 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[1 % usuarios.Count].Id,
                        Brinde = true,
                        Observacao = "",
                        Motivacao = "Indicação de amigo.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Isabela Fernandes",
                        Email = "isabela.fernandes@email.com",
                        Celular = "(11) 99999-0012",
                        EscolaOrigem = "Colégio Adventista",
                        Cidade = "Marília",
                        AnoFormacao = 2021,
                        EixoId = eixos[2 % eixos.Count].EixoId,
                        CursoId = cursos[0].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[2 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[0].TipoIngressoId,
                        UsuarioId = usuarios[2 % usuarios.Count].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Transferência.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Juliana Prado",
                        Email = "juliana.prado@email.com",
                        Celular = "(11) 99999-0013",
                        EscolaOrigem = "Colégio Objetivo",
                        Cidade = "Presidente Prudente",
                        AnoFormacao = 2023,
                        EixoId = eixos[0].EixoId,
                        CursoId = cursos[1 % cursos.Count].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[0].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[1 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[0].Id,
                        Brinde = true,
                        Observacao = "",
                        Motivacao = "Vestibular.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Leonardo Alves",
                        Email = "leonardo.alves@email.com",
                        Celular = "(11) 99999-0014",
                        EscolaOrigem = "Colégio Anglo",
                        Cidade = "Barretos",
                        AnoFormacao = 2020,
                        EixoId = eixos[1 % eixos.Count].EixoId,
                        CursoId = cursos[2 % cursos.Count].CursoId,
                        Turno = Periodo.Noturno,
                        Modalidade = Titulacao.Tecnologo,
                        StatusMatriculaId = statusList[1 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[2 % tiposIngresso.Count].TipoIngressoId,
                        UsuarioId = usuarios[1 % usuarios.Count].Id,
                        Brinde = false,
                        Observacao = "",
                        Motivacao = "Indicação de professor.",
                        DataMatricula = DateTime.Now
                    },
                    new Matricula
                    {
                        NomeCompleto = "Marina Duarte",
                        Email = "marina.duarte@email.com",
                        Celular = "(11) 99999-0015",
                        EscolaOrigem = "Colégio Bandeirantes",
                        Cidade = "Botucatu",
                        AnoFormacao = 2021,
                        EixoId = eixos[2 % eixos.Count].EixoId,
                        CursoId = cursos[0].CursoId,
                        Turno = Periodo.Matutino,
                        Modalidade = Titulacao.Tecnico,
                        StatusMatriculaId = statusList[2 % statusList.Count].StatusMatriculaId,
                        TipoIngressoId = tiposIngresso[0].TipoIngressoId,
                        UsuarioId = usuarios[2 % usuarios.Count].Id,
                        Brinde = true,
                        Observacao = "",
                        Motivacao = "Feira de profissões.",
                        DataMatricula = DateTime.Now
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}