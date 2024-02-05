using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
