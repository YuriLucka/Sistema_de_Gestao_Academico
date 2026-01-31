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
    public class TipoIngressoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoIngressoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoIngresso
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoIngresso.ToListAsync());
        }

        // GET: TipoIngresso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIngresso = await _context.TipoIngresso
                .FirstOrDefaultAsync(m => m.TipoIngressoId == id);
            if (tipoIngresso == null)
            {
                return NotFound();
            }

            return View(tipoIngresso);
        }

        // GET: TipoIngresso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoIngresso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoIngressoId,Nome")] TipoIngresso tipoIngresso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoIngresso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoIngresso);
        }

        // GET: TipoIngresso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIngresso = await _context.TipoIngresso.FindAsync(id);
            if (tipoIngresso == null)
            {
                return NotFound();
            }
            return View(tipoIngresso);
        }

        // POST: TipoIngresso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoIngressoId,Nome")] TipoIngresso tipoIngresso)
        {
            if (id != tipoIngresso.TipoIngressoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoIngresso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoIngressoExists(tipoIngresso.TipoIngressoId))
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
            return View(tipoIngresso);
        }

        // GET: TipoIngresso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoIngresso = await _context.TipoIngresso
                .FirstOrDefaultAsync(m => m.TipoIngressoId == id);
            if (tipoIngresso == null)
            {
                return NotFound();
            }

            return View(tipoIngresso);
        }

        // POST: TipoIngresso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoIngresso = await _context.TipoIngresso.FindAsync(id);
            if (tipoIngresso != null)
            {
                _context.TipoIngresso.Remove(tipoIngresso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoIngressoExists(int id)
        {
            return _context.TipoIngresso.Any(e => e.TipoIngressoId == id);
        }
    }
}
