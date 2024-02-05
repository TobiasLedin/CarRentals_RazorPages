using FribergCarRentals.Models;

namespace FribergCarRentals.ViewModels
{
    public class LoginVM
    {
        public Admin Admin { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
}
