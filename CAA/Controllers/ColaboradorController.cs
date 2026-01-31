using CAA.Data;
using CAA.Models;
using CAA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CAA.Controllers
{
    [Authorize(Roles = "Colaboradores, Admin")]
    public class ColaboradorController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public ColaboradorController(ApplicationDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, userManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ColaboradorCreateViewModel();
            model.AllRoles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColaboradorCreateViewModel model)
        {
            model.AllRoles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            if (ModelState.IsValid)
            {
                var colaborador = new Usuario
                {
                    Nome = model.Nome.ToUpperInvariant(),
                    Sobrenome = model.Sobrenome.ToUpperInvariant(),
                    Cargo = model.Cargo.ToUpperInvariant(),
                    Departamento = model.Departamento.ToUpperInvariant(),
                    DataNascimento = model.DataNascimento,
                    Email = model.Email,
                    EmailConfirmed = true,
                    UserName = model.Email,
                    DataCadastro = DateTime.UtcNow
                };

                // Processa a imagem, se enviada
                if (model.FotoPerfil != null && model.FotoPerfil.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.FotoPerfil.CopyToAsync(ms);
                        colaborador.FotoPerfil = ms.ToArray();
                    }
                }

                var result = await _userManager.CreateAsync(colaborador, model.Password);
                if (result.Succeeded)
                {
                    // Adiciona roles selecionadas
                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        foreach (var role in model.SelectedRoles)
                        {
                            await _userManager.AddToRoleAsync(colaborador, role);
                        }
                        // Atualiza o SecurityStamp se houver roles
                        await _userManager.UpdateSecurityStampAsync(colaborador);
                    }
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var model = new ColaboradorEditViewModel
            {
                Id = user.Id,
                Nome = user.Nome,
                Sobrenome = user.Sobrenome,
                Departamento = user.Departamento,
                Cargo = user.Cargo,
                DataNascimento = user.DataNascimento,
                Email = user.Email,
                IsBlocked = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow,
                Ativo = user.Ativo
            };

            // Carrega roles
            model.AllRoles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            model.SelectedRoles = userRoles.ToList();

            if (user.FotoPerfil != null)
            {
                string imageBase64 = Convert.ToBase64String(user.FotoPerfil);
                string imageUrl = $"data:image/png;base64,{imageBase64}";
                ViewBag.FotoPerfilUrl = imageUrl;
            }
            else
            {
                ViewBag.FotoPerfilUrl = null;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ColaboradorEditViewModel model)
        {
            model.AllRoles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Erro em {key}: {error.ErrorMessage}");
                    }
                }
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            user.Nome = model.Nome.ToUpperInvariant();
            user.Sobrenome = model.Sobrenome.ToUpperInvariant();
            user.Cargo = model.Cargo.ToUpperInvariant();
            user.Departamento = model.Departamento.ToUpperInvariant();
            user.DataNascimento = model.DataNascimento;
            user.Ativo = model.Ativo;

            // Atualiza foto de perfil se enviada
            if (model.FotoPerfil != null && model.FotoPerfil.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.FotoPerfil.CopyToAsync(ms);
                    user.FotoPerfil = ms.ToArray();
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                // Atualiza roles e SecurityStamp se necessário
                var userRoles = await _userManager.GetRolesAsync(user);
                var rolesToAdd = model.SelectedRoles.Except(userRoles).ToList();
                var rolesToRemove = userRoles.Except(model.SelectedRoles).ToList();
                bool rolesChanged = false;
                if (rolesToAdd.Any())
                {
                    await _userManager.AddToRolesAsync(user, rolesToAdd);
                    rolesChanged = true;
                }
                if (rolesToRemove.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    rolesChanged = true;
                }
                if (rolesChanged)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                // Permanece na tela de edição após salvar
                return RedirectToAction("Edit", new { id = user.Id });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unblock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Edit", new { id = user.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // Remover todas as roles do usuário antes de deletar
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, userRoles);

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                // Redireciona de volta para edição se falhar
                return RedirectToAction("Edit", new { id });
            }
            return RedirectToAction("Index");
        }
    }
}
