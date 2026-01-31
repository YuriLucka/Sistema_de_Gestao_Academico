// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CAA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace CAA.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Usuario> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Campo obrigatório.")]
            [EmailAddress(ErrorMessage = "E-mail inválido.")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Não revela se o usuário não existe ou não está confirmado
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // Para mais informações sobre como habilitar confirmação de conta e redefinição de senha
                // acesse https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                string corpoEmailHtml = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "RedefinirSenha.cshtml"), System.Text.Encoding.UTF8);
                // Se o usuário tem foto, converte para base64, senão usa uma imagem padrão
                string fotoPerfilBase64;
                if (user.FotoPerfil != null && user.FotoPerfil.Length > 0)
                {
                    fotoPerfilBase64 = $"data:image/png;base64,{Convert.ToBase64String(user.FotoPerfil)}";
                }
                else
                {
                    // Use uma URL absoluta para imagem padrão (acessível publicamente)
                    fotoPerfilBase64 = "https://yurilucka.bsite.net/EmailTemplates/EmailImage?imgName=placeholderPerfil.png";
                }

                corpoEmailHtml = corpoEmailHtml
                    .Replace("[LINK]", HtmlEncoder.Default.Encode(callbackUrl))
                    .Replace("[NOME]", user.Nome)
                    .Replace("[SOBRENOME]", user.Sobrenome)
                    .Replace("[FOTO_PERFIL]", fotoPerfilBase64);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Redefinir senha",
                    corpoEmailHtml);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
