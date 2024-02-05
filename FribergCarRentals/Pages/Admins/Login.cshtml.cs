using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.AdminPages
{
    public class AdminModel : PageModel
    {
        private readonly IAdminRepository _adminRepo;
        private const string sessionAdmin = "_admin";

        public AdminModel(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

        //Properties
        public Admin? Admin { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public IActionResult OnGet()
        {
            ViewData["action"] = "login";
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove(sessionAdmin);
            ViewData["action"] = "logout";
            return Page();
        }

        public IActionResult OnPostLogin(string email, string password)
        {
            Admin = _adminRepo.GetByEmail(email);
            if (Admin != null && Admin.Password == password)
            {
                // Session state

                HttpContext.Session.SetInt32(sessionAdmin, Admin.AdminId);

                // Session state

                return RedirectToPage("Overview");
            }
            if (Admin == null)
            {
                ViewData["Fail"] = "There is no account with this email!";
            }
            if (Admin != null && Admin.Password != password)
            {
                ViewData["Fail"] = "The email and password does not match!";
            }
            return Page();
        }
    }
}
