using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChickenAPI.Model
{
    public class Chicken
    {
        [Key]
        public int ChickID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Breed { get; set; }

        [Range(0, 50)] // Assuming chickens won’t live past 50 years
        public int Age { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)] // eggs per day
        public decimal EggProduction { get; set; }

        [Required]
        public bool IsPregnant { get; set; }

        [Required]
        public DateTime LastVetCheck { get; set; }
    }
}
