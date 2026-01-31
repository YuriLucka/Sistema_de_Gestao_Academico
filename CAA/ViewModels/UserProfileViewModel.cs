namespace CAA.ViewModels
{
    public class UserProfileViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string? FotoPerfilBase64 { get; set; }
        public string Departamento { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
