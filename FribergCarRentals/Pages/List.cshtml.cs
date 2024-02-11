using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages
{
    public class ListModel : PageModel
    {
        private readonly IVehicleRepository _vehicleRepo;

        public ListModel(IVehicleRepository vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();


        public IActionResult OnGet()
        {
            Vehicles = _vehicleRepo.GetAll();

            return Page();
        }
    }
}
