using System.ComponentModel.DataAnnotations;

namespace TruckGarage.Entity {
    public class Truck {
        [Key,Required]
        public long Id { get; set; }
        [Required]
        public string modelo { get; set; } = string.Empty;
        [Editable(false)]
        public string anoFabricacao { get; set; } = string.Empty;
        [Required]
        public string anoModelo { get; set; } = string.Empty;
    }
}