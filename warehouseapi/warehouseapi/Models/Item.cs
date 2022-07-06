using warehouseapi.DTOs;

namespace warehouseapi.Models
{
    public class Item
    {
        /// <summary>
        /// The unique id of the item
        /// </summary>
        /// <example>500fccb6-9844-497e-8edc-dbafadcb1133</example>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the item
        /// </summary>
        /// <example>Small box</example>
        public string Name { get; set; }
        /// <summary>
        /// The creation date of the item
        /// </summary>
        /// <example>04/07/2022 15:41:33</example>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// The description of the item
        /// </summary>
        /// <example>A Small box</example>
        public string Description { get; set; }
        /// <summary>
        /// The quantity of the item
        /// </summary>
        /// <example>2</example>
        public int Quantity { get; set; }
        /// <summary>
        /// The price of the item
        /// </summary>
        /// <example>12.5</example>
        public float Price { get; set; }

        public Item()
        {
            Id = Guid.NewGuid();
            Name = String.Empty;
            CreateDate = DateTime.Now;
            Description = String.Empty;
        }

        public Item(ItemDTO itemBody)
        {
            Id = Guid.NewGuid();
            Name = itemBody.Name;
            CreateDate = DateTime.Now;
            Description = itemBody.Description;
            Quantity = itemBody.Quantity;
            Price = itemBody.Price;
        }

        public Item(Guid id, ItemDTO itemBody)
        {
            Id = id;
            Name = itemBody.Name;
            CreateDate = DateTime.Now;
            Description = itemBody.Description;
            Quantity = itemBody.Quantity;
            Price = itemBody.Price;
        }
    }
}
