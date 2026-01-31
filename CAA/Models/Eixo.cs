using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Eixo")]
    public class Eixo
    {
        [Key]
        public int EixoId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Eixo é obrigatório.")]
        [Display(Name = "Nome do Eixo")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        [ValidateNever]
        public virtual ICollection<CoordenadorEixo> CoordenadorEixo { get; set; } = null!;
    }
}