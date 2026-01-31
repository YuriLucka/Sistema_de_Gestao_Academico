using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAA.Data;
using CAA.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CAA.Controllers
{
    [Authorize(Roles = "Links Úteis, Admin")]
    public class LinkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinkController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var links = await _context.Link.OrderBy(x => x.Nome).ToListAsync();
            return View(links);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Link link)
        {
            if (ModelState.IsValid)
            {
                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var links = await _context.Link.ToListAsync();
            return View("Index", links);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Link link)
        {
            if (ModelState.IsValid)
            {
                _context.Update(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var links = await _context.Link.ToListAsync();
            return View("Index", links);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var link = await _context.Link.FindAsync(id);
            if (link != null)
            {
                _context.Link.Remove(link);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
