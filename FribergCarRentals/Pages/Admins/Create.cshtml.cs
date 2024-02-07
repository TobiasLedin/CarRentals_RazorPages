using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
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
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Type = "customer";
            return Page();
        }

        public IActionResult OnGetVehicle()
        {
            var result = _auth.CheckAdminAuth();
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("Login");
            }

            Object.Type = "vehicle";
            return Page();
        }

        public IActionResult OnPostCustomer()       // TODO : Fixa auth för Post-metoder?
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
    }
}
