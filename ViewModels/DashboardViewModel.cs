using AgendaPro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendaPro.ViewModels
{
    public class DashboardViewModel
    {
        public DateTime DataReferencia { get; set; } = DateTime.Today;

        public string? ProfissionalId { get; set; }

        public int TotalAgendamentosHoje { get; set; }
        public int TotalAgendadosHoje { get; set; }
        public int TotalConcluidosHoje { get; set; }
        public int TotalCanceladosHoje { get; set; }

        public int TotalClientes { get; set; }
        public int TotalServicos { get; set; }
        public int TotalProfissionais { get; set; }

        public List<SelectListItem> Profissionais { get; set; } = new();
        public List<Agendamento> AgendamentosHoje { get; set; } = new();
        public List<Agendamento> ProximosAtendimentos { get; set; } = new();
    }
}