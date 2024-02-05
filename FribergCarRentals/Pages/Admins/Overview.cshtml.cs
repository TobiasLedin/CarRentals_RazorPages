using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class OverviewModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                ViewData["type"] = "Admin";
            }
            else
            {
                ViewData["type"] = "NoAdmin";
            }
        }
    }
}
