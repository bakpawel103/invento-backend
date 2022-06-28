namespace warehouseapi.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }

        public Item()
        {
            Id = Guid.NewGuid().ToString();
            Name = String.Empty;
            Description = String.Empty;
        }

        public Item(ItemBody itemBody)
        {
            Id = Guid.NewGuid().ToString();
            Name = itemBody.Name;
            CreateDate = DateTime.Now;
            Description = itemBody.Description;
            Quantity = itemBody.Quantity;
            Price = itemBody.Price;
        }

        public class ItemBody
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public float Quantity { get; set; }
            public float Price { get; set; }
        }
    }
}
