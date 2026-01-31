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
    public class CoordenadorEixoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordenadorEixoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CoordenadorEixo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CoordenadorEixo.Include(c => c.Coordenador).Include(c => c.Eixo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CoordenadorEixo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenadorEixo = await _context.CoordenadorEixo
                .Include(c => c.Coordenador)
                .Include(c => c.Eixo)
                .FirstOrDefaultAsync(m => m.CoordenadorEixoId == id);
            if (coordenadorEixo == null)
            {
                return NotFound();
            }

            return View(coordenadorEixo);
        }

        // GET: CoordenadorEixo/Create
        public IActionResult Create()
        {
            ViewData["CoordenadorId"] = new SelectList(_context.Coordenador, "CoordenadorId", "Nome");
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome");
            return View();
        }

        // POST: CoordenadorEixo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoordenadorEixoId,CoordenadorId,EixoId")] CoordenadorEixo coordenadorEixo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordenadorEixo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordenadorId"] = new SelectList(_context.Coordenador, "CoordenadorId", "Nome", coordenadorEixo.CoordenadorId);
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", coordenadorEixo.EixoId);
            return View(coordenadorEixo);
        }

        // GET: CoordenadorEixo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenadorEixo = await _context.CoordenadorEixo.FindAsync(id);
            if (coordenadorEixo == null)
            {
                return NotFound();
            }
            ViewData["CoordenadorId"] = new SelectList(_context.Coordenador, "CoordenadorId", "Nome", coordenadorEixo.CoordenadorId);
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", coordenadorEixo.EixoId);
            return View(coordenadorEixo);
        }

        // POST: CoordenadorEixo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoordenadorEixoId,CoordenadorId,EixoId")] CoordenadorEixo coordenadorEixo)
        {
            if (id != coordenadorEixo.CoordenadorEixoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordenadorEixo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordenadorEixoExists(coordenadorEixo.CoordenadorEixoId))
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
            ViewData["CoordenadorId"] = new SelectList(_context.Coordenador, "CoordenadorId", "Nome", coordenadorEixo.CoordenadorId);
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", coordenadorEixo.EixoId);
            return View(coordenadorEixo);
        }

        // GET: CoordenadorEixo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordenadorEixo = await _context.CoordenadorEixo
                .Include(c => c.Coordenador)
                .Include(c => c.Eixo)
                .FirstOrDefaultAsync(m => m.CoordenadorEixoId == id);
            if (coordenadorEixo == null)
            {
                return NotFound();
            }

            return View(coordenadorEixo);
        }

        // POST: CoordenadorEixo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordenadorEixo = await _context.CoordenadorEixo.FindAsync(id);
            if (coordenadorEixo != null)
            {
                _context.CoordenadorEixo.Remove(coordenadorEixo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordenadorEixoExists(int id)
        {
            return _context.CoordenadorEixo.Any(e => e.CoordenadorEixoId == id);
        }
    }
}
