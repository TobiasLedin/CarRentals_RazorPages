using FribergCarRentals.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class DeleteModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IAuthService _auth;

        public DeleteModel(IVehicleRepository vehicleRepo, ICustomerRepository customerRepo, IBookingRepository bookingRepo, IAuthService auth)
        {
            _vehicleRepo = vehicleRepo;
            _customerRepo = customerRepo;
            _bookingRepo = bookingRepo;
            _auth = auth;
            Object = new DeleteVM();
        }

        [BindProperty]
        public DeleteVM Object { get; set; }


        public IActionResult OnGetVehicle(int id)
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Vehicle = _vehicleRepo.GetById(id);
            Object.Type = "Vehicle";
            return Page();
        }

        public IActionResult OnGetCustomer(int id)
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Customer = _customerRepo.GetById(id);
            Object.Type = "Customer";
            return Page();
        }

        public IActionResult OnGetBooking(int id)
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Booking = _bookingRepo.GetById(id);
            Object.Type = "Booking";
            return Page();
        }

        public IActionResult OnPostVehicle()        // TODO: Fixa auth for Post?
        {
            _vehicleRepo.Delete(Object.Vehicle.VehicleId);

            return RedirectToPage("List", "Vehicles");
        }

        public IActionResult OnPostCustomer()
        {
            _customerRepo.Delete(Object.Customer.CustomerId);

            return RedirectToPage("List", "Customers");
        }

        public IActionResult OnPostBooking()
        {
            _bookingRepo.Delete(Object.Booking.BookingId);

            return RedirectToPage("List", "Bookings");
        }
    }
}
