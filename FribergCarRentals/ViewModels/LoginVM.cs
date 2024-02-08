using FribergCarRentals.Models;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.ViewModels
{
    public class LoginVM
    {
        public Admin Admin { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public string Action { get; set; } = default!;

        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = default!;
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = default!;

    }
}
