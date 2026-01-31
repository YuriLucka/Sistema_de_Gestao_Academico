using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAA.Models
{
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; } = string.Empty;

        [Display(Name = "Foto de Perfil")]
        public byte[]? FotoPerfil { get; set; }

        [Required(ErrorMessage = "O campo Departamento é obrigatório.")]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}
