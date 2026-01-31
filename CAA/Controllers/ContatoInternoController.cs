using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAA.Data;
using CAA.Models;

namespace CAA.Controllers
{
    public class ContatoInternoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContatoInternoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContatoInterno
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ContatoInterno.Include(c => c.Departamento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ContatoInterno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoInterno = await _context.ContatoInterno
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(m => m.ContatoInternoId == id);
            if (contatoInterno == null)
            {
                return NotFound();
            }

            return View(contatoInterno);
        }

        // GET: ContatoInterno/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome");
            return View();
        }

        // POST: ContatoInterno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContatoInternoId,Nome,DepartamentoId,Telefone,Ramal,Email")] ContatoInterno contatoInterno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatoInterno);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Contato");
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", contatoInterno.DepartamentoId);
            return View(contatoInterno);
        }

        // GET: ContatoInterno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoInterno = await _context.ContatoInterno.FindAsync(id);
            if (contatoInterno == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", contatoInterno.DepartamentoId);
            return View(contatoInterno);
        }

        // POST: ContatoInterno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContatoInternoId,Nome,DepartamentoId,Telefone,Ramal,Email")] ContatoInterno contatoInterno)
        {
            if (id != contatoInterno.ContatoInternoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoInterno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoInternoExists(contatoInterno.ContatoInternoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Contato");
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", contatoInterno.DepartamentoId);
            return View(contatoInterno);
        }

        // GET: ContatoInterno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoInterno = await _context.ContatoInterno
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(m => m.ContatoInternoId == id);
            if (contatoInterno == null)
            {
                return NotFound();
            }

            return View(contatoInterno);
        }

        // POST: ContatoInterno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contatoInterno = await _context.ContatoInterno.FindAsync(id);
            if (contatoInterno != null)
            {
                _context.ContatoInterno.Remove(contatoInterno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Contato");
        }

        private bool ContatoInternoExists(int id)
        {
            return _context.ContatoInterno.Any(e => e.ContatoInternoId == id);
        }
    }
}
