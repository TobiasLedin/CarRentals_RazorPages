using FribergCarRentals.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class OverviewModel : PageModel
    {
        private readonly IAuthService _auth;

        public OverviewModel(IAuthService auth)
        {
            _auth = auth;
        }


        public IActionResult OnGet()
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            return Page();
        }
    }
}
