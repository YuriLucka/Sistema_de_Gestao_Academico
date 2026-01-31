using CAA.Data;
using CAA.Helpers;
using CAA.Models;
using ClosedXML.Excel;
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
    [Authorize(Roles = "Matrículas, Admin")]
    public class MatriculaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatriculaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matricula
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matricula.Include(m => m.Atendente).Include(m => m.Curso).Include(m => m.Eixo).Include(m => m.StatusMatricula).Include(m => m.TipoIngresso);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula
                .Include(m => m.Atendente)
                .Include(m => m.Curso)
                .Include(m => m.Eixo)
                .Include(m => m.StatusMatricula)
                .Include(m => m.TipoIngresso)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matricula/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(
                _context.Users.Select(u => new { u.Id, NomeCompleto = u.Nome + " " + u.Sobrenome }),
                "Id",
                "NomeCompleto"
            );

            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome");
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome");
            ViewData["StatusMatriculaId"] = new SelectList(_context.StatusMatricula, "StatusMatriculaId", "Nome");
            ViewData["TipoIngressoId"] = new SelectList(_context.TipoIngresso, "TipoIngressoId", "Nome");
            return View();
        }

        // POST: Matricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatriculaId,DataMatricula,NomeCompleto,Email,Celular,EscolaOrigem,Cidade,AnoFormacao,EixoId,CursoId,Turno,Modalidade,StatusMatriculaId,TipoIngressoId,InstituicaoTransferencia,UsuarioId,Brinde,Observacao,Motivacao")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(
                _context.Users.Select(u => new { u.Id, NomeCompleto = u.Nome + " " + u.Sobrenome }),
                "Id",
                "NomeCompleto",
                matricula.UsuarioId
            );
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome", matricula.CursoId);
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", matricula.EixoId);
            ViewData["StatusMatriculaId"] = new SelectList(_context.StatusMatricula, "StatusMatriculaId", "Nome", matricula.StatusMatriculaId);
            ViewData["TipoIngressoId"] = new SelectList(_context.TipoIngresso, "TipoIngressoId", "Nome", matricula.TipoIngressoId);
            return View(matricula);
        }

        // GET: Matricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(
                _context.Users.Select(u => new { u.Id, NomeCompleto = u.Nome + " " + u.Sobrenome }),
                "Id",
                "NomeCompleto",
                matricula.UsuarioId
            );
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome", matricula.CursoId);
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", matricula.EixoId);
            ViewData["StatusMatriculaId"] = new SelectList(_context.StatusMatricula, "StatusMatriculaId", "Nome", matricula.StatusMatriculaId);
            ViewData["TipoIngressoId"] = new SelectList(_context.TipoIngresso, "TipoIngressoId", "Nome", matricula.TipoIngressoId);
            return View(matricula);
        }

        // POST: Matricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatriculaId,DataMatricula,NomeCompleto,Email,Celular,EscolaOrigem,Cidade,AnoFormacao,EixoId,CursoId,Turno,Modalidade,StatusMatriculaId,TipoIngressoId,InstituicaoTransferencia,UsuarioId,Brinde,Observacao,Motivacao")] Matricula matricula)
        {
            if (id != matricula.MatriculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.MatriculaId))
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
            ViewData["UsuarioId"] = new SelectList(
                _context.Users.Select(u => new { u.Id, NomeCompleto = u.Nome + " " + u.Sobrenome }),
                "Id",
                "NomeCompleto",
                matricula.UsuarioId
            );
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Nome", matricula.CursoId);
            ViewData["EixoId"] = new SelectList(_context.Eixo, "EixoId", "Nome", matricula.EixoId);
            ViewData["StatusMatriculaId"] = new SelectList(_context.StatusMatricula, "StatusMatriculaId", "Nome", matricula.StatusMatriculaId);
            ViewData["TipoIngressoId"] = new SelectList(_context.TipoIngresso, "TipoIngressoId", "Nome", matricula.TipoIngressoId);
            return View(matricula);
        }

        // GET: Matricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matricula
                .Include(m => m.Atendente)
                .Include(m => m.Curso)
                .Include(m => m.Eixo)
                .Include(m => m.StatusMatricula)
                .Include(m => m.TipoIngresso)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matricula.FindAsync(id);
            if (matricula != null)
            {
                _context.Matricula.Remove(matricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: DataTable Server-Side
        [HttpPost]
        public IActionResult GetMatriculasDataTable()
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
                var matriculasQuery = _context.Matricula
                    .Include(m => m.Atendente)
                    .Include(m => m.Curso)
                    .Include(m => m.Eixo)
                    .Include(m => m.StatusMatricula)
                    .Include(m => m.TipoIngresso)
                    .AsQueryable();

                // Total de registros (antes do filtro)
                int recordsTotal = matriculasQuery.Count();

                // Aplicar busca
                if (!string.IsNullOrEmpty(searchValue))
                {
                    matriculasQuery = matriculasQuery.Where(m =>
                        m.NomeCompleto.Contains(searchValue) ||
                        (m.Email != null && m.Email.Contains(searchValue)) ||
                        (m.Celular != null && m.Celular.Contains(searchValue)) ||
                        (m.EscolaOrigem != null && m.EscolaOrigem.Contains(searchValue)) ||
                        (m.Cidade != null && m.Cidade.Contains(searchValue)) ||
                        (m.Eixo != null && m.Eixo.Nome.Contains(searchValue)) ||
                        (m.Curso != null && m.Curso.Nome.Contains(searchValue)) ||
                        (m.InstituicaoTransferencia != null && m.InstituicaoTransferencia.Contains(searchValue)) ||
                        (m.Observacao != null && m.Observacao.Contains(searchValue)) ||
                        (m.Motivacao != null && m.Motivacao.Contains(searchValue))
                    );
                }

                // Total após filtro
                int recordsFiltered = matriculasQuery.Count();

                // Aplicar ordenação
                if (!string.IsNullOrEmpty(sortColumnIndex))
                {
                    switch (sortColumnIndex)
                    {
                        case "0": // DataMatricula
                            matriculasQuery = sortDirection == "asc"
                                ? matriculasQuery.OrderBy(m => m.DataMatricula)
                                : matriculasQuery.OrderByDescending(m => m.DataMatricula);
                            break;
                        case "1": // NomeCompleto
                            matriculasQuery = sortDirection == "asc"
                                ? matriculasQuery.OrderBy(m => m.NomeCompleto)
                                : matriculasQuery.OrderByDescending(m => m.NomeCompleto);
                            break;
                        case "7": // Eixo
                            matriculasQuery = sortDirection == "asc"
                                ? matriculasQuery.OrderBy(m => m.Eixo.Nome)
                                : matriculasQuery.OrderByDescending(m => m.Eixo.Nome);
                            break;
                        case "8": // Curso
                            matriculasQuery = sortDirection == "asc"
                                ? matriculasQuery.OrderBy(m => m.Curso.Nome)
                                : matriculasQuery.OrderByDescending(m => m.Curso.Nome);
                            break;
                        default:
                            matriculasQuery = matriculasQuery.OrderByDescending(m => m.DataMatricula);
                            break;
                    }
                }
                else
                {
                    matriculasQuery = matriculasQuery.OrderByDescending(m => m.DataMatricula);
                }

                // Aplicar paginação e projetar dados
                var data = matriculasQuery
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(m => new
                    {
                        matriculaId = m.MatriculaId,
                        dataMatricula = m.DataMatricula.HasValue ? m.DataMatricula.Value.ToString("dd/MM/yyyy") : "",
                        nomeCompleto = m.NomeCompleto ?? "",
                        email = m.Email ?? "",
                        celular = m.Celular ?? "",
                        escolaOrigem = m.EscolaOrigem ?? "",
                        cidade = m.Cidade ?? "",
                        anoFormacao = m.AnoFormacao.HasValue ? m.AnoFormacao.Value.ToString() : "",
                        eixo = m.Eixo != null ? m.Eixo.Nome : "",
                        curso = m.Curso != null ? m.Curso.Nome : "",
                        turno = m.Turno.ToString(),
                        modalidade = (((CAA.Models.Titulacao)m.Modalidade).GetDisplayName()),
                        statusMatricula = m.StatusMatricula != null ? m.StatusMatricula.Nome : "",
                        tipoIngresso = m.TipoIngresso != null ? m.TipoIngresso.Nome : "",
                        instituicaoTransferencia = m.InstituicaoTransferencia ?? "",
                        atendenteId = m.Atendente != null ? m.Atendente.Nome + " " + m.Atendente.Sobrenome : "",
                        brinde = m.Brinde ? "Sim" : "Não",
                        observacao = m.Observacao ?? "",
                        motivacao = m.Motivacao ?? ""
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

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            try
            {
                var matriculas = _context.Matricula
                    .Include(m => m.Atendente)
                    .Include(m => m.Curso)
                    .Include(m => m.Eixo)
                    .Include(m => m.StatusMatricula)
                    .Include(m => m.TipoIngresso)
                    .OrderByDescending(m => m.DataMatricula)
                    .ToList();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Matrículas");

                    // Definir largura das colunas
                    worksheet.Column(1).Width = 15;  // Data Matrícula
                    worksheet.Column(2).Width = 25;  // Nome Completo
                    worksheet.Column(3).Width = 20;  // Email
                    worksheet.Column(4).Width = 15;  // Celular
                    worksheet.Column(5).Width = 20;  // Escola Origem
                    worksheet.Column(6).Width = 15;  // Cidade
                    worksheet.Column(7).Width = 12;  // Ano Formação
                    worksheet.Column(8).Width = 15;  // Eixo
                    worksheet.Column(9).Width = 20;  // Curso
                    worksheet.Column(10).Width = 12; // Turno
                    worksheet.Column(11).Width = 15; // Modalidade
                    worksheet.Column(12).Width = 15; // Status
                    worksheet.Column(13).Width = 15; // Tipo Ingresso
                    worksheet.Column(14).Width = 20; // Instituição Transf.
                    worksheet.Column(15).Width = 15; // Atendente
                    worksheet.Column(16).Width = 10; // Brinde
                    worksheet.Column(17).Width = 25; // Observação
                    worksheet.Column(18).Width = 25; // Motivação

                    // Adicionar header
                    var headerRow = worksheet.Row(1);
                    headerRow.Height = 25;

                    var headers = new[]
                    {
                "Data Matrícula",
                "Nome Completo",
                "Email",
                "Celular",
                "Escola Origem",
                "Cidade",
                "Ano Formação",
                "Eixo",
                "Curso",
                "Turno",
                "Modalidade",
                "Status",
                "Tipo Ingresso",
                "Instituição Transf.",
                "Atendente",
                "Brinde",
                "Observação",
                "Motivação"
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
                        cell.Style.Font.FontSize = 11;
                    }

                    // Adicionar dados
                    int row = 2;
                    foreach (var matricula in matriculas)
                    {
                        worksheet.Cell(row, 1).Value = matricula.DataMatricula.HasValue ? matricula.DataMatricula.Value.ToString("dd/MM/yyyy") : "";
                        worksheet.Cell(row, 2).Value = matricula.NomeCompleto ?? "";
                        worksheet.Cell(row, 3).Value = matricula.Email ?? "";
                        worksheet.Cell(row, 4).Value = matricula.Celular ?? "";
                        worksheet.Cell(row, 5).Value = matricula.EscolaOrigem ?? "";
                        worksheet.Cell(row, 6).Value = matricula.Cidade ?? "";
                        worksheet.Cell(row, 7).Value = matricula.AnoFormacao.HasValue ? matricula.AnoFormacao.Value.ToString() : "";
                        worksheet.Cell(row, 8).Value = matricula.Eixo?.Nome ?? "";
                        worksheet.Cell(row, 9).Value = matricula.Curso?.Nome ?? "";
                        worksheet.Cell(row, 10).Value = matricula.Turno.ToString();
                        worksheet.Cell(row, 11).Value = matricula.Modalidade.ToString();
                        worksheet.Cell(row, 12).Value = matricula.StatusMatricula?.Nome ?? "";
                        worksheet.Cell(row, 13).Value = matricula.TipoIngresso?.Nome ?? "";
                        worksheet.Cell(row, 14).Value = matricula.InstituicaoTransferencia ?? "";
                        worksheet.Cell(row, 15).Value = matricula.Atendente?.Id ?? "";
                        worksheet.Cell(row, 16).Value = matricula.Brinde ? "Sim" : "Não";
                        worksheet.Cell(row, 17).Value = matricula.Observacao ?? "";
                        worksheet.Cell(row, 18).Value = matricula.Motivacao ?? "";

                        // Estilo das células de dados
                        for (int col = 1; col <= 18; col++)
                        {
                            var cell = worksheet.Cell(row, col);
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.OutsideBorderColor = XLColor.FromArgb(0xE5E7EB); // Cinza claro
                            cell.Style.Font.FontSize = 10;
                        }

                        // Alternância de cores nas linhas
                        if (row % 2 == 0)
                        {
                            for (int col = 1; col <= 18; col++)
                            {
                                worksheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.FromArgb(0xF9FAFB);
                            }
                        }

                        // Centralizar células específicas
                        worksheet.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Data
                        worksheet.Cell(row, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Ano
                        worksheet.Cell(row, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Turno
                        worksheet.Cell(row, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Modalidade
                        worksheet.Cell(row, 16).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Brinde

                        row++;
                    }

                    // Congelar header
                    worksheet.SheetView.FreezeRows(1);

                    // Adicionar tabela Excel para filtros automáticos
                    if (matriculas.Count > 0)
                    {
                        var lastRow = matriculas.Count + 1;
                        var range = worksheet.Range($"A1:R{lastRow}");
                        range.CreateTable("TabelaMatriculas");
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Matriculas_{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")):ddMMyyyy_HHmmss}.xlsx"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao exportar: {ex.Message}");
            }
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matricula.Any(e => e.MatriculaId == id);
        }
    }
}
