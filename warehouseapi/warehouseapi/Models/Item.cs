using warehouseapi.DTOs;

namespace warehouseapi.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
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
