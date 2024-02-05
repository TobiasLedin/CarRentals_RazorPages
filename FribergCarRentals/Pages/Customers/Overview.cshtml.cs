using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Customers
{
    public class OverviewModel : PageModel
    {
        public void OnGet()
        {
            if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                ViewData["type"] = "Customer";
            }
            else
            {
                ViewData["type"] = "NoCustomer";
            }
        }
    }
}
