using System.ComponentModel.DataAnnotations;

namespace AgendaPro.ViewModels
{
    public class ProfissionalViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Especialidade")]
        public string? Especialidade { get; set; }

        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string? ConfirmarSenha { get; set; }
    }
}
