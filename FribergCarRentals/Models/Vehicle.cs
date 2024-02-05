using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergCarRentals.Models
{
    public class Vehicle
    {
        [Key]
        [DisplayName("Vehicle ID")]
        public int VehicleId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [DisplayName("Fribergs comment")]
        public string? Comment { get; set; }
        [DisplayName("Image link")]
        public string? ImageUrl { get; set; }

        public override string? ToString()
        {
            return $"{Brand} {Model} -{Year}";
        }
    }
}
