using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.AdminPages
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public int VehicleId { get; set; }

        [BindProperty]
        public string Action { get; set; }

        public void OnGetVehicles()
        {
            if (!HttpContext.Session.TryGetValue("_admin", out _))
            {
                ViewData["type"] = "NoAdmin";
            }
            else
            {
                ViewData["type"] = "Vehicle";
            }
        }

        public void OnGetCustomers()
        {
            if (!HttpContext.Session.TryGetValue("_admin", out _))
            {
                ViewData["type"] = "NoAdmin";
            }
            else
            {
                ViewData["type"] = "Customer";
            }
        }

        public void OnGetBookings()
        {
            if (!HttpContext.Session.TryGetValue("_admin", out _))
            {
                ViewData["type"] = "NoAdmin";
            }
            else
            {
                ViewData["type"] = "Booking";
            }
        }

        public void OnPost()
        {
            var id = VehicleId;
            var action = Action;
        }
        
    }
}
