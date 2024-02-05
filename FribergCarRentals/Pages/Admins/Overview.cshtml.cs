using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.AdminPages
{
    public class OverviewModel : PageModel
    {
        public void OnGet()
        {
            if (!HttpContext.Session.TryGetValue("_admin", out _))
            {
                ViewData["type"] = "NoAdmin";
            }
            else
            {
                ViewData["type"] = "Admin";
            }
        }
        public IActionResult OnGetVehicle(int id, string action)
        {
            var test = id;
            var test1 = action;
            return Page();
        }
    }
}
