using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CAA.Services
{
    /// <summary>
    /// Serviço responsável pelo envio de e-mails utilizando SMTP.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        // Configurações de SMTP obtidas do appsettings.json
        private readonly IConfiguration _config;

        /// <summary>
        /// Injeta as configurações necessárias para o envio de e-mails.
        /// </summary>
        /// <param name="config">Configuração da aplicação (IConfiguration)</param>
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Envia um e-mail assíncrono utilizando as configurações SMTP.
        /// </summary>
        /// <param name="email">Destinatário</param>
        /// <param name="subject">Assunto do e-mail</param>
        /// <param name="htmlMessage">Corpo do e-mail em HTML</param>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Cria o cliente SMTP com as configurações do appsettings.json
            var smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Pass"]),
                EnableSsl = true,
            };

            // Monta a mensagem de e-mail
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Smtp:From"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            // Envia o e-mail de forma assíncrona
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
