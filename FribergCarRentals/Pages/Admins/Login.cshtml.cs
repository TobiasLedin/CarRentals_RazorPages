using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class LoginModel : PageModel
    {
        private readonly IAdminRepository _adminRepo;
        private const string sessionAdmin = "_admin";

        public LoginModel(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
            LoginData = new LoginVM();
        }

        [BindProperty]
        public LoginVM LoginData { get; set; }

        public IActionResult OnGet()
        {
            LoginData.Action = "login";
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove(sessionAdmin);
            LoginData.Action = "logout";
            return Page();
        }

        public IActionResult OnPost()
        {
            LoginData.Admin = _adminRepo.GetByEmail(LoginData.Email);
            if (LoginData.Admin != null && LoginData.Admin.Password == LoginData.Password)
            {
                // Session state

                HttpContext.Session.SetInt32(sessionAdmin, LoginData.Admin.AdminId);

                // Session state

                return RedirectToPage("/Admins/Overview");
            }
            if (LoginData.Admin == null)
            {
                ViewData["Fail"] = "There is no account with this email!";
            }
            if (LoginData.Admin != null && LoginData.Admin.Password != LoginData.Password)
            {
                ViewData["Fail"] = "The email and password does not match!";
            }
            return Page();
        }
    }
}
