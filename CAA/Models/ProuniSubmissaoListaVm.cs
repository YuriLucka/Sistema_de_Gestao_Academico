namespace CAA.Models
{
    public class ProuniSubmissaoListaVm
    {
        public int Id { get; set; }
        public string NomeCandidato { get; set; } = string.Empty;
        public string CpfCandidato { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TotalDocs { get; set; }
        public int DocsEnviados { get; set; }
    }
}
