using FribergCarRentals.Interfaces;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class EditModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly ICustomerRepository _customerRepo;

        public EditModel(IVehicleRepository vehicleRepo, ICustomerRepository customerRepo)
        {
            _vehicleRepo = vehicleRepo;
            _customerRepo = customerRepo;
            Object = new EditVM();
        }

        [BindProperty]
        public EditVM Object { get; set; }


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

        public IActionResult OnPostVehicle()
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
