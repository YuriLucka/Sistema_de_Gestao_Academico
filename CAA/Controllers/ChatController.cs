using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using CAA.Data;
using CAA.Models;
using Microsoft.EntityFrameworkCore;

namespace CAA.Controllers
{
    [Authorize(Roles = "Recados, Admin")]
    public class ChatController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatController(UserManager<Usuario> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            // Otimização: carregar apenas ID, Nome, Sobrenome e Foto (projeção)
            var usuarios = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Nome,
                    u.Sobrenome,
                    u.FotoPerfil,
                    u.UserName
                })
                .ToListAsync();

            var mensagensNaoLidas = await _context.Mensagem
                .Where(m => m.DestinatarioId == userId && !m.Lida)
                .GroupBy(m => m.RemetenteId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            ViewBag.Usuarios = usuarios;
            ViewBag.MensagensNaoLidas = mensagensNaoLidas;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarcarMensagensLidas([FromBody] MarcarLidasRequest req)
        {
            var userId = _userManager.GetUserId(User);
            var mensagens = _context.Mensagem
                .Where(m => m.RemetenteId == req.UsuarioId && m.DestinatarioId == userId && !m.Lida);

            foreach (var mensagem in mensagens)
                mensagem.Lida = true;

            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Endpoint otimizado: carrega mensagens paginadas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> MensagensPrivadas(string usuarioId, int pagina = 1, int tamanho = 30)
        {
            var userId = _userManager.GetUserId(User);

            // Total de mensagens (para saber se há mais)
            var totalMensagens = await _context.Mensagem
                .Where(m => (m.RemetenteId == userId && m.DestinatarioId == usuarioId)
                         || (m.RemetenteId == usuarioId && m.DestinatarioId == userId))
                .CountAsync();

            // Buscar mensagens em ordem DECRESCENTE (mais recentes primeiro)
            // Depois reverter no cliente para exibir ordem crescente
            var mensagens = await _context.Mensagem
                .Where(m => (m.RemetenteId == userId && m.DestinatarioId == usuarioId)
                         || (m.RemetenteId == usuarioId && m.DestinatarioId == userId))
                .OrderByDescending(m => m.DataEnvio)
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .Select(m => new
                {
                    m.MensagemId,
                    m.Conteudo,
                    m.RemetenteId,
                    m.DataEnvio
                })
                .ToListAsync();

            // Buscar nomes de usuários envolvidos (apenas necessários)
            var idsUsuarios = mensagens.Select(m => m.RemetenteId).Distinct().ToList();
            var dicUsuarios = await _context.Users
                .Where(u => idsUsuarios.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.UserName);

            var result = mensagens.Select(m => new
            {
                m.MensagemId,
                conteudo = m.Conteudo,
                remetenteId = m.RemetenteId,
                remetenteUserName = dicUsuarios.ContainsKey(m.RemetenteId) ? dicUsuarios[m.RemetenteId] : "",
                dataEnvio = m.DataEnvio
            }).Reverse(); // Reverter para ordem crescente no cliente

            return Json(new
            {
                mensagens = result,
                totalMensagens = totalMensagens,
                paginaAtual = pagina,
                temMais = totalMensagens > (pagina * tamanho)
            });
        }

        public class MarcarLidasRequest
        {
            public string UsuarioId { get; set; }
        }
    }
}
