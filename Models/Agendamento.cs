using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgendaPro.Models;


namespace AgendaPro.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        [Required]
        [Display(Name = "Serviço")]
        public int ServicoId { get; set; }
        public Servico Servico { get; set; } = null!;

        [Required]
        [Display(Name = "Profissional")]
        public string ProfissionalId { get; set; } = null!;
        public ApplicationUser Profissional { get; set; } = null!;


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Hora Inicial")]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [Display(Name = "Hora Final")]
        public TimeSpan HoraFim { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Agendado";

        [StringLength(500)]
        public string? Observacoes { get; set; }
    }
}
