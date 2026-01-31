using System.ComponentModel.DataAnnotations;

namespace CAA.Models
{
    public enum TipoDescontoValor
    {
        [Display(Name = "Valor Fixo")]
        ValorFixo,
        [Display(Name = "Porcentagem")]
        Porcentagem
    }
}
