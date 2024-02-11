using FribergCarRentals.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Customers
{
    public class ListModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IAuthService _auth;

        public ListModel(IVehicleRepository vehicleRepo, IBookingRepository bookingRepo, IAuthService auth)
        {
            _vehicleRepo = vehicleRepo;
            _bookingRepo = bookingRepo;
            _auth = auth;
            Lists = new ListAllVM();
        }

        public ListAllVM Lists { get; set; }

        // Type = Model to list.
        // User = Customize data and options according admin/customer/visitor.

        public IActionResult OnGetVehicles()
        {
            var result = _auth.CheckCustomerAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            Lists.Vehicles = _vehicleRepo.GetAll();
            Lists.Type = "Vehicles";
            return Page();
        }

        public IActionResult OnGetBookings()
        {
            var result = _auth.CheckCustomerAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            Lists.Bookings = _bookingRepo.GetAllByCustomer(result.Id);
            Lists.Type = "Bookings";
            return Page();
        }
    }
}
