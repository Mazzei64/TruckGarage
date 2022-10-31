using System.ComponentModel.DataAnnotations;

namespace TruckGarage.Entity {
    public class Truck {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public string modelo { get; set; } = string.Empty;
        [Required]
        public DateOnly anoFabricacao { get; set; }
        [Required]
        public DateOnly anoModelo { get; set; }
    }
}