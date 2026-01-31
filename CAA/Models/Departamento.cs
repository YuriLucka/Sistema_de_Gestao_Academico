using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Departamento é obrigatório.")]
        [Display(Name = "Nome do Departamento")]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;
    }
}
