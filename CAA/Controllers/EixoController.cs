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
    public class EixoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EixoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eixo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eixo.ToListAsync());
        }

        // GET: Eixo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eixo = await _context.Eixo
                .FirstOrDefaultAsync(m => m.EixoId == id);
            if (eixo == null)
            {
                return NotFound();
            }

            return View(eixo);
        }

        // GET: Eixo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eixo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EixoId,Nome")] Eixo eixo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eixo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eixo);
        }

        // GET: Eixo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eixo = await _context.Eixo.FindAsync(id);
            if (eixo == null)
            {
                return NotFound();
            }
            return View(eixo);
        }

        // POST: Eixo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EixoId,Nome")] Eixo eixo)
        {
            if (id != eixo.EixoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eixo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EixoExists(eixo.EixoId))
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
            return View(eixo);
        }

        // GET: Eixo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eixo = await _context.Eixo
                .FirstOrDefaultAsync(m => m.EixoId == id);
            if (eixo == null)
            {
                return NotFound();
            }

            return View(eixo);
        }

        // POST: Eixo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Remove vínculos CoordenadorEixo antes de excluir Eixo
            var coordenadorEixos = _context.CoordenadorEixo
                .Where(ce => ce.EixoId == id);
            _context.CoordenadorEixo.RemoveRange(coordenadorEixos);

            // Set EixoId to null for Cursos referencing this Eixo
            var cursos = _context.Curso.Where(c => c.EixoId == id).ToList();
            foreach (var curso in cursos)
            {
                curso.EixoId = null;
            }
            _context.UpdateRange(cursos);

            var eixo = await _context.Eixo.FindAsync(id);
            if (eixo != null)
            {
                _context.Eixo.Remove(eixo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EixoExists(int id)
        {
            return _context.Eixo.Any(e => e.EixoId == id);
        }
    }
}
