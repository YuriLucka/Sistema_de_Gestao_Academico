using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Coordenador")]
    public class Coordenador
    {
        [Key]
        public int CoordenadorId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Coordenador é obrigatório.")]
        [Display(Name = "Nome do Coordenador")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;
    }

}