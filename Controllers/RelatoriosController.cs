using AgendaPro.Data;
using AgendaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? dataInicial, DateTime? dataFinal)
        {
            var hoje = DateTime.Today;

            var inicio = dataInicial?.Date ?? new DateTime(hoje.Year, hoje.Month, 1);
            var fim = dataFinal?.Date ?? hoje;

            if (inicio > fim)
            {
                TempData["Erro"] = "A data inicial não pode ser maior que a data final.";
                inicio = new DateTime(hoje.Year, hoje.Month, 1);
                fim = hoje;
            }

            var agendamentos = await _context.Agendamentos
                .AsNoTracking()
                .Include(a => a.Servico)
                .Include(a => a.Profissional)
                .Where(a => a.Data.Date >= inicio && a.Data.Date <= fim)
                .ToListAsync();

            var vm = new RelatorioViewModel
            {
                DataInicial = inicio,
                DataFinal = fim,
                TotalAgendamentos = agendamentos.Count,
                TotalAgendados = agendamentos.Count(a => a.Status == "Agendado"),
                TotalConcluidos = agendamentos.Count(a => a.Status == "Concluído" || a.Status == "Concluido"),
                TotalCancelados = agendamentos.Count(a => a.Status == "Cancelado")
            };

            vm.FaturamentoEstimado = agendamentos
                .Where(a => a.Status == "Concluído" || a.Status == "Concluido")
                .Sum(a => a.Servico?.ValorPadrao ?? 0m);

            vm.ServicosMaisAgendados = agendamentos
                .GroupBy(a => new
                {
                    a.ServicoId,
                    NomeServico = a.Servico.Nome
                })
                .Select(g => new RelatorioServicoViewModel
                {
                    ServicoId = g.Key.ServicoId,
                    NomeServico = g.Key.NomeServico,
                    QuantidadeAgendamentos = g.Count(),
                    QuantidadeConcluidos = g.Count(x => x.Status == "Concluído" || x.Status == "Concluido"),
                    ValorPotencial = g
                        .Where(x => x.Status == "Concluído" || x.Status == "Concluido")
                        .Sum(x => x.Servico.ValorPadrao)
                })
                .OrderByDescending(x => x.QuantidadeAgendamentos)
                .ThenBy(x => x.NomeServico)
                .ToList();

            vm.DesempenhoProfissionais = agendamentos
                .GroupBy(a => new
                {
                    a.ProfissionalId,
                    NomeProfissional = a.Profissional.NomeCompleto ?? a.Profissional.Email ?? "Profissional sem nome"
                })
                .Select(g => new RelatorioProfissionalViewModel
                {
                    ProfissionalId = g.Key.ProfissionalId,
                    NomeProfissional = g.Key.NomeProfissional,
                    QuantidadeAtendimentos = g.Count(),
                    QuantidadeConcluidos = g.Count(x => x.Status == "Concluído" || x.Status == "Concluido"),
                    QuantidadeCancelados = g.Count(x => x.Status == "Cancelado"),
                    FaturamentoEstimado = g
                        .Where(x => x.Status == "Concluído" || x.Status == "Concluido")
                        .Sum(x => x.Servico.ValorPadrao)
                })
                .OrderByDescending(x => x.QuantidadeAtendimentos)
                .ThenBy(x => x.NomeProfissional)
                .ToList();

            return View(vm);
        }
    }
}