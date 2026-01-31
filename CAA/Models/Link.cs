using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("Link")]
    [Authorize(Roles = "Links Úteis")]
    public class Link
    {
        public int LinkId { get; set; }

        [Required(ErrorMessage = "O campo Nome do Link é obrigatório.")]
        [Display(Name = "Nome do Link")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo URL é obrigatório.")]
        [Display(Name = "URL")]
        [Url]
        [StringLength(2048)]
        public string Url { get; set; } = string.Empty;
    }
}