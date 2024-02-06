using FribergCarRentals.Models;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.ViewModels
{
    public class LoginVM
    {
        public Admin Admin { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public string Action { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;

        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; } = default!;

    }
}
