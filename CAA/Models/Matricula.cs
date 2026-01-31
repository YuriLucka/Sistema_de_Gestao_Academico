using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    public enum Periodo
    {
        [Display(Name = "Matutino")]
        Matutino = 0,

        [Display(Name = "Noturno")]
        Noturno = 1
    }

    [Table("Matricula")]
    public class Matricula
    {
        [Key]
        [Display(Name = "ID da Matrícula")]
        public int MatriculaId { get; set; }

        [Display(Name = "Data da Matrícula")]
        public DateTime? DataMatricula { get; set; }

        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Celular é obrigatório.")]
        [Display(Name = "Celular")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Origem é obrigatório.")]
        [Display(Name = "Escola de Origem")]
        public string EscolaOrigem { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = string.Empty;

        [Display(Name = "Ano de Formação")]
        public int? AnoFormacao { get; set; }

        [Display(Name = "Eixo")]
        public int EixoId { get; set; }
        [ForeignKey("EixoId")]
        [ValidateNever]
        public virtual Eixo Eixo { get; set; } = null!;

        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        [ValidateNever]
        public virtual Curso Curso { get; set; } = null!;

        [Display(Name = "Turno")]
        public Periodo Turno { get; set; }

        [Display(Name = "Modalidade")]
        public Titulacao Modalidade { get; set; }

        [Display(Name = "Status da Matrícula")]
        public int StatusMatriculaId { get; set; }
        [ForeignKey("StatusMatriculaId")]
        [ValidateNever]
        public virtual StatusMatricula StatusMatricula { get; set; } = null!;

        [Display(Name = "Tipo de Ingresso")]
        public int TipoIngressoId { get; set; }
        [ForeignKey("TipoIngressoId")]
        [ValidateNever]
        public virtual TipoIngresso TipoIngresso { get; set; } = null!;

        [Display(Name = "Instituição de Transferência")]
        public string? InstituicaoTransferencia { get; set; }

        [Display(Name = "Usuário Atendente")]
        public string UsuarioId { get; set; } = string.Empty;
        [ForeignKey("UsuarioId")]
        [ValidateNever]
        public virtual Usuario Atendente { get; set; } = null!;

        [Display(Name = "Brinde")]
        public bool Brinde { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; } = string.Empty;

        [Display(Name = "Motivação")]
        public string Motivacao { get; set; } = string.Empty;
    }
}
