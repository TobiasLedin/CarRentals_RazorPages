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


        public DeleteVM Object { get; set; }


        public IActionResult OnGetVehicle(int id)
        {
            Object.Vehicle = _vehicleRepo.GetById(id);
            ViewData["type"] = "Vehicle";
            return Page();
        }

        public IActionResult OnGetCustomer(int id)
        {
            Object.Customer = _customerRepo.GetById(id);
            ViewData["type"] = "Customer";
            return Page();
        }

        public IActionResult OnGetBooking(int id)
        {
            Object.Customer = _customerRepo.GetById(id);
            ViewData["type"] = "Customer";
            return Page();
        }

        public IActionResult OnPostVehicle(int id)
        {
            _vehicleRepo.Delete(id);

            return RedirectToPage("List", "Vehicles");
        }

        public IActionResult OnPostCustomer(int id)
        {
            _customerRepo.Delete(id);

            return RedirectToPage("List", "Customers");
        }

        public IActionResult OnPostBooking()
        {
            _customerRepo.Delete(Object.Booking.BookingId);

            return RedirectToPage("List", "Customers");
        }
    }
}
