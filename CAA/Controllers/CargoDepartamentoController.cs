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
    public class CargoDepartamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CargoDepartamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CargoDepartamento
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CargoDepartamento.Include(c => c.Cargo).Include(c => c.Departamento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CargoDepartamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoDepartamento = await _context.CargoDepartamento
                .Include(c => c.Cargo)
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(m => m.CargoDepartamentoId == id);
            if (cargoDepartamento == null)
            {
                return NotFound();
            }

            return View(cargoDepartamento);
        }

        // GET: CargoDepartamento/Create
        public IActionResult Create()
        {
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "Nome");
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome");
            return View();
        }

        // POST: CargoDepartamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CargoDepartamentoId,CargoId,DepartamentoId")] CargoDepartamento cargoDepartamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargoDepartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "Nome", cargoDepartamento.CargoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", cargoDepartamento.DepartamentoId);
            return View(cargoDepartamento);
        }

        // GET: CargoDepartamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoDepartamento = await _context.CargoDepartamento.FindAsync(id);
            if (cargoDepartamento == null)
            {
                return NotFound();
            }
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "Nome", cargoDepartamento.CargoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", cargoDepartamento.DepartamentoId);
            return View(cargoDepartamento);
        }

        // POST: CargoDepartamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CargoDepartamentoId,CargoId,DepartamentoId")] CargoDepartamento cargoDepartamento)
        {
            if (id != cargoDepartamento.CargoDepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargoDepartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoDepartamentoExists(cargoDepartamento.CargoDepartamentoId))
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
            ViewData["CargoId"] = new SelectList(_context.Cargo, "CargoId", "Nome", cargoDepartamento.CargoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", cargoDepartamento.DepartamentoId);
            return View(cargoDepartamento);
        }

        // GET: CargoDepartamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargoDepartamento = await _context.CargoDepartamento
                .Include(c => c.Cargo)
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(m => m.CargoDepartamentoId == id);
            if (cargoDepartamento == null)
            {
                return NotFound();
            }

            return View(cargoDepartamento);
        }

        // POST: CargoDepartamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargoDepartamento = await _context.CargoDepartamento.FindAsync(id);
            if (cargoDepartamento != null)
            {
                _context.CargoDepartamento.Remove(cargoDepartamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoDepartamentoExists(int id)
        {
            return _context.CargoDepartamento.Any(e => e.CargoDepartamentoId == id);
        }
    }
}
