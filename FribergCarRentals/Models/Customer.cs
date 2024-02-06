using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergCarRentals.Models
{
    public class Customer
    {
        [Key]
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }
        [Required]
        [DisplayName("Name")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 letters long")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Special character are not allowed")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Lastname")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 letters long")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Special character are not allowed")]
        public string LastName { get; set; }
        [Required]

        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long")]
        public string Password { get; set; }
        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
