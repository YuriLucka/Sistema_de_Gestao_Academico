using System.ComponentModel.DataAnnotations;

namespace CAA.Models
{
    public class ProuniDocumentoAnexado
    {
        public int Id { get; set; }

        public int SubmissaoId { get; set; }
        public ProuniSubmissao Submissao { get; set; } = null!;

        public int CampoDocumentoId { get; set; }
        public ProuniCampoDocumento CampoDocumento { get; set; } = null!;

        // "Unico", "Frente", "Verso"
        [StringLength(10)]
        public string Lado { get; set; } = "Unico";

        public byte[]? Arquivo { get; set; }

        [StringLength(255)]
        public string? NomeArquivo { get; set; }

        [StringLength(100)]
        public string? ContentType { get; set; }

        // "NaoEnviado", "Aguardando", "Aprovado", "Reprovado"
        [StringLength(20)]
        public string Status { get; set; } = "NaoEnviado";

        [StringLength(1000)]
        public string? Comentario { get; set; }
    }
}
