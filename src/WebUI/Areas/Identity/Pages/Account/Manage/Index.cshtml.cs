
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using seminario.Domain.Entities;

namespace seminario.WebUI.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "Debe ingresar su nombre.")]
            [StringLength(128, ErrorMessage = "El nombre debe contener de {2} a {1} caracteres.", MinimumLength = 1)]
            [Display(Name = "Nombre")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Debe ingresar su apellido.")]
            [StringLength(128, ErrorMessage = "El apellido debe contener de {2} a {1} caracteres.", MinimumLength = 1)]
            [Display(Name = "Apellido")]
            public string LastName { get; set; }

            [Phone(ErrorMessage = "Numero de telefono inválido.")]
            [Display(Name = "Telefono")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != user.PhoneNumber
                || Input.FirstName != user.FirstName
                || Input.LastName != user.LastName)
            {
                user.PhoneNumber = Input.PhoneNumber;
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                var updateProfileResult = await _userManager.UpdateAsync(user);
                if (!updateProfileResult.Succeeded)
                {
                    StatusMessage = "Error inesperado al actualizar.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Perfil actualizado.";
            return RedirectToPage();
        }
    }
}
