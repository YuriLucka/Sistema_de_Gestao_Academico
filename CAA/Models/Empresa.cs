using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "O campo Razão Social é obrigatório.")]
        [Display(Name = "Razão Social")]
        [StringLength(200)]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Nome Fantasia é obrigatório.")]
        [Display(Name = "Nome Fantasia")]
        [StringLength(200)]
        public string NomeFantasia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        [Display(Name = "CNPJ")]
        [StringLength(18)] // Formato 00.000.000/0000-00
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "CNPJ inválido. Use o formato 00.000.000/0000-00.")]
        public string CNPJ { get; set; } = string.Empty;
    }

}
