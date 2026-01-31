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
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CAA.Controllers
{
    [Authorize(Roles = "Cursos, Admin")]
    public class CursoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Curso.Include(c => c.Eixo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Eixo)
                    .ThenInclude(e => e.CoordenadorEixo)
                        .ThenInclude(ce => ce.Coordenador)
                .Include(c => c.PlanosFinanceiros)
                    .ThenInclude(pf => pf.Descontos)
                        .ThenInclude(d => d.TipoDesconto)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (curso == null)
            {
                return NotFound();
            }

            // Preenche a ViewBag com os nomes dos tipos de ingresso
            ViewBag.TipoIngressos = _context.TipoIngresso?.Select(t => t.Nome).ToList() ?? new List<string>();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            ViewBag.Atendente = usuario?.Nome;

            return View(curso);
        }

        // GET: Curso/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome");
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("CursoId,Nome,Titulacao,EixoId,QtdSemestres")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", curso.EixoId);
            return View(curso);
        }

        // GET: Curso/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", curso.EixoId);
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("CursoId,Nome,Titulacao,EixoId,QtdSemestres")] Curso curso)
        {
            if (id != curso.CursoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.CursoId))
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
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", curso.EixoId);
            return View(curso);
        }

        // GET: Curso/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso
                .Include(c => c.Eixo)
                .FirstOrDefaultAsync(m => m.CursoId == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca todos os PlanoFinanceiro vinculados ao curso
            var planos = _context.PlanoFinanceiro.Where(p => p.CursoId == id).ToList();
            if (planos.Any())
            {
                // Exclui todos os descontos vinculados aos planos desse curso
                var planoIds = planos.Select(p => p.PlanoFinanceiroId).ToList();
                var descontos = _context.Desconto.Where(d => planoIds.Contains(d.PlanoFinanceiroId));
                if (descontos.Any())
                {
                    _context.Desconto.RemoveRange(descontos);
                }
                // Exclui os planos financeiros
                _context.PlanoFinanceiro.RemoveRange(planos);
            }

            // Exclui o curso
            var curso = await _context.Curso.FindAsync(id);
            if (curso != null)
            {
                _context.Curso.Remove(curso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Curso/UploadDocumento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadDocumento(int id, IFormFile pdfFile)
        {
            if (pdfFile != null && pdfFile.Length > 0 && pdfFile.ContentType == "application/pdf")
            {
                var curso = await _context.Curso.FindAsync(id);
                if (curso == null)
                    return NotFound();
                using (var ms = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(ms);
                    curso.Documento = ms.ToArray();
                }
                _context.Update(curso);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id });
        }

        // GET: Curso/DownloadDocumento/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadDocumento(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            if (curso == null || curso.Documento == null)
                return NotFound();
            // Não define o nome do arquivo para forçar visualização no navegador
            return File(curso.Documento, "application/pdf");
        }

        // POST: Curso/RemoverDocumento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverDocumento(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
                return NotFound();
            curso.Documento = null;
            _context.Update(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id });
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.CursoId == id);
        }
    }
}
