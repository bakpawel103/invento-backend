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
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive number allowed")]
        public float Price { get; set; }
    }
}
