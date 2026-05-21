using System.ComponentModel.DataAnnotations;

namespace CAA.Models
{
    public class ProuniCampoDocumento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200)]
        [Display(Name = "Nome do Documento")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Tem Frente e Verso")]
        public bool TemFrenteVerso { get; set; }

        [Display(Name = "Obrigatório")]
        public bool Obrigatorio { get; set; } = true;

        [Display(Name = "Ordem de Exibição")]
        public int Ordem { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        public ICollection<ProuniDocumentoAnexado> Documentos { get; set; } = new List<ProuniDocumentoAnexado>();
    }
}
