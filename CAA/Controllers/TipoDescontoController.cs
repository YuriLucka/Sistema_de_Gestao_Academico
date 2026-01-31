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
    public class TipoDescontoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoDescontoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoDesconto
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDesconto.ToListAsync());
        }

        // GET: TipoDesconto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDesconto = await _context.TipoDesconto
                .FirstOrDefaultAsync(m => m.TipoDescontoId == id);
            if (tipoDesconto == null)
            {
                return NotFound();
            }

            return View(tipoDesconto);
        }

        // GET: TipoDesconto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDesconto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDescontoId,Nome,ValorPadrao,TipoDescontoValor")] TipoDesconto tipoDesconto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDesconto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDesconto);
        }

        // GET: TipoDesconto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDesconto = await _context.TipoDesconto.FindAsync(id);
            if (tipoDesconto == null)
            {
                return NotFound();
            }
            return View(tipoDesconto);
        }

        // POST: TipoDesconto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoDescontoId,Nome,ValorPadrao,TipoDescontoValor")] TipoDesconto tipoDesconto)
        {
            if (id != tipoDesconto.TipoDescontoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDesconto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDescontoExists(tipoDesconto.TipoDescontoId))
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
            return View(tipoDesconto);
        }

        // GET: TipoDesconto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDesconto = await _context.TipoDesconto
                .FirstOrDefaultAsync(m => m.TipoDescontoId == id);
            if (tipoDesconto == null)
            {
                return NotFound();
            }

            // Verifica se existe algum plano financeiro vinculado via descontos
            bool vinculadoAPlanosFinanceiros = await _context.Desconto.AnyAsync(d => d.TipoDescontoId == id);
            ViewBag.VinculadoAPlanosFinanceiros = vinculadoAPlanosFinanceiros;

            return View(tipoDesconto);
        }

        // POST: TipoDesconto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDesconto = await _context.TipoDesconto.FindAsync(id);
            if (tipoDesconto == null)
            {
                return NotFound();
            }

            // Remove todos os descontos relacionados
            var descontosRelacionados = await _context.Desconto.Where(d => d.TipoDescontoId == id).ToListAsync();
            if (descontosRelacionados.Any())
            {
                _context.Desconto.RemoveRange(descontosRelacionados);
            }

            _context.TipoDesconto.Remove(tipoDesconto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDescontoExists(int id)
        {
            return _context.TipoDesconto.Any(e => e.TipoDescontoId == id);
        }
    }
}
