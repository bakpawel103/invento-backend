using System.ComponentModel.DataAnnotations;

namespace warehouseapi.DTOs
{
    public class ItemDTO
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        /// <example>Small box</example>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The description of the item
        /// </summary>
        /// <example>A Small box</example>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// The quantity of the item
        /// </summary>
        /// <example>2</example>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
        /// <summary>
        /// The price of the item
        /// </summary>
        /// <example>12.5</example>
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Only positive number allowed")]
        public float Price { get; set; }
    }
}
