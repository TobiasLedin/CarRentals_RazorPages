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
            ViewData["NavBar"] = "NoDisplay";
            LoginData.Action = "login";

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            ViewData["NavBar"] = "NoDisplay";
            HttpContext.Session.Remove(sessionAdmin);
            LoginData.Action = "logout";

            return Page();
        }

        public IActionResult OnPost()
        {
            LoginData.Admin = _adminRepo.GetByEmail(LoginData.Email);
            if (LoginData.Admin != null && LoginData.Admin.Password == LoginData.Password)
            {
                HttpContext.Session.SetInt32(sessionAdmin, LoginData.Admin.AdminId);    // Session state (Key: "_admin", Value: AdminId)

                if (HttpContext.Session.TryGetValue("_customer", out _))     // Search and close customer session if present.
                {
                    HttpContext.Session.Remove("_customer");
                }

                return RedirectToPage("/Admins/Overview");
            }

            if (LoginData.Admin == null)
            {
                ViewData["Fail"] = "There is no account with this email!";
            }
            else if (LoginData.Admin != null && LoginData.Admin.Password != LoginData.Password)
            {
                ViewData["Fail"] = "The email and password does not match!";
            }

            LoginData.Action = "login";
            return Page();
        }
    }
}
