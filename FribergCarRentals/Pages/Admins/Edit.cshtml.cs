using FribergCarRentals.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Admins
{
    public class EditModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IAuthService _auth;

        public EditModel(IVehicleRepository vehicleRepo, ICustomerRepository customerRepo, IAuthService auth)
        {
            _vehicleRepo = vehicleRepo;
            _customerRepo = customerRepo;
            _auth = auth;
            Object = new EditVM();
        }

        [BindProperty]
        public EditVM Object { get; set; }


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

        public IActionResult OnPostVehicle()    // TODO: Fixa auth for onPost?
        {
            ModelState.Clear();
            if (TryValidateModel(Object.Vehicle))
            {
                _vehicleRepo.Update(Object.Vehicle);
            }

            return RedirectToPage("List", "Vehicles");
        }

        public IActionResult OnPostCustomer()
        {
            ModelState.Clear();
            if (TryValidateModel(Object.Customer))
            {
                _customerRepo.Update(Object.Customer);
            }

            return RedirectToPage("List", "Customers");
        }
    }
}
