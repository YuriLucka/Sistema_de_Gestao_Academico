using CAA.Models;
using CAA.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CAA.ViewComponents
{
    /// <summary>
    /// ViewComponent responsável por exibir o perfil do usuário autenticado.
    /// </summary>
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UserProfileViewComponent(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                return View("RedirectToLogin");
            }

            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return View("RedirectToLogin");
            }

            var model = new UserProfileViewModel
            {
                Nome = user.Nome,
                Sobrenome = user.Sobrenome,
                Departamento = user.Departamento,
                Cargo = user.Cargo,
                Email = user.Email,
                FotoPerfilBase64 = user.FotoPerfil != null ? $"data:image/png;base64,{Convert.ToBase64String(user.FotoPerfil)}" : null
            };
            return View(model);
        }
    }
}
