using System.ComponentModel.DataAnnotations;

namespace TruckGarage.Entity {
    public class Truck {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public string modelo { get; set; } = string.Empty;
        [Required]
        public DateTime anoFabricacao { get; set; }
        [Required]
        public DateTime anoModelo { get; set; }
    }
}