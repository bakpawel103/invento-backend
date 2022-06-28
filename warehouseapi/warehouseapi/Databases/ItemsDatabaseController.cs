using System.Xml.Linq;
using warehouseapi.Models;

namespace warehouseapi.Databases
{
    public class ItemsDatabaseController
    {
        public static List<Item> LoadItemsDatabase(string path)
        {
            if(!File.Exists(path))
            {
                ItemsDatabaseController.CreateAndGetItemsDatabase(path);
            }

            XDocument xDocument = XDocument.Load(path);

            List<Item> items = new List<Item>();

            foreach(var element in xDocument.Root.Elements("item"))
            {
                Item item = new Item();

                item.Id = element.Attribute("id").Value;
                item.Name = element.Attribute("name").Value;
                item.CreateDate = DateTime.Parse(element.Attribute("createDate").Value);
                item.Description = element.Attribute("description").Value;
                item.Quantity = float.Parse(element.Attribute("quantity").Value);
                item.Price = float.Parse(element.Attribute("price").Value);

                items.Add(item);
            }

            return items;
        }

        public static List<Item> CreateAndGetItemsDatabase(string path)
        {
            XDocument root = new XDocument(
                new XElement("items")
            );

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(root.ToString());
            }

            return new List<Item>();
        }
    }
}
