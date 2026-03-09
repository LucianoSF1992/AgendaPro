using Microsoft.AspNetCore.Identity;

namespace AgendaPro.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NomeCompleto { get; set; }

        public string? TipoUsuario { get; set; }
        // Admin ou Profissional

        public string? Especialidade { get; set; }

        public string? Telefone { get; set; }
    }
}