using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Authorization;

namespace CAA.Controllers
{
    [Authorize(Roles = "Fichas Médicas, Admin")]
    public class FichaMedicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FichaMedicaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FichaMedica
        public async Task<IActionResult> Index()
        {
            return View(await _context.FichaMedica.OrderByDescending(x => x.DataPreenchimento).ThenByDescending(x => x.FichaMedicaId).ToListAsync());
        }

        [AllowAnonymous]
        public ActionResult AvisoAluno()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult FichaAluno()
        {
            var parametro = _context.ParametroGeral.FirstOrDefault();
            ViewBag.SenhaCadastrada = parametro != null && !string.IsNullOrEmpty(parametro.SenhaFichaMedica);
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> FichaAluno(FichaMedica fichaMedica, string Senha)
        {
            var parametro = await _context.ParametroGeral.FirstOrDefaultAsync();
            if (parametro != null)
            {
                if (parametro.SenhaFichaMedica != Senha)
                {
                    ViewBag.SenhaCadastrada = !string.IsNullOrEmpty(parametro.SenhaFichaMedica);
                    ViewBag.SenhaErro = "Senha incorreta. Tente novamente.";
                    return View(fichaMedica);
                }
            }
            _context.Add(fichaMedica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AvisoAluno));
        }

        // GET: FichaMedica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaMedica = await _context.FichaMedica
                .FirstOrDefaultAsync(m => m.FichaMedicaId == id);
            if (fichaMedica == null)
            {
                return NotFound();
            }

            return View(fichaMedica);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FichaMedicaId,NomeCompleto,RA,Diabetes,CriseConvulsiva,Taquicardia,Bronquite,Rinite,Sinusite,OutrosProblemasCronicos,Medicamentos,AlergiaMedicamentos,AlergiaInsetos,AlergiaAlimentos,OutrasAlergias,TratamentoMedico,ConvenioMedico,DefCegueira,DefBaixaVisao,DefSurdocegueira,DefSurdez,DefAuditiva,DefFisica,DefMultipla,DefIntelectual,OutrasDeficiencias,InformacoesAdicionais,NomeContato1,Contato1,NomeContato2,Contato2,NomeContato3,Contato3,DataPreenchimento")] FichaMedica fichaMedica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fichaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fichaMedica);
        }

        // GET: FichaMedica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaMedica = await _context.FichaMedica.FindAsync(id);
            if (fichaMedica == null)
            {
                return NotFound();
            }
            // Encaminha para a view FichaImpressa, passando o modelo
            return View(fichaMedica);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FichaMedicaId,NomeCompleto,RA,Diabetes,CriseConvulsiva,Taquicardia,Bronquite,Rinite,Sinusite,OutrosProblemasCronicos,Medicamentos,AlergiaMedicamentos,AlergiaInsetos,AlergiaAlimentos,OutrasAlergias,TratamentoMedico,ConvenioMedico,DefCegueira,DefBaixaVisao,DefSurdocegueira,DefSurdez,DefAuditiva,DefFisica,DefMultipla,DefIntelectual,OutrasDeficiencias,InformacoesAdicionais,NomeContato1,Contato1,NomeContato2,Contato2,NomeContato3,Contato3,DataPreenchimento")] FichaMedica fichaMedica)
        {
            if (id != fichaMedica.FichaMedicaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fichaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichaMedicaExists(fichaMedica.FichaMedicaId))
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
            return View(fichaMedica);
        }

        // GET: FichaMedica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaMedica = await _context.FichaMedica
                .FirstOrDefaultAsync(m => m.FichaMedicaId == id);
            if (fichaMedica == null)
            {
                return NotFound();
            }

            return View(fichaMedica);
        }

        // POST: FichaMedica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fichaMedica = await _context.FichaMedica.FindAsync(id);
            if (fichaMedica != null)
            {
                _context.FichaMedica.Remove(fichaMedica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichaMedicaExists(int id)
        {
            return _context.FichaMedica.Any(e => e.FichaMedicaId == id);
        }
    }
}
