using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    public enum Titulacao
    {
        [Display(Name = "Técnico")]
        Tecnico,

        [Display(Name = "Tecnólogo")]
        Tecnologo,

        [Display(Name = "Bacharelado")]
        Bacharelado,

        [Display(Name = "Licenciatura")]
        Licenciatura,

        [Display(Name = "Especialização")]
        Especializacao,

        [Display(Name = "MBA")]
        MBA,

        [Display(Name = "Mestrado")]
        Mestrado,

        [Display(Name = "Doutorado")]
        Doutorado,

        [Display(Name = "Pós-Doutorado")]
        PosDoutorado
    }

    [Table("Cursos")]
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Curso é obrigatório.")]
        [StringLength(150)]
        [Display(Name = "Nome do Curso")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Titulação é obrigatório.")]
        [Display(Name = "Titulação")]
        public Titulacao Titulacao { get; set; }

        [Display(Name = "Eixo")]
        public int? EixoId { get; set; }
        [ForeignKey("EixoId")]
        [ValidateNever]
        public virtual Eixo Eixo { get; set; } = null!;

        [Required(ErrorMessage = "O campo Quantidade de Semestres é obrigatório.")]
        [Display(Name = "Quantidade de Semestres")]
        public int QtdSemestres { get; set; }

        [Display(Name = "Documento Anexado")]
        public byte[]? Documento { get; set; }

        [ValidateNever]
        public virtual ICollection<PlanoFinanceiro> PlanosFinanceiros { get; set; } = new List<PlanoFinanceiro>();
    }
}
