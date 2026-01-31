using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Cargo é obrigatório.")]
        [Display(Name = "Nome do Cargo")]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;
    }
}
