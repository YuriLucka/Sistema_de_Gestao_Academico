using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("CoordenadorEixo")]
    public class CoordenadorEixo
    {
        [Key]
        public int CoordenadorEixoId { get; set; }

        [Required(ErrorMessage = "O campo Coordenador é obrigatório.")]
        [Display(Name = "Coordenador")]
        public int CoordenadorId { get; set; }
        [ForeignKey("CoordenadorId")]
        [ValidateNever]
        public Coordenador Coordenador { get; set; } = null!;

        [Required(ErrorMessage = "O campo Eixo é obrigatório.")]
        [Display(Name = "Eixo")]
        public int EixoId { get; set; }
        [ForeignKey("EixoId")]
        [ValidateNever]
        public Eixo Eixo { get; set; } = null!;
    }
}
