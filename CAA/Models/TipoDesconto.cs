using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{

    [Table("TipoDesconto")]
    public class TipoDesconto
    {
        [Key]
        public int TipoDescontoId { get; set; }

        [Required(ErrorMessage = "O campo Tipo de Desconto é obrigatório.")]
        [Display(Name = "Desconto")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Valor Padrão")]
        public decimal? ValorPadrao { get; set; }

        [Display(Name = "Tipo")]
        public TipoDescontoValor? TipoDescontoValor { get; set; }
    }
}
