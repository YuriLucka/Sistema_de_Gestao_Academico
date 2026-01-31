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
    public class TipoContratoEstagioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoContratoEstagioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoContratoEstagio
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoContratoEstagio.ToListAsync());
        }

        // GET: TipoContratoEstagio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContratoEstagio = await _context.TipoContratoEstagio
                .FirstOrDefaultAsync(m => m.TipoContratoEstagioId == id);
            if (tipoContratoEstagio == null)
            {
                return NotFound();
            }

            return View(tipoContratoEstagio);
        }

        // GET: TipoContratoEstagio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoContratoEstagio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoContratoEstagioId,Nome")] TipoContratoEstagio tipoContratoEstagio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoContratoEstagio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoContratoEstagio);
        }

        // GET: TipoContratoEstagio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContratoEstagio = await _context.TipoContratoEstagio.FindAsync(id);
            if (tipoContratoEstagio == null)
            {
                return NotFound();
            }
            return View(tipoContratoEstagio);
        }

        // POST: TipoContratoEstagio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoContratoEstagioId,Nome")] TipoContratoEstagio tipoContratoEstagio)
        {
            if (id != tipoContratoEstagio.TipoContratoEstagioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoContratoEstagio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoContratoEstagioExists(tipoContratoEstagio.TipoContratoEstagioId))
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
            return View(tipoContratoEstagio);
        }

        // GET: TipoContratoEstagio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoContratoEstagio = await _context.TipoContratoEstagio
                .FirstOrDefaultAsync(m => m.TipoContratoEstagioId == id);
            if (tipoContratoEstagio == null)
            {
                return NotFound();
            }

            return View(tipoContratoEstagio);
        }

        // POST: TipoContratoEstagio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoContratoEstagio = await _context.TipoContratoEstagio.FindAsync(id);

            if (tipoContratoEstagio != null)
            {
                // Busca todos os estágios relacionados a este tipo de contrato
                var estagiosRelacionados = _context.Estagio.Where(e => e.TipoContratoEstagioId == id);

                // Remove todos os estágios encontrados
                _context.Estagio.RemoveRange(estagiosRelacionados);

                // Remove o tipo de contrato
                _context.TipoContratoEstagio.Remove(tipoContratoEstagio);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TipoContratoEstagioExists(int id)
        {
            return _context.TipoContratoEstagio.Any(e => e.TipoContratoEstagioId == id);
        }
    }
}
