using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class ListModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly ICustomerRepository _customerRepo;

        public ListModel(IVehicleRepository vehicleRepo, IBookingRepository bookingRepo, ICustomerRepository customerRepo)
        {
            _vehicleRepo = vehicleRepo;
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
            Lists = new ListAllVM();
        }

        public ListAllVM Lists { get; set; }

        // Type = Model to list.
        // User = Customize data and options according admin/customer/visitor.

        public void OnGetVehicles()
        {
            Lists.Type = "vehicle";

            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                Lists.User = "admin";
            }
            else if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                Lists.User = "customer";
            }
            else
            {
                Lists.User = "visitor";
            }

            Lists.Vehicles = _vehicleRepo.GetAll();
        }

        public void OnGetCustomers()
        {
            Lists.Type = "customer";

            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                Lists.User = "admin";
                Lists.Customers = _customerRepo.GetAll();
            }
        }

        public void OnGetBookings()
        {
            Lists.Type = "booking";

            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                Lists.User = "admin";
                Lists.Bookings = _bookingRepo.GetAll();
            }
            else if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                Lists.User = "customer";
                int customerId = (int)HttpContext.Session.GetInt32("_customer");
                Lists.Bookings = _bookingRepo.GetAllByCustomer(customerId);
            }
        }
    }
}
