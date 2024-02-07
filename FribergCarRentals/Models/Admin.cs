using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long")]
        public string Password { get; set; }

    }
}
