using System.ComponentModel.DataAnnotations;

namespace AgendaPro.ViewModels
{
    public class RelatorioViewModel
    {
        [Display(Name = "Data inicial")]
        [DataType(DataType.Date)]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Data final")]
        [DataType(DataType.Date)]
        public DateTime DataFinal { get; set; }

        public int TotalAgendamentos { get; set; }
        public int TotalAgendados { get; set; }
        public int TotalConcluidos { get; set; }
        public int TotalCancelados { get; set; }

        [Display(Name = "Faturamento estimado")]
        public decimal FaturamentoEstimado { get; set; }

        public List<RelatorioServicoViewModel> ServicosMaisAgendados { get; set; } = new();
        public List<RelatorioProfissionalViewModel> DesempenhoProfissionais { get; set; } = new();
    }

    public class RelatorioServicoViewModel
    {
        public int ServicoId { get; set; }
        public string NomeServico { get; set; } = string.Empty;
        public int QuantidadeAgendamentos { get; set; }
        public int QuantidadeConcluidos { get; set; }
        public decimal ValorPotencial { get; set; }
    }

    public class RelatorioProfissionalViewModel
    {
        public string ProfissionalId { get; set; } = string.Empty;
        public string NomeProfissional { get; set; } = string.Empty;
        public int QuantidadeAtendimentos { get; set; }
        public int QuantidadeConcluidos { get; set; }
        public int QuantidadeCancelados { get; set; }
        public decimal FaturamentoEstimado { get; set; }
    }
}