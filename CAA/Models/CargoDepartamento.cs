using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAA.Models
{
    [Table("CargoDepartamento")]
    public class CargoDepartamento
    {
        [Key]
        public int CargoDepartamentoId { get; set; }

        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }
        [ForeignKey("CargoId")]
        [ValidateNever]
        public Cargo Cargo { get; set; } = null!; 

        [Required(ErrorMessage = "O campo Departamento é obrigatório.")]
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }
        [ForeignKey("DepartamentoId")]
        [ValidateNever]
        public Departamento Departamento { get; set; } = null!;
    }
}
