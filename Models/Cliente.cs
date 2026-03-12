using System.ComponentModel.DataAnnotations;

namespace AgendaPro.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }
    }
}