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
    public class ContatoExternoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContatoExternoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContatoExterno
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContatoExterno.ToListAsync());
        }

        // GET: ContatoExterno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoExterno = await _context.ContatoExterno
                .FirstOrDefaultAsync(m => m.ContatoExternoId == id);
            if (contatoExterno == null)
            {
                return NotFound();
            }

            return View(contatoExterno);
        }

        // GET: ContatoExterno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContatoExterno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContatoExternoId,Nome,Telefone,Email")] ContatoExterno contatoExterno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatoExterno);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Contato");
            }
            return View(contatoExterno);
        }

        // GET: ContatoExterno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoExterno = await _context.ContatoExterno.FindAsync(id);
            if (contatoExterno == null)
            {
                return NotFound();
            }
            return View(contatoExterno);
        }

        // POST: ContatoExterno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContatoExternoId,Nome,Telefone,Email")] ContatoExterno contatoExterno)
        {
            if (id != contatoExterno.ContatoExternoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoExterno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExternoExists(contatoExterno.ContatoExternoId))
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
            return View(contatoExterno);
        }

        // GET: ContatoExterno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoExterno = await _context.ContatoExterno
                .FirstOrDefaultAsync(m => m.ContatoExternoId == id);
            if (contatoExterno == null)
            {
                return NotFound();
            }

            return View(contatoExterno);
        }

        // POST: ContatoExterno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contatoExterno = await _context.ContatoExterno.FindAsync(id);
            if (contatoExterno != null)
            {
                _context.ContatoExterno.Remove(contatoExterno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Contato");
        }

        private bool ContatoExternoExists(int id)
        {
            return _context.ContatoExterno.Any(e => e.ContatoExternoId == id);
        }
    }
}
