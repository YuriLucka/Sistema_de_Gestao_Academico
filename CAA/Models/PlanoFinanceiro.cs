using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CAA.Models
{
    public enum TipoPlanoFinanceiro
    {
        [Display(Name = "Plano Normal")]
        PlanoNormal,
        [Display(Name = "Plano Estendido")]
        PlanoEstendido
    }

    [Table("PlanoFinanceiro")]
    public class PlanoFinanceiro
    {
        [Key]
        public int PlanoFinanceiroId { get; set; }

        [Required(ErrorMessage = "O campo Curso é obrigatório.")]
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        [ValidateNever]
        public virtual Curso Curso { get; set; } = null!;

        [Required(ErrorMessage = "O campo Tipo do Plano Financeiro é obrigatório.")]
        [Display(Name = "Tipo do Plano Financeiro")]
        public TipoPlanoFinanceiro TipoPlanoFinanceiro { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Matutino")]
        public bool Matutino { get; set; }

        [Display(Name = "Noturno")]
        public bool Noturno { get; set; }

        [ValidateNever]
        public virtual ICollection<Desconto> Descontos { get; set; } = new List<Desconto>();
    }
}
