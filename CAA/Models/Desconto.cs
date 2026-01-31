using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Desconto")]
    public class Desconto
    {
        [Key]
        public int DescontoId { get; set; }

        [Required(ErrorMessage = "O campo Plano Financeiro é obrigatório.")]
        [Display(Name = "Plano Financeiro")]
        public int PlanoFinanceiroId { get; set; }
        [Display(Name = "Plano Financeiro")]
        [ForeignKey("PlanoFinanceiroId")]
        [ValidateNever]
        public PlanoFinanceiro PlanoFinanceiro { get; set; } = null!;

        [Required(ErrorMessage = "O campo Tipo de Desconto é obrigatório.")]
        [Display(Name = "Desconto")]
        public int TipoDescontoId { get; set; }
        [ForeignKey("TipoDescontoId")]
        [Display(Name = "Desconto")]
        [ValidateNever]
        public virtual TipoDesconto TipoDesconto { get; set; } = null!;

        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Tipo")]
        public TipoDescontoValor TipoDescontoValor { get; set; }
    }
}
