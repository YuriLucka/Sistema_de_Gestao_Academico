using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using CAA.Data;
using CAA.Models;

namespace CAA.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ChatHub(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnviarMensagem(string destinatarioId, string conteudo)
        {
            var remetenteId = Context.UserIdentifier;
            var mensagem = new Mensagem
            {
                MensagemId = Guid.NewGuid().ToString(),
                Conteudo = conteudo,
                RemetenteId = remetenteId,
                DestinatarioId = destinatarioId,
                DataEnvio = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")),
                Lida = false
            };
            _context.Mensagem.Add(mensagem);
            await _context.SaveChangesAsync();

            var remetenteUser = await _userManager.FindByIdAsync(remetenteId);
            var remetenteUserName = remetenteUser?.Nome ?? "";

            // Notifica apenas o destinatário
            await Clients.User(destinatarioId).SendAsync("NotificacaoMensagem", remetenteUserName, conteudo);
            await Clients.User(destinatarioId).SendAsync("ReceberMensagem", remetenteId, conteudo);
            await Clients.User(destinatarioId).SendAsync("AtualizarNaoLidas", remetenteId);
            await Clients.User(remetenteId).SendAsync("ReceberMensagem", remetenteId, conteudo);
        }
    }
}
