using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class ListModel : PageModel
    {
        public string Type { get; set; } = default!;
        public string UserType { get; set; } = default!;



        public void OnGetVehicles()
        {
            Type = "Vehicle";

            if(HttpContext.Session.TryGetValue("_admin", out _))
            {
                UserType = "admin";
            }
            else if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                UserType = "customer";
            }
            else
            {
                UserType = "visitor";
            }
        }

        public void OnGetCustomers()
        {
            Type = "Customer";

            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                UserType = "admin";
            }
            else if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                UserType = "customer";
            }
            else
            {
                UserType = "visitor";
            }
        }

        public void OnGetBookings()
        {
            Type = "Booking";

            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                UserType = "admin";
            }
            else if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                UserType = "customer";
            }
            else
            {
                UserType = "visitor";
            }
        }
    }
}
