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
        [Range(1960, 2025)]
        public int Year { get; set; }
        [Required]
        [DisplayName("Daily rate")]
        [Range(100, 10000)]
        public int DailyRate { get; set; }
        [DisplayName("Fribergs comment")]
        public string? Comment { get; set; }
        [DisplayName("Image")]
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }

        public override string? ToString()
        {
            return $"{Brand} {Model}";
        }
    }
}
