using FribergCarRentals.Interfaces;
using FribergCarRentals.Services;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _auth;

        public LoginModel(IAdminRepository adminRepo, IAuthService auth)
        {
            _auth = auth;
            LoginData = new LoginVM();
        }

        [BindProperty]
        public LoginVM LoginData { get; set; }

        public IActionResult OnGet()
        {
            ViewData["NavBar"] = "NoDisplay";
            LoginData.Action = "login";

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            ViewData["NavBar"] = "NoDisplay";
            _auth.RemoveAdminAuth();
            LoginData.Action = "logout";

            return Page();
        }

        public IActionResult OnPost()
        {
            var result = _auth.AdminAuth(LoginData.Email, LoginData.Password);
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("/Admins/Login");
            }

            LoginData.Action = "login";
            return RedirectToPage("/Admins/Overview");
        }
    }
}
