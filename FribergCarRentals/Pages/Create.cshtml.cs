using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IVehicleRepository _vehicleRepo;


        public CreateModel(ICustomerRepository customerRepo, IVehicleRepository vehicleRepo)
        {
            _customerRepo = customerRepo;
            _vehicleRepo = vehicleRepo;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        public IActionResult OnGetCustomer()
        {
            ViewData["type"] = "customer";
            return Page();
        }

        public IActionResult OnGetVehicle()
        {
            ViewData["type"] = "vehicle";
            return Page();
        }

        public IActionResult OnPostCustomer()
        {
            ModelState.Clear();
            if (!TryValidateModel(Customer))
            {
                return Page();
            }

            _customerRepo.Create(Customer);

            return RedirectToPage("./Index");   //TODO
        }

        public IActionResult OnPostVehicle()
        {
            ModelState.Clear();
            if (!TryValidateModel(Vehicle))
            {
                return Page();
            }

            _vehicleRepo.Create(Vehicle);

            return RedirectToPage("./Index");   //TODO
        }
    }
}
