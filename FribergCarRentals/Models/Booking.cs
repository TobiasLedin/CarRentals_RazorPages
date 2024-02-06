using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FribergCarRentals.Attributes;

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
        [NoPastDate]
        public DateTime BookingStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Return date")]
        [EqualOrGreater("BookingStart")]
        public DateTime BookingEnd { get; set; }

        [DisplayName("Customer ID")]
        public int? CustomerId { get; set; }

        [DisplayName("Vehicle ID")]
        public int? VehicleId { get; set; }

        [Required]
        [DisplayName("Daily rate")]
        public int DailyRate { get; set; }

        [DisplayName("Total")]
        public int TotalCost
        {
            get
            {
                int days = (int)(BookingEnd - BookingStart).TotalDays + 1;
                int totalCost = days * DailyRate;

                return totalCost;
            }
        }

        [ForeignKey("CustomerId")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public Customer Customer { get; set; }

        [ForeignKey("VehicleId")]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public Vehicle Vehicle { get; set; }

    }
}
