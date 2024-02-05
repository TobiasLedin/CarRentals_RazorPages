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


        public IActionResult OnGetVehicle(int id, string action)
        {
            Object.Vehicle = _vehicleRepo.GetById(id);
            ViewData["type"] = "Vehicle";
            ViewData["action"] = action;
            return Page();
        }

        public IActionResult OnGetCustomer(int id, string action)
        {
            Object.Customer = _customerRepo.GetById(id);
            ViewData["type"] = "Customer";
            ViewData["action"] = action;
            return Page();
        }

        public IActionResult OnGetBooking(int id, string action)
        {
            Object.Customer = _customerRepo.GetById(id);
            ViewData["type"] = "Customer";
            ViewData["action"] = action;
            return Page();
        }

        public IActionResult OnPostVehicle(int id, string action)
        {
            if (action == "edit")
            {
                ModelState.Clear();
                if (!TryValidateModel(Object.Vehicle))
                {
                    return Page();  // TODO: felmeddelande
                }
                _vehicleRepo.Update(Object.Vehicle);
            }
            else if (action == "delete")
            {
                _vehicleRepo.Delete(id);
            }
            return RedirectToPage("List", "Vehicles");
        }

        public IActionResult OnPostCustomer(int id, string action)
        {
            if (action == "edit")
            {
                ModelState.Clear();
                if (!TryValidateModel(Object.Customer))
                {
                    return Page();  // TODO: felmeddelande
                }
                _customerRepo.Update(Object.Customer);
            }
            else if (action == "delete")
            {
                _customerRepo.Delete(id);
            }
            return RedirectToPage("List", "Customers");
        }

        public IActionResult OnPostBooking(int id, string action)
        {
            if (action == "edit")
            {
                ModelState.Clear();
                if (!TryValidateModel(Object.Customer))
                {
                    return Page();  // TODO: felmeddelande
                }
                _customerRepo.Update(Object.Customer);
            }
            else if (action == "delete")
            {
                _customerRepo.Delete(id);
            }
            return RedirectToPage("List", "Customers");
        }
    }
}
