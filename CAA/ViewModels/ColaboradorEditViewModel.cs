using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAA.ViewModels
{
    public class ColaboradorEditViewModel
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        public string Sobrenome { get; set; } = string.Empty;

        public IFormFile? FotoPerfil { get; set; }

        [Required(ErrorMessage = "O campo Departamento é obrigatório.")]
        public string Departamento { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        // Roles para seleção
        public List<string> SelectedRoles { get; set; } = new();
        public List<SelectListItem> AllRoles { get; set; } = new();

        // Indica se o usuário está bloqueado
        public bool IsBlocked { get; set; } = false;

        // Novo campo para ativar/inativar
        public bool Ativo { get; set; } = true;
    }
}
