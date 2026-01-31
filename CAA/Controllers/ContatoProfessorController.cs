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
    public class ContatoProfessorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContatoProfessorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContatoProfessor
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContatoProfessor.ToListAsync());
        }

        // GET: ContatoProfessor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoProfessor = await _context.ContatoProfessor
                .FirstOrDefaultAsync(m => m.ContatoProfessorId == id);
            if (contatoProfessor == null)
            {
                return NotFound();
            }

            return View(contatoProfessor);
        }

        // GET: ContatoProfessor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContatoProfessor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContatoProfessorId,Nome,Email")] ContatoProfessor contatoProfessor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatoProfessor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Contato");
            }
            return View(contatoProfessor);
        }

        // GET: ContatoProfessor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoProfessor = await _context.ContatoProfessor.FindAsync(id);
            if (contatoProfessor == null)
            {
                return NotFound();
            }
            return View(contatoProfessor);
        }

        // POST: ContatoProfessor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContatoProfessorId,Nome,Email")] ContatoProfessor contatoProfessor)
        {
            if (id != contatoProfessor.ContatoProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoProfessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoProfessorExists(contatoProfessor.ContatoProfessorId))
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
            return View(contatoProfessor);
        }

        // GET: ContatoProfessor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoProfessor = await _context.ContatoProfessor
                .FirstOrDefaultAsync(m => m.ContatoProfessorId == id);
            if (contatoProfessor == null)
            {
                return NotFound();
            }

            return View(contatoProfessor);
        }

        // POST: ContatoProfessor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contatoProfessor = await _context.ContatoProfessor.FindAsync(id);
            if (contatoProfessor != null)
            {
                _context.ContatoProfessor.Remove(contatoProfessor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Contato");
        }

        private bool ContatoProfessorExists(int id)
        {
            return _context.ContatoProfessor.Any(e => e.ContatoProfessorId == id);
        }
    }
}
