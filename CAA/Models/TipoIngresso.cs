using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("TipoIngresso")]
    public class TipoIngresso
    {
        [Key]
        public int TipoIngressoId { get; set; }

        [Required(ErrorMessage = "O campo Tipo de Ingresso é obrigatório.")]
        [Display(Name = "Tipo de Ingresso")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
    }
}
