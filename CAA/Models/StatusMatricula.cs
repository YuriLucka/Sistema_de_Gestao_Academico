using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("StatusMatricula")]
    public class StatusMatricula
    {
        [Key]
        public int StatusMatriculaId { get; set; }

        [Required(ErrorMessage = "O campo Status da Matrícula é obrigatório.")]
        [Display(Name = "Status da Matrícula")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
    }
}
