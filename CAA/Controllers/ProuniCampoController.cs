using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProuniCampoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProuniCampoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProuniCampoDocumentos
                .OrderBy(c => c.Ordem)
                .ThenBy(c => c.Nome)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            var proximaOrdem = _context.ProuniCampoDocumentos.Any()
                ? _context.ProuniCampoDocumentos.Max(c => c.Ordem) + 1
                : 1;
            ViewBag.ProximaOrdem = proximaOrdem;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProuniCampoDocumento campo)
        {
            if (ModelState.IsValid)
            {
                _context.ProuniCampoDocumentos.Add(campo);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Campo cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(campo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var campo = await _context.ProuniCampoDocumentos.FindAsync(id);
            if (campo == null) return NotFound();
            return View(campo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProuniCampoDocumento campo)
        {
            if (id != campo.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(campo);
                await _context.SaveChangesAsync();
                TempData["Sucesso"] = "Campo atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(campo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var campo = await _context.ProuniCampoDocumentos
                .Include(c => c.Documentos)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (campo == null) return NotFound();
            return View(campo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campo = await _context.ProuniCampoDocumentos.FindAsync(id);
            if (campo != null)
            {
                _context.ProuniCampoDocumentos.Remove(campo);
                await _context.SaveChangesAsync();
            }
            TempData["Sucesso"] = "Campo removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
