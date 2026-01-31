using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAA.Controllers
{
    [Authorize(Roles = "Parametros, Admin")]
    public class ParametrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
