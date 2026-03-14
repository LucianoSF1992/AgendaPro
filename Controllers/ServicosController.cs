using AgendaPro.Data;
using AgendaPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? busca)
        {
            var query = _context.Servicos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                query = query.Where(s => s.Nome.Contains(busca));
            }

            var servicos = await query
                .OrderBy(s => s.Nome)
                .ToListAsync();

            ViewBag.Busca = busca;
            return View(servicos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servico servico)
        {
            if (!ModelState.IsValid)
                return View(servico);

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Serviço cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servicos.FirstOrDefaultAsync(s => s.Id == id);
            if (servico == null) return NotFound();

            return View(servico);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return NotFound();

            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servico servico)
        {
            if (id != servico.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(servico);

            _context.Update(servico);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Serviço atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servicos.FirstOrDefaultAsync(s => s.Id == id);
            if (servico == null) return NotFound();

            return View(servico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return NotFound();

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Serviço excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}
