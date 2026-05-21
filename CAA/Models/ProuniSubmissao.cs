using System.ComponentModel.DataAnnotations;

namespace CAA.Models
{
    public class ProuniSubmissao
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Nome do Candidato")]
        public string NomeCandidato { get; set; } = string.Empty;

        [Required]
        [StringLength(14)]
        [Display(Name = "CPF")]
        public string CpfCandidato { get; set; } = string.Empty;

        [Display(Name = "Data de Envio")]
        public DateTime DataEnvio { get; set; }

        [StringLength(30)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pendente";

        [StringLength(2000)]
        [Display(Name = "Comentário da Análise")]
        public string? Comentario { get; set; }

        [StringLength(450)]
        public string? AnalistaId { get; set; }

        [Display(Name = "Data da Análise")]
        public DateTime? DataAnalise { get; set; }

        public ICollection<ProuniDocumentoAnexado> Documentos { get; set; } = new List<ProuniDocumentoAnexado>();
    }
}
