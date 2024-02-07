using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IBookingRepository _bookingRepo;

        public CreateModel(ICustomerRepository customerRepo, IVehicleRepository vehicleRepo, IBookingRepository bookingRepo)
        {
            _customerRepo = customerRepo;
            _vehicleRepo = vehicleRepo;
            _bookingRepo = bookingRepo;
            Object = new CreateVM();
        }

        [BindProperty(SupportsGet = true)]
        public CreateVM Object { get; set; }


        public IActionResult OnGetCustomer()
        {
            Object.Type = "customer";
            return Page();
        }

        public IActionResult OnGetVehicle()
        {
            Object.Type = "vehicle";
            return Page();
        }

        public IActionResult OnGetBooking()
        {
            if (!HttpContext.Session.TryGetValue("_customer", out _))    // Check if a customer i logged in. If not, save vehicleId in session and redirect to Login page.
            {
                HttpContext.Session.SetInt32("_vehicleId", Object.VehicleId);
                return RedirectToPage("/Customers/Login");
            }

            if (HttpContext.Session.TryGetValue("_vehicleId", out _))    // If a vehicleId is present in session, use this for the booking then erase from session.
            {
                Object.VehicleId = (int)HttpContext.Session.GetInt32("_vehicleId");
                HttpContext.Session.Remove("_vehicleId");
            }

            Object.Vehicle = _vehicleRepo.GetById(Object.VehicleId);
            Object.Type = "booking";
            return Page();
        }

        public IActionResult OnPostCustomer()
        {
            ModelState.Clear();
            if (!TryValidateModel(Object.Customer))
            {
                return Page();
            }

            _customerRepo.Create(Object.Customer);

            return RedirectToPage("List", "Customers");
        }

        public IActionResult OnPostVehicle()
        {
            ModelState.Clear();
            if (!TryValidateModel(Object.Vehicle))
            {
                return Page();
            }

            _vehicleRepo.Create(Object.Vehicle);

            return RedirectToPage("List", "Vehicles");
        }

        public IActionResult OnPostBooking()
        {
            int customerId = (int)HttpContext.Session.GetInt32("_customer");
            var customer = _customerRepo.GetById(customerId);
            int vehicleId = Object.VehicleId;
            var vehicle = _vehicleRepo.GetById(Object.VehicleId);

            Booking booking = new()
            {
                CustomerId = customerId,
                Customer = customer,
                VehicleId = vehicleId,
                Vehicle = vehicle,
                DailyRate = vehicle.DailyRate,
                BookingStart = Object.Booking.BookingStart,
                BookingEnd = Object.Booking.BookingEnd
            };

            ModelState.Clear();
            if (!TryValidateModel(booking))
            {
                Object.Type = "booking";
                return Page();
            }

            var bookingId = _bookingRepo.Create(booking);
            Object.Booking = _bookingRepo.GetById(bookingId);   //Write booking to VM for presentation in confirmation.

            Object.Type = "confirmation";
            return Page();
        }
    }
}
