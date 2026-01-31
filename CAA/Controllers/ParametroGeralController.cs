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
    public class ParametroGeralController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametroGeralController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParametroGeral/Config
        public async Task<IActionResult> Config()
        {
            var parametro = await _context.ParametroGeral.FirstOrDefaultAsync();
            if (parametro == null)
            {
                parametro = new ParametroGeral();
                _context.ParametroGeral.Add(parametro);
                await _context.SaveChangesAsync();
            }
            return View(parametro);
        }

        // POST: ParametroGeral/Config
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Config(ParametroGeral parametroGeral)
        {
            if (ModelState.IsValid)
            {
                var parametro = await _context.ParametroGeral.FirstOrDefaultAsync();
                if (parametro == null)
                {
                    parametro = new ParametroGeral();
                    _context.ParametroGeral.Add(parametro);
                }
                parametro.SenhaFichaMedica = parametroGeral.SenhaFichaMedica;
                await _context.SaveChangesAsync();
                ViewBag.Sucesso = true;
            }
            return View(parametroGeral);
        }
    }
}
