using FribergCarRentals.DataAccess.Interfaces;
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

            //BookingData = new BookingDataVM();
        }

        [BindProperty(SupportsGet = true)]
        public CreateVM Object { get; set; }

        //[BindProperty]
        //public BookingDataVM BookingData { get; set; }

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
            if(!HttpContext.Session.TryGetValue("_customer", out _))
            {
                HttpContext.Session.SetInt32("_vehicleId", Object.VehicleId); // Sätt VehicleId till sessionen medan kund skapas.
                return RedirectToPage("/Customers/Login");
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

            return RedirectToPage("./Index");   //TODO
        }

        public IActionResult OnPostVehicle()
        {
            ModelState.Clear();
            if (!TryValidateModel(Object.Vehicle))
            {
                return Page();
            }

            _vehicleRepo.Create(Object.Vehicle);

            return RedirectToPage("./Index");   //TODO
        }

        public IActionResult OnPostBooking()
        {
            int customerId = (int)HttpContext.Session.GetInt32("_customer");
            var customer = _customerRepo.GetById(customerId);

            Booking booking = new()
            {
                CustomerId = customerId,
                VehicleId = Object.VehicleId,
                BookingStart = Object.Booking.BookingStart,
                BookingEnd = Object.Booking.BookingEnd
            };

            ModelState.Clear();
            if (!TryValidateModel(booking))
            {
                return Page();
            }

            _vehicleRepo.Create(Object.Vehicle);

            return RedirectToPage("./Index");   //TODO
        }
    }
}
