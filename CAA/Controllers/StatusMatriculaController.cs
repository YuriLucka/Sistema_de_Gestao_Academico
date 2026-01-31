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
    public class StatusMatriculaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusMatriculaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusMatricula
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusMatricula.ToListAsync());
        }

        // GET: StatusMatricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusMatricula = await _context.StatusMatricula
                .FirstOrDefaultAsync(m => m.StatusMatriculaId == id);
            if (statusMatricula == null)
            {
                return NotFound();
            }

            return View(statusMatricula);
        }

        // GET: StatusMatricula/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusMatricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusMatriculaId,Nome")] StatusMatricula statusMatricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusMatricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusMatricula);
        }

        // GET: StatusMatricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusMatricula = await _context.StatusMatricula.FindAsync(id);
            if (statusMatricula == null)
            {
                return NotFound();
            }
            return View(statusMatricula);
        }

        // POST: StatusMatricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusMatriculaId,Nome")] StatusMatricula statusMatricula)
        {
            if (id != statusMatricula.StatusMatriculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusMatricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusMatriculaExists(statusMatricula.StatusMatriculaId))
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
            return View(statusMatricula);
        }

        // GET: StatusMatricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusMatricula = await _context.StatusMatricula
                .FirstOrDefaultAsync(m => m.StatusMatriculaId == id);
            if (statusMatricula == null)
            {
                return NotFound();
            }

            return View(statusMatricula);
        }

        // POST: StatusMatricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusMatricula = await _context.StatusMatricula.FindAsync(id);
            if (statusMatricula != null)
            {
                _context.StatusMatricula.Remove(statusMatricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusMatriculaExists(int id)
        {
            return _context.StatusMatricula.Any(e => e.StatusMatriculaId == id);
        }
    }
}
