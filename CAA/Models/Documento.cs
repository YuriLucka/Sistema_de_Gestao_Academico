using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Documento")]
    public class Documento
    {
        [Key]
        public int DocumentoId { get; set; }

        [Display(Name = "Nome do Documento")]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [Display(Name = "Descrição")]
        [StringLength(255)]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Arquivo")]
        public byte[] Arquivo { get; set; }
    }
}
