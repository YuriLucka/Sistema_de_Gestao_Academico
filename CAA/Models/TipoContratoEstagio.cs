using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("TipoContratoEstagio")]
    public class TipoContratoEstagio
    {
        [Key]
        public int TipoContratoEstagioId { get; set; }

        [Required(ErrorMessage = "O campo Tipo de Contrato de Estágio é obrigatório.")]
        [Display(Name = "Tipo de Contrato de Estágio")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
    }

}
