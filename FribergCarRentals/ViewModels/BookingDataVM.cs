using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergCarRentals.ViewModels
{
    public class BookingDataVM
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Pickup date")]
        public DateTime BookingStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Return date")]
        public DateTime BookingEnd { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int VehicleId { get; set; }
    }
}
