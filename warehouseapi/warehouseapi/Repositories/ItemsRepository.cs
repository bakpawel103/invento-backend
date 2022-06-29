using System.Xml.Linq;
using warehouseapi.Models;

namespace warehouseapi.Repositories
{
    public class ItemsRepository : IRepository<Item>
    {
        private string ItemsDbRootPath;
        private string ItemsDbPath;

        private List<Item> items = new List<Item>();

        public ItemsRepository()
        {
            ItemsDbRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Databases");
            ItemsDbPath = Path.Combine(ItemsDbRootPath, "items.xml");
        }

        private void InitializeDatabaseFile()
        {
            XDocument root = new XDocument(
                new XElement("items")
            );

            if(!Directory.Exists(ItemsDbRootPath)) {
                Directory.CreateDirectory(ItemsDbRootPath);
            }

            using (FileStream fs = new FileStream(ItemsDbPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(root.ToString());
                }
            }
        }

        private void SaveDatabase()
        {
            XDocument root = new XDocument(
                new XElement("items",
                    items.Select(item =>
                        new XElement("item",
                            new XAttribute("id", item.Id),
                            new XAttribute("name", item.Name ?? ""),
                            new XAttribute("createDate", item.CreateDate.ToString() ?? ""),
                            new XAttribute("description", item.Description ?? ""),
                            new XAttribute("quantity", item.Quantity),
                            new XAttribute("price", item.Price)
                        )
                    )
                )
            );

            using (FileStream fs = new FileStream(ItemsDbPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(root.ToString());
                }
            }
        }

        private void LoadItemsDatabase()
        {
            // if database file not exists or is empty then initialize database
            if (!File.Exists(ItemsDbPath) || new FileInfo(ItemsDbPath).Length == 0)
            {
                InitializeDatabaseFile();
            }

            items.Clear();

            XDocument xDocument = XDocument.Load(ItemsDbPath);

            foreach (var element in xDocument.Root.Elements("item"))
            {
                Item item = new Item();

                item.Id = Guid.Parse(element.Attribute("id").Value);
                item.Name = element.Attribute("name").Value;
                item.CreateDate = DateTime.Parse(element.Attribute("createDate").Value);
                item.Description = element.Attribute("description").Value;
                item.Quantity = float.Parse(element.Attribute("quantity").Value);
                item.Price = float.Parse(element.Attribute("price").Value);

                items.Add(item);
            }
        }

        public Item Create(Item item)
        {
            LoadItemsDatabase();

            items.Add(item);

            SaveDatabase();

            return item;
        }

        public Item? Update(Item item)
        {
            LoadItemsDatabase();

            Item? foundItem = items.FirstOrDefault(itemELem => itemELem.Id == item.Id);
            if (foundItem == null)
            {
                return null;
            }

            foundItem.Name = item.Name;
            foundItem.Description = item.Description;
            foundItem.Quantity = item.Quantity;
            foundItem.Price = item.Price;

            SaveDatabase();

            return item;
        }

        public List<Item> GetAll()
        {
            LoadItemsDatabase();

            return items;
        }

        public Item? GetById(Guid id)
        {
            LoadItemsDatabase();

            return items.FirstOrDefault(item => item.Id == id);
        }

        public bool Delete(Guid id)
        {
            LoadItemsDatabase();

            Item? foundItem = items.FirstOrDefault(itemELem => itemELem.Id == id);
            if (foundItem == null)
            {
                return false;
            }

            items.Remove(foundItem);

            SaveDatabase();

            return true;
        }
    }
}
