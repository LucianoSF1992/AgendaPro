using System.ComponentModel.DataAnnotations;

namespace AgendaPro.Models
{
    public class Servico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, 1440, ErrorMessage = "Informe uma duração válida em minutos.")]
        [Display(Name = "Duração (minutos)")]
        public int DuracaoEmMinutos { get; set; }

        [Required(ErrorMessage = "O valor padrão é obrigatório.")]
        [Range(0, 999999.99, ErrorMessage = "Informe um valor válido.")]
        [Display(Name = "Valor padrão")]
        public decimal ValorPadrao { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }
    }
}
