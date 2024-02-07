using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IAuthService _auth;

        public CreateModel(ICustomerRepository customerRepo, IVehicleRepository vehicleRepo, IBookingRepository bookingRepo, IAuthService auth)
        {
            _customerRepo = customerRepo;
            _vehicleRepo = vehicleRepo;
            _bookingRepo = bookingRepo;
            _auth = auth;
            Object = new CreateVM();
        }

        [BindProperty(SupportsGet = true)]
        public CreateVM Object { get; set; }


        public IActionResult OnGetCustomer()
        {
            var result = _auth.CheckCustomerAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Type = "customer";
            return Page();
        }

        public IActionResult OnGetBooking()
        {
            var result = _auth.CheckCustomerAuth();

            if (!result.Success)
            {
                HttpContext.Session.SetInt32("_vehicleId", Object.VehicleId);
                return RedirectToPage("/Customers/Login");
            }

            if (result.Success)
            {
                Object.VehicleId = (int)HttpContext.Session.GetInt32("_vehicleId");
                HttpContext.Session.Remove("_vehicleId");
            }

            Object.Vehicle = _vehicleRepo.GetById(Object.VehicleId);
            Object.Type = "booking";
            return Page();
        }

        public IActionResult OnPostCustomer()       // TODO: Fixa auth for POST-metoder?
        {
            ModelState.Clear();
            if (!TryValidateModel(Object.Customer))
            {
                return Page();
            }

            _customerRepo.Create(Object.Customer);

            return RedirectToPage("List", "Customers");
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
            Object.Booking = _bookingRepo.GetById(bookingId);

            Object.Type = "confirmation";
            return Page();
        }
    }
}
