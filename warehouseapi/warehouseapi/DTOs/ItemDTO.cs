using System.ComponentModel.DataAnnotations;

namespace warehouseapi.DTOs
{
    public class ItemDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Quantity { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
