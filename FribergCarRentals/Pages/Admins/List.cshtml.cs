using FribergCarRentals.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class ListModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IAuthService _auth;

        public ListModel(IVehicleRepository vehicleRepo, IBookingRepository bookingRepo, ICustomerRepository customerRepo, IAuthService auth)
        {
            _vehicleRepo = vehicleRepo;
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
            _auth = auth;
            Lists = new ListAllVM();
        }

        public ListAllVM Lists { get; set; }

        // Type = Model to list.
        // User = Customize data and options according admin/customer/visitor.

        public IActionResult OnGetVehicles()
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            Lists.Type = "vehicle";
            Lists.Vehicles = _vehicleRepo.GetAll();
            return Page();
        }

        public IActionResult OnGetCustomers()
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            Lists.Type = "customer";
            Lists.Customers = _customerRepo.GetAll();
            return Page();
        }

        public IActionResult OnGetBookings()
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                TempData["expired"] = result.Message;
                return RedirectToPage("Login");
            }

            Lists.Type = "booking";
            Lists.Bookings = _bookingRepo.GetAll();
            return Page();
        }
    }
}
