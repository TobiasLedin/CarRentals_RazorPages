using FribergCarRentals.Models;

namespace FribergCarRentals.ViewModels
{
    public class EditVM
    {
        public Vehicle Vehicle { get; set; } = default!;
        public Customer Customer { get; set; } = default!;

        public string Type { get; set; } = default!;
    }
}
