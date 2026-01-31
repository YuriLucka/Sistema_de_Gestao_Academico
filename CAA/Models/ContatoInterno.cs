using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("ContatoInterno")]
    public class ContatoInterno : IValidatableObject
    {
        public int ContatoInternoId { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Display(Name = "Departamento")]
        public int? DepartamentoId { get; set; }

        [ForeignKey("DepartamentoId")]
        [ValidateNever]
        public Departamento Departamento { get; set; } = null!;

        [Display(Name = "Telefone")]
        [StringLength(30)]
        public string? Telefone { get; set; }

        [Display(Name = "Ramal")]
        public int? Ramal { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Nome) &&
                string.IsNullOrWhiteSpace(Telefone) &&
                Ramal == null &&  
                string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "É necessário preencher ao menos um campo para cadastrar o contato.",
                    new[] { "Nome", "Telefone", "Ramal", "Email" }
                );
            }
        }
    }
}
