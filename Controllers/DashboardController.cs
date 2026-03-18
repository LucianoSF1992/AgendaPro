using System.Security.Claims;
using AgendaPro.Data;
using AgendaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Controllers
{
    [Authorize(Roles = "Admin,Profissional")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? profissionalId)
        {
            var hoje = DateTime.Today;
            var agora = DateTime.Now;

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ehAdmin = User.IsInRole("Admin");

            var vm = new DashboardViewModel
            {
                DataReferencia = hoje
            };

            vm.Profissionais = await _context.Users
                .Where(u => u.TipoUsuario == "Profissional")
                .OrderBy(u => u.NomeCompleto)
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.NomeCompleto ?? u.Email ?? "Profissional sem nome"
                })
                .ToListAsync();

            IQueryable<AgendaPro.Models.Agendamento> query = _context.Agendamentos
                .AsNoTracking()
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .Include(a => a.Profissional)
                .Where(a => a.Data.Date == hoje);

            if (ehAdmin)
            {
                vm.ProfissionalId = profissionalId;

                if (!string.IsNullOrWhiteSpace(profissionalId))
                {
                    query = query.Where(a => a.ProfissionalId == profissionalId);
                }
            }
            else
            {
                vm.ProfissionalId = usuarioId;

                if (!string.IsNullOrWhiteSpace(usuarioId))
                {
                    query = query.Where(a => a.ProfissionalId == usuarioId);
                }
            }

            var agendamentosHoje = await query
                .OrderBy(a => a.HoraInicio)
                .ToListAsync();

            vm.AgendamentosHoje = agendamentosHoje;

            vm.TotalAgendamentosHoje = agendamentosHoje.Count;
            vm.TotalAgendadosHoje = agendamentosHoje.Count(a => a.Status == "Agendado");
            vm.TotalConcluidosHoje = agendamentosHoje.Count(a => a.Status == "Concluído" || a.Status == "Concluido");
            vm.TotalCanceladosHoje = agendamentosHoje.Count(a => a.Status == "Cancelado");

            vm.ProximosAtendimentos = agendamentosHoje
                .Where(a => a.Data.Date.Add(a.HoraInicio) >= agora && a.Status != "Cancelado")
                .OrderBy(a => a.HoraInicio)
                .ToList();

            if (ehAdmin)
            {
                vm.TotalClientes = await _context.Clientes.CountAsync();
                vm.TotalServicos = await _context.Servicos.CountAsync();
                vm.TotalProfissionais = await _context.Users.CountAsync(u => u.TipoUsuario == "Profissional");
            }

            return View(vm);
        }
    }
}