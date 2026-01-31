using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("ContatoProfessor")]
    public class ContatoProfessor : IValidatableObject
    {
        public int ContatoProfessorId { get; set; }

        [Display(Name = "Nome")]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Nome) &&
                string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "É necessário preencher ao menos um campo para cadastrar o professor.",
                    new[] { "Nome", "Email" }
                );
            }
        }
    }
}
