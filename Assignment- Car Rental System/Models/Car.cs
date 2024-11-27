using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Assignment__Car_Rental_System.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public int Year {  get; set; }
        [Range(0, double.MaxValue)]
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
