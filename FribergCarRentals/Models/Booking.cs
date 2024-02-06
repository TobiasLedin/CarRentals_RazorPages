using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergCarRentals.Models
{
    public class Booking
    {
        [Key]
        [DisplayName("Booking ID")]
        public int BookingId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Pickup date")]
        [FutureDate(ErrorMessage = "Pick date cannot be in the past")]
        public DateTime BookingStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Return date")]
        [FutureDate(ErrorMessage = "Return date cannot be in the past")]
        public DateTime BookingEnd { get; set; }

        [DisplayName("Customer ID")]
        public int? CustomerId { get; set; }

        [DisplayName("Vehicle ID")]
        public int? VehicleId { get; set; }
        
        [ForeignKey("CustomerId")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public Customer Customer { get; set; }
        
        [ForeignKey("VehicleId")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public Vehicle Vehicle { get; set; }

    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is DateTime dateTime && dateTime >= DateTime.Today;
        }

        public override string FormatErrorMessage(string name)
        {
            return "The date for " + name + " must be in the future.";
        }
    }
}
