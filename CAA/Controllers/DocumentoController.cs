using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CAA.Controllers
{
    [Authorize(Roles = "Documentos Institucionais, Admin")]
    public class DocumentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Documento
        public async Task<IActionResult> Index(string tipo, string busca)
        {
            var documentos = await _context.Documentos.OrderBy(x => x.Descricao).ToListAsync();
            if (!string.IsNullOrEmpty(tipo))
            {
                documentos = tipo switch
                {
                    "imagem" => documentos.Where(d => new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" }.Contains(Path.GetExtension(d.Nome).ToLowerInvariant())).ToList(),
                    "pdf" => documentos.Where(d => Path.GetExtension(d.Nome).ToLowerInvariant() == ".pdf").ToList(),
                    "word" => documentos.Where(d => new[] { ".doc", ".docx" }.Contains(Path.GetExtension(d.Nome).ToLowerInvariant())).ToList(),
                    "excel" => documentos.Where(d => new[] { ".xls", ".xlsx" }.Contains(Path.GetExtension(d.Nome).ToLowerInvariant())).ToList(),
                    _ => documentos
                };
            }
            if (!string.IsNullOrWhiteSpace(busca))
            {
                documentos = documentos.Where(d => d.Descricao != null && d.Descricao.ToLower().Contains(busca.ToLower())).ToList();
            }
            ViewBag.Busca = busca;
            return View(documentos);
        }

        // GET: Documento/Download/5
        public async Task<IActionResult> Download(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null || documento.Arquivo == null)
                return NotFound();
            var ext = Path.GetExtension(documento.Nome).ToLowerInvariant();
            var contentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
            return File(documento.Arquivo, contentType, documento.Nome);
        }

        // GET: Documento/Visualizar/5
        public async Task<IActionResult> Visualizar(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null || documento.Arquivo == null)
                return NotFound();
            var ext = Path.GetExtension(documento.Nome).ToLowerInvariant();
            var contentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            };
            // Não força download, apenas exibe
            return File(documento.Arquivo, contentType);
        }

        // POST: Documento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento != null)
            {
                _context.Documentos.Remove(documento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Documento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] Documento documento, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                ModelState.AddModelError("Arquivo", "O arquivo é obrigatório.");
            }
            else
            {
                // Tipos permitidos: imagens, PDF, Word, Excel
                var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
                var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
                if (!permittedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("Arquivo", "Tipo de arquivo não permitido.");
                }
                if (arquivo.Length > 100 * 1024 * 1024)
                {
                    ModelState.AddModelError("Arquivo", "O tamanho máximo permitido é 100MB.");
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                await arquivo.CopyToAsync(memoryStream);
                documento.Arquivo = memoryStream.ToArray();
            }
            documento.Nome = Path.GetFileName(arquivo.FileName);
            _context.Add(documento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Documento/RenomearDescricao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RenomearDescricao(int DocumentoId, string Descricao)
        {
            var documento = await _context.Documentos.FindAsync(DocumentoId);
            if (documento == null)
            {
                return NotFound();
            }
            documento.Descricao = Descricao;
            _context.Update(documento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
