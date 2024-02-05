using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IBookingRepository _bookingRepo;

        public DeleteModel(IVehicleRepository vehicleRepo, ICustomerRepository customerRepo, IBookingRepository bookingRepo)
        {
            _vehicleRepo = vehicleRepo;
            _customerRepo = customerRepo;
            _bookingRepo = bookingRepo;
            Object = new DeleteVM();
        }

        [BindProperty]
        public DeleteVM Object { get; set; }


        public IActionResult OnGetVehicle(int id)
        {
            Object.Vehicle = _vehicleRepo.GetById(id);
            Object.Type = "Vehicle";
            return Page();
        }

        public IActionResult OnGetCustomer(int id)
        {
            Object.Customer = _customerRepo.GetById(id);
            Object.Type = "Customer";
            return Page();
        }

        public IActionResult OnGetBooking(int id)
        {
            Object.Booking = _bookingRepo.GetById(id);
            Object.Type = "Booking";
            return Page();
        }

        public IActionResult OnPostVehicle()
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
