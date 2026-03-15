using AgendaPro.Models;
using AgendaPro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfissionaisController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfissionaisController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = _userManager.Users.ToList();
            var profissionais = new List<ApplicationUser>();

            foreach (var usuario in usuarios)
            {
                if (await _userManager.IsInRoleAsync(usuario, "Profissional"))
                {
                    profissionais.Add(usuario);
                }
            }

            return View(profissionais.OrderBy(p => p.NomeCompleto).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfissionalViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (string.IsNullOrWhiteSpace(model.Senha))
            {
                ModelState.AddModelError("Senha", "A senha é obrigatória.");
                return View(model);
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Já existe um usuário com este e-mail.");
                return View(model);
            }

            var profissional = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                TipoUsuario = "Profissional",
                Especialidade = model.Especialidade,
                Telefone = model.Telefone,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(profissional, model.Senha);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            await _userManager.AddToRoleAsync(profissional, "Profissional");

            TempData["Sucesso"] = "Profissional cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var profissional = await _userManager.FindByIdAsync(id);
            if (profissional == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(profissional, "Profissional"))
                return NotFound();

            return View(profissional);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var profissional = await _userManager.FindByIdAsync(id);
            if (profissional == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(profissional, "Profissional"))
                return NotFound();

            var model = new ProfissionalViewModel
            {
                Id = profissional.Id,
                NomeCompleto = profissional.NomeCompleto ?? string.Empty,
                Email = profissional.Email ?? string.Empty,
                Especialidade = profissional.Especialidade,
                Telefone = profissional.Telefone
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProfissionalViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var profissional = await _userManager.FindByIdAsync(id);
            if (profissional == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(profissional, "Profissional"))
                return NotFound();

            var emailEmUso = await _userManager.FindByEmailAsync(model.Email);
            if (emailEmUso != null && emailEmUso.Id != profissional.Id)
            {
                ModelState.AddModelError("Email", "Já existe um usuário com este e-mail.");
                return View(model);
            }

            profissional.NomeCompleto = model.NomeCompleto;
            profissional.Email = model.Email;
            profissional.UserName = model.Email;
            profissional.NormalizedEmail = model.Email.ToUpper();
            profissional.NormalizedUserName = model.Email.ToUpper();
            profissional.Especialidade = model.Especialidade;
            profissional.Telefone = model.Telefone;

            var result = await _userManager.UpdateAsync(profissional);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            TempData["Sucesso"] = "Profissional atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var profissional = await _userManager.FindByIdAsync(id);
            if (profissional == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(profissional, "Profissional"))
                return NotFound();

            return View(profissional);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var profissional = await _userManager.FindByIdAsync(id);
            if (profissional == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(profissional, "Profissional"))
                return NotFound();

            var result = await _userManager.DeleteAsync(profissional);

            if (!result.Succeeded)
            {
                TempData["Erro"] = "Não foi possível excluir o profissional.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Sucesso"] = "Profissional excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}
