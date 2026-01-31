using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Estagio")]
    public class Estagio
    {
        [Key]
        public int EstagioId { get; set; }

        [Required(ErrorMessage = "O campo RA é obrigatório.")]
        [Display(Name = "RA")]
        public string RA { get; set; } = string.Empty; // Registro do aluno

        [Required(ErrorMessage = "O campo Nome do Aluno é obrigatório.")]
        [Display(Name = "Nome do Aluno")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Curso é obrigatório.")]
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        [ValidateNever]
        public virtual Curso Curso { get; set; } = null!;

        [Required(ErrorMessage = "O campo Tipo de Contrato de Estágio é obrigatório.")]
        [Display(Name = "Tipo de Contrato de Estágio")]
        public int TipoContratoEstagioId { get; set; }
        [ForeignKey("TipoContratoEstagioId")]
        [ValidateNever]
        public virtual TipoContratoEstagio TipoContratoEstagio { get; set; } = null!;

        [Required(ErrorMessage = "O campo Empresa é obrigatório.")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        [ForeignKey("EmpresaId")]
        [ValidateNever]
        public virtual Empresa Empresa { get; set; } = null!;

        [Required(ErrorMessage = "O campo Integradora é obrigatório.")]
        [Display(Name = "Integradora")]
        public string Integradora { get; set; } = string.Empty;

        [Display(Name = "Vigência Início")]
        [DataType(DataType.Date)]
        public DateTime? VigenciaInicio { get; set; }

        [Display(Name = "Vigência Término")]
        [DataType(DataType.Date)]
        public DateTime? VigenciaTermino { get; set; }

        [Required(ErrorMessage = "O campo Apólice é obrigatório.")]
        [Display(Name = "Apólice")]
        public string Apolice { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Seguradora é obrigatório.")]
        [Display(Name = "Seguradora")]
        public string Seguradora { get; set; } = string.Empty;
    }
}
