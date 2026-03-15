using AgendaPro.Data;
using AgendaPro.Models;
using AgendaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Controllers
{
    [Authorize]
    public class AgendamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .Include(a => a.Profissional)
                .OrderBy(a => a.Data)
                .ThenBy(a => a.HoraInicio)
                .ToListAsync();

            return View(agendamentos);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new AgendamentoFormViewModel();
            await CarregarCombosAsync(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgendamentoFormViewModel vm)
        {
            await CarregarCombosAsync(vm);

            if (vm.Data.Date < DateTime.Today)
                ModelState.AddModelError(nameof(vm.Data), "Não é permitido agendar em data passada.");

            if (vm.HoraFim <= vm.HoraInicio)
                ModelState.AddModelError(nameof(vm.HoraFim), "A hora final deve ser maior que a hora inicial.");

            bool conflito = await ExisteConflitoHorario(vm.ProfissionalId, vm.Data, vm.HoraInicio, vm.HoraFim);

            if (conflito)
                ModelState.AddModelError(string.Empty, "Já existe um agendamento para esse profissional nesse horário.");

            if (!ModelState.IsValid)
                return View(vm);

            var agendamento = new Agendamento
            {
                ClienteId = vm.ClienteId,
                ServicoId = vm.ServicoId,
                ProfissionalId = vm.ProfissionalId,
                Data = vm.Data.Date,
                HoraInicio = vm.HoraInicio,
                HoraFim = vm.HoraFim,
                Status = vm.Status,
                Observacoes = vm.Observacoes
            };

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Agendamento criado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
                return NotFound();

            var vm = new AgendamentoFormViewModel
            {
                Id = agendamento.Id,
                ClienteId = agendamento.ClienteId,
                ServicoId = agendamento.ServicoId,
                ProfissionalId = agendamento.ProfissionalId,
                Data = agendamento.Data,
                HoraInicio = agendamento.HoraInicio,
                HoraFim = agendamento.HoraFim,
                Status = agendamento.Status,
                Observacoes = agendamento.Observacoes
            };

            await CarregarCombosAsync(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgendamentoFormViewModel vm)
        {
            if (id != vm.Id)
                return NotFound();

            await CarregarCombosAsync(vm);

            if (vm.Data.Date < DateTime.Today)
                ModelState.AddModelError(nameof(vm.Data), "Não é permitido agendar em data passada.");

            if (vm.HoraFim <= vm.HoraInicio)
                ModelState.AddModelError(nameof(vm.HoraFim), "A hora final deve ser maior que a hora inicial.");

            bool conflito = await ExisteConflitoHorario(vm.ProfissionalId, vm.Data, vm.HoraInicio, vm.HoraFim, vm.Id);

            if (conflito)
                ModelState.AddModelError(string.Empty, "Já existe um agendamento para esse profissional nesse horário.");

            if (!ModelState.IsValid)
                return View(vm);

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
                return NotFound();

            agendamento.ClienteId = vm.ClienteId;
            agendamento.ServicoId = vm.ServicoId;
            agendamento.ProfissionalId = vm.ProfissionalId;
            agendamento.Data = vm.Data.Date;
            agendamento.HoraInicio = vm.HoraInicio;
            agendamento.HoraFim = vm.HoraFim;
            agendamento.Status = vm.Status;
            agendamento.Observacoes = vm.Observacoes;

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Agendamento atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .Include(a => a.Profissional)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
                return NotFound();

            return View(agendamento);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Servico)
                .Include(a => a.Profissional)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
                return NotFound();

            return View(agendamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
                return NotFound();

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Agendamento excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarCombosAsync(AgendamentoFormViewModel vm)
        {
            vm.Clientes = await _context.Clientes
                .OrderBy(c => c.Nome)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                })
                .ToListAsync();

            vm.Servicos = await _context.Servicos
                .OrderBy(s => s.Nome)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                })
                .ToListAsync();

            vm.Profissionais = await _context.Users
                .Where(u => u.TipoUsuario == "Profissional")
                .OrderBy(u => u.NomeCompleto)
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.NomeCompleto ?? u.Email ?? "Profissional sem nome"
                })
                .ToListAsync();
        }

        private async Task<bool> ExisteConflitoHorario(
            string profissionalId,
            DateTime data,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            int? ignorarId = null)
        {
            return await _context.Agendamentos.AnyAsync(a =>
                a.ProfissionalId == profissionalId &&
                a.Data.Date == data.Date &&
                (ignorarId == null || a.Id != ignorarId) &&
                horaInicio < a.HoraFim &&
                horaFim > a.HoraInicio
            );
        }
    }
}
