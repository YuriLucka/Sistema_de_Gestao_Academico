using CAA.Data;
using CAA.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CAA.Controllers
{
    [Authorize(Roles = "Estágios, Admin")]
    public class EstagioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstagioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Index
        public IActionResult Index()
        {
            return View(); // NÃO passa Model aqui
        }

        // POST: DataTable
        [HttpPost]
        public IActionResult GetEstagiosDataTable()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
                var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 10;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                // Query base
                var estagiosQuery = _context.Estagio
                    .Include(e => e.Curso)
                    .Include(e => e.TipoContratoEstagio)
                    .Include(e => e.Empresa)
                    .AsQueryable();

                // Total de registros (antes do filtro)
                int recordsTotal = estagiosQuery.Count();

                // Aplicar busca
                if (!string.IsNullOrEmpty(searchValue))
                {
                    estagiosQuery = estagiosQuery.Where(e =>
                        e.RA.Contains(searchValue) ||
                        e.Nome.Contains(searchValue) ||
                        (e.Curso != null && e.Curso.Nome.Contains(searchValue)) ||
                        (e.TipoContratoEstagio != null && e.TipoContratoEstagio.Nome.Contains(searchValue)) ||
                        (e.Empresa != null && e.Empresa.CNPJ.Contains(searchValue)) ||
                        (e.Integradora != null && e.Integradora.Contains(searchValue)) ||
                        (e.Apolice != null && e.Apolice.Contains(searchValue)) ||
                        (e.Seguradora != null && e.Seguradora.Contains(searchValue))
                    );
                }

                // Total após filtro
                int recordsFiltered = estagiosQuery.Count();

                // Aplicar ordenação
                if (!string.IsNullOrEmpty(sortColumnIndex))
                {
                    switch (sortColumnIndex)
                    {
                        case "0": // RA
                            estagiosQuery = sortDirection == "asc"
                                ? estagiosQuery.OrderBy(e => e.RA)
                                : estagiosQuery.OrderByDescending(e => e.RA);
                            break;
                        case "1": // Nome
                            estagiosQuery = sortDirection == "asc"
                                ? estagiosQuery.OrderBy(e => e.Nome)
                                : estagiosQuery.OrderByDescending(e => e.Nome);
                            break;
                        case "6": // VigenciaInicio
                            estagiosQuery = sortDirection == "asc"
                                ? estagiosQuery.OrderBy(e => e.VigenciaInicio)
                                : estagiosQuery.OrderByDescending(e => e.VigenciaInicio);
                            break;
                        default:
                            estagiosQuery = estagiosQuery.OrderByDescending(e => e.VigenciaInicio);
                            break;
                    }
                }
                else
                {
                    estagiosQuery = estagiosQuery.OrderByDescending(e => e.VigenciaInicio);
                }

                // Aplicar paginação e projetar dados
                var data = estagiosQuery
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(e => new
                    {
                        estagioId = e.EstagioId,
                        ra = e.RA ?? "",
                        nome = e.Nome ?? "",
                        curso = e.Curso != null ? e.Curso.Nome : "",
                        tipoContrato = e.TipoContratoEstagio != null ? e.TipoContratoEstagio.Nome : "",
                        empresa = e.Empresa != null ? e.Empresa.CNPJ : "",
                        integradora = e.Integradora ?? "",
                        vigenciaInicio = e.VigenciaInicio.HasValue ? e.VigenciaInicio.Value.ToString("dd/MM/yyyy") : "",
                        vigenciaTermino = e.VigenciaTermino.HasValue ? e.VigenciaTermino.Value.ToString("dd/MM/yyyy") : "",
                        apolice = e.Apolice ?? "",
                        seguradora = e.Seguradora ?? ""
                    })
                    .ToList();

                return Json(new
                {
                    draw = draw,
                    recordsFiltered = recordsFiltered,
                    recordsTotal = recordsTotal,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        // GET: Estagio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estagio = await _context.Estagio
                .Include(e => e.Curso)
                .Include(e => e.Empresa)
                .Include(e => e.TipoContratoEstagio)
                .FirstOrDefaultAsync(m => m.EstagioId == id);
            if (estagio == null)
            {
                return NotFound();
            }

            return View(estagio);
        }

        // GET: Estagio/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome");
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaId", "CNPJ");
            ViewData["TipoContratoEstagioId"] = new SelectList(_context.TipoContratoEstagio, "TipoContratoEstagioId", "Nome");
            return View();
        }

        // POST: Estagio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstagioId,RA,Nome,CursoId,TipoContratoEstagioId,EmpresaId,Integradora,VigenciaInicio,VigenciaTermino,Apolice,Seguradora")] Estagio estagio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estagio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome", estagio.CursoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaId", "CNPJ", estagio.EmpresaId);
            ViewData["TipoContratoEstagioId"] = new SelectList(_context.TipoContratoEstagio, "TipoContratoEstagioId", "Nome", estagio.TipoContratoEstagioId);
            return View(estagio);
        }

        // GET: Estagio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estagio = await _context.Estagio.FindAsync(id);
            if (estagio == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome", estagio.CursoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaId", "CNPJ", estagio.EmpresaId);
            ViewData["TipoContratoEstagioId"] = new SelectList(_context.TipoContratoEstagio, "TipoContratoEstagioId", "Nome", estagio.TipoContratoEstagioId);
            return View(estagio);
        }

        // POST: Estagio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstagioId,RA,Nome,CursoId,TipoContratoEstagioId,EmpresaId,Integradora,VigenciaInicio,VigenciaTermino,Apolice,Seguradora")] Estagio estagio)
        {
            if (id != estagio.EstagioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estagio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstagioExists(estagio.EstagioId))
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
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome", estagio.CursoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaId", "CNPJ", estagio.EmpresaId);
            ViewData["TipoContratoEstagioId"] = new SelectList(_context.TipoContratoEstagio, "TipoContratoEstagioId", "Nome", estagio.TipoContratoEstagioId);
            return View(estagio);
        }

        // GET: Estagio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estagio = await _context.Estagio
                .Include(e => e.Curso)
                .Include(e => e.Empresa)
                .Include(e => e.TipoContratoEstagio)
                .FirstOrDefaultAsync(m => m.EstagioId == id);
            if (estagio == null)
            {
                return NotFound();
            }

            return View(estagio);
        }

        // POST: Estagio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estagio = await _context.Estagio.FindAsync(id);
            if (estagio != null)
            {
                _context.Estagio.Remove(estagio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult ExportarExcel()
        {
            try
            {
                var estagios = _context.Estagio
                    .Include(e => e.Curso)
                    .Include(e => e.TipoContratoEstagio)
                    .Include(e => e.Empresa)
                    .OrderByDescending(e => e.VigenciaInicio)
                    .ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Estágios");

                    // Definir largura das colunas
                    worksheet.Column(1).Width = 12;  // RA
                    worksheet.Column(2).Width = 25;  // Nome
                    worksheet.Column(3).Width = 20;  // Curso
                    worksheet.Column(4).Width = 20;  // Tipo Contrato
                    worksheet.Column(5).Width = 18;  // Empresa (CNPJ)
                    worksheet.Column(6).Width = 20;  // Integradora
                    worksheet.Column(7).Width = 15;  // Vigência Início
                    worksheet.Column(8).Width = 15;  // Vigência Término
                    worksheet.Column(9).Width = 18;  // Apólice
                    worksheet.Column(10).Width = 18; // Seguradora

                    // Adicionar header
                    var headerRow = worksheet.Row(1);
                    headerRow.Height = 25;

                    var headers = new[]
                    {
                "RA",
                "Nome do Aluno",
                "Curso",
                "Tipo de Contrato",
                "Empresa (CNPJ)",
                "Integradora",
                "Vigência Início",
                "Vigência Término",
                "Apólice",
                "Seguradora"
            };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = worksheet.Cell(1, i + 1);
                        cell.Value = headers[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Font.FontColor = XLColor.White;
                        cell.Style.Fill.BackgroundColor = XLColor.FromArgb(0x128C7E); // Verde escuro
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }

                    // Adicionar dados
                    int row = 2;
                    foreach (var estagio in estagios)
                    {
                        worksheet.Cell(row, 1).Value = estagio.RA;
                        worksheet.Cell(row, 2).Value = estagio.Nome;
                        worksheet.Cell(row, 3).Value = estagio.Curso?.Nome ?? "-";
                        worksheet.Cell(row, 4).Value = estagio.TipoContratoEstagio?.Nome ?? "-";
                        worksheet.Cell(row, 5).Value = estagio.Empresa?.CNPJ ?? "-";
                        worksheet.Cell(row, 6).Value = estagio.Integradora ?? "-";
                        worksheet.Cell(row, 7).Value = estagio.VigenciaInicio?.ToString("dd/MM/yyyy") ?? "-";
                        worksheet.Cell(row, 8).Value = estagio.VigenciaTermino?.ToString("dd/MM/yyyy") ?? "-";
                        worksheet.Cell(row, 9).Value = estagio.Apolice ?? "-";
                        worksheet.Cell(row, 10).Value = estagio.Seguradora ?? "-";

                        // Estilo das células de dados
                        for (int col = 1; col <= 10; col++)
                        {
                            var cell = worksheet.Cell(row, col);
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.OutsideBorderColor = XLColor.FromArgb(0xE5E7EB); // Cinza claro
                        }

                        // Alternância de cores nas linhas
                        if (row % 2 == 0)
                        {
                            for (int col = 1; col <= 10; col++)
                            {
                                worksheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.FromArgb(0xF9FAFB);
                            }
                        }

                        row++;
                    }

                    // Congelar header
                    worksheet.SheetView.FreezeRows(1);

                    // Adicionar tabela Excel para filtros automáticos
                    if (estagios.Count > 0)
                    {
                        var lastRow = estagios.Count + 1;
                        var range = worksheet.Range($"A1:J{lastRow}");
                        range.CreateTable("TabelaEstagios");
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Estagiarios_{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")):ddMMyyyy_HHmmss}.xlsx"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao exportar: {ex.Message}");
            }
        }


        private bool EstagioExists(int id)
        {
            return _context.Estagio.Any(e => e.EstagioId == id);
        }
    }
}
