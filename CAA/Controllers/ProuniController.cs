using CAA.Data;
using CAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CAA.Controllers
{
    [Authorize]
    public class ProuniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProuniController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var campos = await _context.ProuniCampoDocumentos
                .Where(c => c.Ativo)
                .OrderBy(c => c.Ordem)
                .ThenBy(c => c.Nome)
                .ToListAsync();
            return View(campos);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
        public async Task<IActionResult> Enviar(string nomeCandidato, string cpfCandidato)
        {
            var campos = await _context.ProuniCampoDocumentos
                .Where(c => c.Ativo)
                .OrderBy(c => c.Ordem)
                .ThenBy(c => c.Nome)
                .ToListAsync();

            var cpfLimpo = new string((cpfCandidato ?? string.Empty).Where(char.IsDigit).ToArray());

            if (string.IsNullOrWhiteSpace(nomeCandidato))
            {
                ViewBag.Erro = "Nome é obrigatório.";
                ViewBag.CpfCandidato = cpfCandidato;
                return View("Index", campos);
            }

            if (!ValidarCpf(cpfLimpo))
            {
                ViewBag.Erro = "CPF inválido. Verifique e tente novamente.";
                ViewBag.NomeCandidato = nomeCandidato;
                ViewBag.CpfCandidato = cpfCandidato;
                return View("Index", campos);
            }

            foreach (var campo in campos.Where(c => c.Obrigatorio))
            {
                bool temArquivo;
                if (campo.TemFrenteVerso)
                {
                    var f = Request.Form.Files[$"arquivo_{campo.Id}_frente"];
                    var v = Request.Form.Files[$"arquivo_{campo.Id}_verso"];
                    temArquivo = (f != null && f.Length > 0) || (v != null && v.Length > 0);
                }
                else
                {
                    var f = Request.Form.Files[$"arquivo_{campo.Id}"];
                    temArquivo = f != null && f.Length > 0;
                }

                if (!temArquivo)
                {
                    ViewBag.Erro = $"O documento '{campo.Nome}' é obrigatório.";
                    ViewBag.NomeCandidato = nomeCandidato;
                    ViewBag.CpfCandidato = cpfCandidato;
                    return View("Index", campos);
                }
            }

            var cpfFormatado = $"{cpfLimpo[..3]}.{cpfLimpo[3..6]}.{cpfLimpo[6..9]}-{cpfLimpo[9..11]}";
            var brTz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var submissao = new ProuniSubmissao
            {
                NomeCandidato = nomeCandidato.Trim(),
                CpfCandidato = cpfFormatado,
                DataEnvio = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brTz),
                Status = "Pendente"
            };

            foreach (var campo in campos)
            {
                if (campo.TemFrenteVerso)
                {
                    submissao.Documentos.Add(await CriarDocumento(campo.Id, "Frente", $"arquivo_{campo.Id}_frente"));
                    submissao.Documentos.Add(await CriarDocumento(campo.Id, "Verso", $"arquivo_{campo.Id}_verso"));
                }
                else
                {
                    submissao.Documentos.Add(await CriarDocumento(campo.Id, "Unico", $"arquivo_{campo.Id}"));
                }
            }

            _context.ProuniSubmissoes.Add(submissao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Confirmacao));
        }

        private async Task<ProuniDocumentoAnexado> CriarDocumento(int campoId, string lado, string inputName)
        {
            var doc = new ProuniDocumentoAnexado
            {
                CampoDocumentoId = campoId,
                Lado = lado,
                Status = "NaoEnviado"
            };

            var file = Request.Form.Files[inputName];
            if (file != null && file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                doc.Arquivo = ms.ToArray();
                doc.NomeArquivo = file.FileName;
                doc.ContentType = file.ContentType;
                doc.Status = "Aguardando";
            }

            return doc;
        }

        [AllowAnonymous]
        public IActionResult Confirmacao()
        {
            return View();
        }

        [Authorize(Roles = "ProUni, Admin")]
        public async Task<IActionResult> Analise(string? nome, string? cpf, string? status)
        {
            var query = _context.ProuniSubmissoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(s => s.NomeCandidato.Contains(nome));
            if (!string.IsNullOrWhiteSpace(cpf))
                query = query.Where(s => s.CpfCandidato.Contains(cpf));
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(s => s.Status == status);

            var submissoes = await query
                .OrderByDescending(s => s.DataEnvio)
                .Select(s => new ProuniSubmissaoListaVm
                {
                    Id = s.Id,
                    NomeCandidato = s.NomeCandidato,
                    CpfCandidato = s.CpfCandidato,
                    DataEnvio = s.DataEnvio,
                    Status = s.Status,
                    TotalDocs = s.Documentos.Count(),
                    DocsEnviados = s.Documentos.Count(d => d.Arquivo != null)
                })
                .ToListAsync();

            ViewBag.Filtro_Nome = nome;
            ViewBag.Filtro_Cpf = cpf;
            ViewBag.Filtro_Status = status;

            return View(submissoes);
        }

        [Authorize(Roles = "ProUni, Admin")]
        public async Task<IActionResult> DocumentosSubmissao(int id)
        {
            var submissao = await _context.ProuniSubmissoes
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.NomeCandidato,
                    s.CpfCandidato,
                    s.DataEnvio,
                    s.Status,
                    s.Comentario,
                    Documentos = s.Documentos.Select(d => new
                    {
                        d.Id,
                        CampoNome = d.CampoDocumento.Nome,
                        d.Lado,
                        TemArquivo = d.Arquivo != null,
                        d.ContentType,
                        d.Status,
                        d.Comentario
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (submissao == null) return NotFound();

            return Json(submissao);
        }

        [Authorize(Roles = "ProUni, Admin")]
        public async Task<IActionResult> Arquivo(int id)
        {
            var doc = await _context.ProuniDocumentosAnexados
                .Where(d => d.Id == id)
                .Select(d => new { d.Arquivo, d.ContentType, d.NomeArquivo })
                .FirstOrDefaultAsync();

            if (doc?.Arquivo == null) return NotFound();

            return File(doc.Arquivo, doc.ContentType ?? "application/octet-stream");
        }

        [Authorize(Roles = "ProUni, Admin")]
        [HttpPost]
        public async Task<IActionResult> AtualizarDocumento([FromBody] AtualizarDocumentoRequest req)
        {
            var doc = await _context.ProuniDocumentosAnexados.FindAsync(req.Id);
            if (doc == null) return NotFound();

            doc.Status = req.Status;
            doc.Comentario = req.Comentario;
            await _context.SaveChangesAsync();

            return Ok(new { ok = true });
        }

        [Authorize(Roles = "ProUni, Admin")]
        [HttpPost]
        public async Task<IActionResult> AtualizarStatusSubmissao([FromBody] AtualizarStatusRequest req)
        {
            var submissao = await _context.ProuniSubmissoes.FindAsync(req.Id);
            if (submissao == null) return NotFound();

            var brTz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            submissao.Status = req.Status;
            submissao.Comentario = req.Comentario;
            submissao.AnalistaId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            submissao.DataAnalise = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brTz);
            await _context.SaveChangesAsync();

            return Ok(new { ok = true });
        }

        private static bool ValidarCpf(string cpf)
        {
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

            int[] m1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] m2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = m1.Select((m, i) => (cpf[i] - '0') * m).Sum();
            int d1 = 11 - soma % 11;
            if (d1 >= 10) d1 = 0;

            soma = m2.Select((m, i) => (cpf[i] - '0') * m).Sum();
            int d2 = 11 - soma % 11;
            if (d2 >= 10) d2 = 0;

            return (cpf[9] - '0') == d1 && (cpf[10] - '0') == d2;
        }
    }

    public class AtualizarDocumentoRequest
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Comentario { get; set; }
    }

    public class AtualizarStatusRequest
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Comentario { get; set; }
    }
}
