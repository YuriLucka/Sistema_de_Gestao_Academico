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
    [Authorize(Roles = "Cursos,Admin")]
    public class DescontoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescontoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Desconto
        public async Task<IActionResult> Index(int? planoFinanceiroId)
        {
            if (planoFinanceiroId == null)
            {
                return NotFound();
            }

            var descontos = await _context.Desconto
                .Include(d => d.PlanoFinanceiro)
                .Include(d => d.TipoDesconto)
                .Where(d => d.PlanoFinanceiroId == planoFinanceiroId)
                .ToListAsync();

            var planoFinanceiro = await _context.PlanoFinanceiro
                .FirstOrDefaultAsync(p => p.PlanoFinanceiroId == planoFinanceiroId);

            ViewBag.VbPlanoFinanceiroId = planoFinanceiroId;
            ViewBag.VbCursoId = planoFinanceiro?.CursoId;

            return View(descontos);
        }

        // GET: Desconto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto
                .Include(d => d.PlanoFinanceiro)
                .Include(d => d.TipoDesconto)
                .FirstOrDefaultAsync(m => m.DescontoId == id);
            if (desconto == null)
            {
                return NotFound();
            }

            ViewBag.VbPlanoFinanceiroId = desconto.PlanoFinanceiroId;
            return View(desconto);
        }

        // GET: Desconto/Create
        public IActionResult Create(int? planoFinanceiroId)
        {
            var model = new Desconto();
            if (planoFinanceiroId.HasValue)
            {
                model.PlanoFinanceiroId = planoFinanceiroId.Value;
                ViewBag.VbPlanoFinanceiroId = planoFinanceiroId.Value;
            }

            ViewData["TipoDescontoId"] = new SelectList(_context.TipoDesconto, "TipoDescontoId", "Nome");
            return View(model);
        }

        // POST: Desconto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescontoId,PlanoFinanceiroId,TipoDescontoId,Valor,TipoDescontoValor")] Desconto desconto)
        {
            // Verifica se já existe o mesmo tipo de desconto para o mesmo plano
            bool descontoDuplicado = await _context.Desconto.AnyAsync(d => d.PlanoFinanceiroId == desconto.PlanoFinanceiroId && d.TipoDescontoId == desconto.TipoDescontoId);
            if (descontoDuplicado)
            {
                ModelState.AddModelError(string.Empty, "Já existe um desconto deste tipo para este plano financeiro.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(desconto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { planoFinanceiroId = desconto.PlanoFinanceiroId });
            }
            ViewBag.VbPlanoFinanceiroId = desconto.PlanoFinanceiroId;
            ViewData["TipoDescontoId"] = new SelectList(_context.TipoDesconto, "TipoDescontoId", "Nome", desconto.TipoDescontoId);
            return View(desconto);
        }

        // GET: Desconto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto.FindAsync(id);
            if (desconto == null)
            {
                return NotFound();
            }
            ViewBag.VbPlanoFinanceiroId = desconto.PlanoFinanceiroId;
            ViewData["TipoDescontoId"] = new SelectList(_context.TipoDesconto, "TipoDescontoId", "Nome", desconto.TipoDescontoId);
            return View(desconto);
        }

        // POST: Desconto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescontoId,PlanoFinanceiroId,TipoDescontoId,Valor,TipoDescontoValor")] Desconto desconto)
        {
            if (id != desconto.DescontoId)
            {
                return NotFound();
            }

            // Verifica se já existe o mesmo tipo de desconto para o mesmo plano (exclui o próprio desconto)
            bool descontoDuplicado = await _context.Desconto.AnyAsync(d => d.PlanoFinanceiroId == desconto.PlanoFinanceiroId && d.TipoDescontoId == desconto.TipoDescontoId && d.DescontoId != desconto.DescontoId);
            if (descontoDuplicado)
            {
                ModelState.AddModelError(string.Empty, "Já existe um desconto deste tipo para este plano financeiro.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desconto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescontoExists(desconto.DescontoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { planoFinanceiroId = desconto.PlanoFinanceiroId });
            }
            ViewBag.VbPlanoFinanceiroId = desconto.PlanoFinanceiroId;
            ViewData["TipoDescontoId"] = new SelectList(_context.TipoDesconto, "TipoDescontoId", "Nome", desconto.TipoDescontoId);
            return View(desconto);
        }

        // GET: Desconto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Desconto
                .Include(d => d.PlanoFinanceiro)
                .Include(d => d.TipoDesconto)
                .FirstOrDefaultAsync(m => m.DescontoId == id);
            if (desconto == null)
            {
                return NotFound();
            }

            ViewBag.VbPlanoFinanceiroId = desconto.PlanoFinanceiroId;
            return View(desconto);
        }

        // POST: Desconto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desconto = await _context.Desconto.FindAsync(id);
            if (desconto != null)
            {
                _context.Desconto.Remove(desconto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { planoFinanceiroId = desconto.PlanoFinanceiroId });
        }

        [HttpGet]
        public async Task<IActionResult> GetParametroDesconto(int tipoDescontoId)
        {
            // Exemplo: busca o tipo de desconto e valor padrão
            var tipoDesconto = await _context.TipoDesconto.FindAsync(tipoDescontoId);

            if (tipoDesconto == null)
                return Json(new { found = false });

            // Supondo que tipoDesconto tenha os campos abaixo:
            return Json(new
            {
                found = true,
                tipoDescontoValor = tipoDesconto.TipoDescontoValor, // Enum ou string
                valor = tipoDesconto.ValorPadrao // decimal
            });
        }
        private bool DescontoExists(int id)
        {
            return _context.Desconto.Any(e => e.DescontoId == id);
        }
    }
}
