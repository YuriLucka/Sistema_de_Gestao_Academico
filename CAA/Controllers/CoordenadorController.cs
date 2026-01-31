using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Controllers
{
    [Authorize(Roles = "Parametros, Admin")]
    public class CoordenadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordenadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coordenador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coordenador.ToListAsync());
        }

        // GET: Coordenador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenador
                .FirstOrDefaultAsync(m => m.CoordenadorId == id);
            if (coordenador == null)
            {
                return NotFound();
            }

            return View(coordenador);
        }

        // GET: Coordenador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coordenador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoordenadorId,Nome")] Coordenador coordenador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordenador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordenador);
        }

        // GET: Coordenador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenador.FindAsync(id);
            if (coordenador == null)
            {
                return NotFound();
            }
            return View(coordenador);
        }

        // POST: Coordenador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoordenadorId,Nome")] Coordenador coordenador)
        {
            if (id != coordenador.CoordenadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordenador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordenadorExists(coordenador.CoordenadorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coordenador);
        }

        // GET: Coordenador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenador
                .FirstOrDefaultAsync(m => m.CoordenadorId == id);
            if (coordenador == null)
            {
                return NotFound();
            }

            return View(coordenador);
        }

        // POST: Coordenador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Remove vínculos CoordenadorEixo antes de excluir Coordenador
            var coordenadorEixos = _context.CoordenadorEixo
                .Where(ce => ce.CoordenadorId == id);
            _context.CoordenadorEixo.RemoveRange(coordenadorEixos);

            var coordenador = await _context.Coordenador.FindAsync(id);
            if (coordenador != null)
            {
                _context.Coordenador.Remove(coordenador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordenadorExists(int id)
        {
            return _context.Coordenador.Any(e => e.CoordenadorId == id);
        }
    }
}
