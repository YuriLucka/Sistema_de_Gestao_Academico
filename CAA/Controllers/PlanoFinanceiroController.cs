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
    public class PlanoFinanceiroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanoFinanceiroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlanoFinanceiro
        public async Task<IActionResult> Index(int? cursoId)
        {
            if (cursoId == null)
            {
                return NotFound();
            }
            var planos = await _context.PlanoFinanceiro
                .Include(p => p.Curso)
                .Where(p => p.CursoId == cursoId)
                .ToListAsync();

            ViewBag.VbCursoId = cursoId;
            return View(planos);
        }

        // GET: PlanoFinanceiro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var planoFinanceiro = await _context.PlanoFinanceiro
                .Include(p => p.Curso)
                .FirstOrDefaultAsync(m => m.PlanoFinanceiroId == id);
            if (planoFinanceiro == null)
            {
                return NotFound();
            }

            ViewBag.VbCursoId = planoFinanceiro.CursoId;
            return View(planoFinanceiro);
        }

        // GET: PlanoFinanceiro/Create
        // GET: PlanoFinanceiro/Create
        public IActionResult Create(int? cursoId)
        {
            var model = new PlanoFinanceiro();
            if (cursoId.HasValue)
            {
                model.CursoId = cursoId.Value;
                ViewBag.VbCursoId = cursoId.Value;
            }
            return View(model);
        }

        // POST: PlanoFinanceiro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanoFinanceiroId,CursoId,TipoPlanoFinanceiro,Valor,Matutino,Noturno")] PlanoFinanceiro planoFinanceiro)
        {
            if (!planoFinanceiro.Matutino && !planoFinanceiro.Noturno)
            {
                ModelState.AddModelError(string.Empty, "Selecione pelo menos um período: Matutino ou Noturno.");
            }

            // Verifica duplicidade de TipoPlanoFinanceiro para o mesmo período
            var planosExistentes = await _context.PlanoFinanceiro
                .Where(p => p.CursoId == planoFinanceiro.CursoId && p.TipoPlanoFinanceiro == planoFinanceiro.TipoPlanoFinanceiro)
                .ToListAsync();
            if (planoFinanceiro.Matutino && planosExistentes.Any(p => p.Matutino))
            {
                ModelState.AddModelError(string.Empty, "Já existe um plano deste tipo para o período Matutino.");
            }
            if (planoFinanceiro.Noturno && planosExistentes.Any(p => p.Noturno))
            {
                ModelState.AddModelError(string.Empty, "Já existe um plano deste tipo para o período Noturno.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(planoFinanceiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { cursoId = planoFinanceiro.CursoId });
            }
            ViewBag.VbCursoId = planoFinanceiro.CursoId;
            return View(planoFinanceiro);
        }

        // GET: PlanoFinanceiro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoFinanceiro = await _context.PlanoFinanceiro.FindAsync(id);
            if (planoFinanceiro == null)
            {
                return NotFound();
            }
            ViewBag.VbCursoId = planoFinanceiro.CursoId;
            return View(planoFinanceiro);
        }

        // POST: PlanoFinanceiro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanoFinanceiroId,CursoId,TipoPlanoFinanceiro,Valor,Matutino,Noturno")] PlanoFinanceiro planoFinanceiro)
        {
            if (id != planoFinanceiro.PlanoFinanceiroId)
            {
                return NotFound();
            }

            if (!planoFinanceiro.Matutino && !planoFinanceiro.Noturno)
            {
                ModelState.AddModelError(string.Empty, "Selecione pelo menos um período: Matutino ou Noturno.");
            }

            // Verifica duplicidade de TipoPlanoFinanceiro para o mesmo período (exclui o próprio plano)
            var planosExistentes = await _context.PlanoFinanceiro
                .Where(p => p.CursoId == planoFinanceiro.CursoId && p.TipoPlanoFinanceiro == planoFinanceiro.TipoPlanoFinanceiro && p.PlanoFinanceiroId != planoFinanceiro.PlanoFinanceiroId)
                .ToListAsync();
            if (planoFinanceiro.Matutino && planosExistentes.Any(p => p.Matutino))
            {
                ModelState.AddModelError(string.Empty, "Já existe um plano deste tipo para o período Matutino.");
            }
            if (planoFinanceiro.Noturno && planosExistentes.Any(p => p.Noturno))
            {
                ModelState.AddModelError(string.Empty, "Já existe um plano deste tipo para o período Noturno.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planoFinanceiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoFinanceiroExists(planoFinanceiro.PlanoFinanceiroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { cursoId = planoFinanceiro.CursoId });
            }
            ViewBag.VbCursoId = planoFinanceiro.CursoId;
            return View(planoFinanceiro);
        }

        // GET: PlanoFinanceiro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoFinanceiro = await _context.PlanoFinanceiro
                .Include(p => p.Curso)
                .FirstOrDefaultAsync(m => m.PlanoFinanceiroId == id);
            if (planoFinanceiro == null)
            {
                return NotFound();
            }

            ViewBag.VbCursoId = planoFinanceiro.CursoId;
            return View(planoFinanceiro);
        }

        // POST: PlanoFinanceiro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca todos os Descontos vinculados ao Plano Financeiro
            var descontos = _context.Desconto.Where(p => p.PlanoFinanceiroId == id);
            if (descontos.Any())
            {
                _context.Desconto.RemoveRange(descontos);
            }

            var planoFinanceiro = await _context.PlanoFinanceiro.FindAsync(id);
            if (planoFinanceiro != null)
            {
                _context.PlanoFinanceiro.Remove(planoFinanceiro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { cursoId = planoFinanceiro.CursoId });
        }

        private bool PlanoFinanceiroExists(int id)
        {
            return _context.PlanoFinanceiro.Any(e => e.PlanoFinanceiroId == id);
        }
    }
}
