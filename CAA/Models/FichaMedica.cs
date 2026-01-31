using System.ComponentModel.DataAnnotations;

namespace CAA.Models
{
    public class FichaMedica
    {
        public int FichaMedicaId { get; set; }

        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome não pode exceder 200 caracteres")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "RA")]
        public string? RA { get; set; }

        // Problemas crônicos - checkboxes
        [Display(Name = "Diabetes")]
        public bool Diabetes { get; set; }
        [Display(Name = "Crise Convulsiva")]
        public bool CriseConvulsiva { get; set; }
        [Display(Name = "Taquicardia")]
        public bool Taquicardia { get; set; }
        [Display(Name = "Bronquite")]
        public bool Bronquite { get; set; }
        [Display(Name = "Rinite")]
        public bool Rinite { get; set; }
        [Display(Name = "Sinusite")]
        public bool Sinusite { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Display(Name = "Outros Problemas Crônicos")]
        public string? OutrosProblemasCronicos { get; set; }

        // Medicamento
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Display(Name = "Medicamentos de Uso Contínuo")]
        public string? Medicamentos { get; set; }

        // Alergias
        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Alergia a Medicamentos")]
        public string? AlergiaMedicamentos { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Alergia a Insetos")]
        public string? AlergiaInsetos { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Alergia a Alimentos")]
        public string? AlergiaAlimentos { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        [Display(Name = "Outras Alergias")]
        public string? OutrasAlergias { get; set; }

        // Tratamento
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Display(Name = "Tratamento Médico")]
        public string? TratamentoMedico{ get; set; }

        // Convênio médico
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Display(Name = "Convênio Médico")]
        public string? ConvenioMedico { get; set; }

        // Deficiências - checkboxes
        [Display(Name = "Cegueira")]
        public bool DefCegueira { get; set; }
        [Display(Name = "Baixa Visão")]
        public bool DefBaixaVisao { get; set; }
        [Display(Name = "Surdocegueira")]
        public bool DefSurdocegueira { get; set; }
        [Display(Name = "Surdez")]
        public bool DefSurdez { get; set; }
        [Display(Name = "Deficiência Auditiva")]
        public bool DefAuditiva { get; set; }
        [Display(Name = "Deficiência Física")]
        public bool DefFisica { get; set; }
        [Display(Name = "Deficiência Múltipla")]
        public bool DefMultipla { get; set; }
        [Display(Name = "Deficiência Intelectual")]
        public bool DefIntelectual { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        [Display(Name = "Outras Deficiências")]
        public string? OutrasDeficiencias{ get; set; }

        // Outras informações
        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres")]
        [Display(Name = "Informações Adicionais")]
        public string? InformacoesAdicionais { get; set; }

        // Contatos - 3 contatos com nome e telefone
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Display(Name = "Nome do Contato 1")]
        public string? NomeContato1 { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Display(Name = "Telefone do Contato 1")]
        public string? Contato1 { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Display(Name = "Nome do Contato 2")]
        public string? NomeContato2 { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Display(Name = "Telefone do Contato 2")]
        public string? Contato2 { get; set; }

        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Display(Name = "Nome do Contato 3")]
        public string? NomeContato3 { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Display(Name = "Telefone do Contato 3")]
        public string? Contato3 { get; set; }

        // Propriedades de controle (opcionais)
        [Display(Name = "Data de Preenchimento")]
        public DateTime DataPreenchimento { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
