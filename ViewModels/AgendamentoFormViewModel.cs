using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendaPro.ViewModels
{
    public class AgendamentoFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required]
        [Display(Name = "Serviço")]
        public int ServicoId { get; set; }

        [Required]
        [Display(Name = "Profissional")]
        public string ProfissionalId { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Hora Inicial")]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [Display(Name = "Hora Final")]
        public TimeSpan HoraFim { get; set; }

        public string Status { get; set; } = "Agendado";

        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        public List<SelectListItem> Clientes { get; set; } = new();
        public List<SelectListItem> Servicos { get; set; } = new();
        public List<SelectListItem> Profissionais { get; set; } = new();
    }
}
