using CAA.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Mensagem")]
public class Mensagem
{
    public string MensagemId { get; set; } // GUID da mensagem
    public string Conteudo { get; set; }
    public string RemetenteId { get; set; } // Id do IdentityUser
    public string DestinatarioId { get; set; } // Id do IdentityUser
    public DateTime DataEnvio { get; set; }
    public bool Lida { get; set; } // Indica se a mensagem foi lida
}
