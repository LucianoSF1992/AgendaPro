using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProfissionaisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}