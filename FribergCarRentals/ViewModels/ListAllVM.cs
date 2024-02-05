using FribergCarRentals.Models;

namespace FribergCarRentals.ViewModels
{
    public class ListAllVM
    {
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public string Type { get; set; } = default!;
        public string User { get; set; } = default!;
    }
}
