using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CAA.Data;
using CAA.Models;

namespace CAA.Controllers
{
    public class ContatoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContatoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contato
        public async Task<IActionResult> Index(string filtro, string listaSelecionada = "contatosInternos")
        {
            var internosQuery = _context.ContatoInterno
              .Include(c => c.Departamento)
              .OrderBy(c => c.Nome)
              .AsQueryable();

            var externosQuery = _context.ContatoExterno
                .OrderBy(c => c.Nome)
                .AsQueryable();

            var professoresQuery = _context.ContatoProfessor
                .OrderBy(c => c.Nome)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroLower = filtro.ToLower();
                internosQuery = internosQuery.Where(c =>
                    (c.Nome != null && c.Nome.ToLower().Contains(filtroLower)) ||
                    (c.Email != null && c.Email.ToLower().Contains(filtroLower)) ||
                    (c.Telefone != null && c.Telefone.ToLower().Contains(filtroLower)) ||
                    (c.Departamento != null && c.Departamento.Nome.ToLower().Contains(filtroLower))
                );
                externosQuery = externosQuery.Where(c =>
                    (c.Nome != null && c.Nome.ToLower().Contains(filtroLower)) ||
                    (c.Email != null && c.Email.ToLower().Contains(filtroLower)) ||
                    (c.Telefone != null && c.Telefone.ToLower().Contains(filtroLower))
                );
                professoresQuery = professoresQuery.Where(c =>
                    (c.Nome != null && c.Nome.ToLower().Contains(filtroLower)) ||
                    (c.Email != null && c.Email.ToLower().Contains(filtroLower))
                );
            }

            ViewBag.ContatosInternos = await internosQuery.OrderBy(c => c.Nome).ToListAsync();
            ViewBag.ContatosExternos = await externosQuery.OrderBy(c => c.Nome).ToListAsync();
            ViewBag.ContatosProfessores = await professoresQuery.OrderBy(c => c.Nome).ToListAsync();
            ViewBag.ListaSelecionada = listaSelecionada;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_ListasContatos");

            return View();
        }
    }
}
