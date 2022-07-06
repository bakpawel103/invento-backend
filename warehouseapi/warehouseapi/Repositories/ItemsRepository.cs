using System.Xml.Linq;
using warehouseapi.Models;
using warehouseapi.Tools;

namespace warehouseapi.Repositories
{
    public class ItemsRepository : IRepository<Item>
    {
        private List<Item> items = new List<Item>();
        private ItemDatabaseController itemDatabaseController;

        public ItemsRepository()
        {
            itemDatabaseController = new ItemDatabaseController();
        }

        public Item Create(Item item)
        {
            items = itemDatabaseController.LoadItemsDatabase();

            items.Add(item);

            itemDatabaseController.SaveItems(items);

            return item;
        }

        public Item? Update(Item item)
        {
            items = itemDatabaseController.LoadItemsDatabase();

            Item? foundItem = items.FirstOrDefault(itemELem => itemELem.Id == item.Id);
            if (foundItem == null)
            {
                return null;
            }

            foundItem.Name = item.Name;
            foundItem.Description = item.Description;
            foundItem.Quantity = item.Quantity;
            foundItem.Price = item.Price;

            itemDatabaseController.SaveItems(items);

            return foundItem;
        }

        public List<Item> GetAll()
        {
            items = itemDatabaseController.LoadItemsDatabase();

            return items;
        }

        public Item? GetById(Guid id)
        {
            items = itemDatabaseController.LoadItemsDatabase();

            return items.FirstOrDefault(item => item.Id == id);
        }

        public bool Delete(Guid id)
        {
            items = itemDatabaseController.LoadItemsDatabase();

            Item? foundItem = items.FirstOrDefault(itemELem => itemELem.Id == id);
            if (foundItem == null)
            {
                return false;
            }

            items.Remove(foundItem);

            itemDatabaseController.SaveItems(items);

            return true;
        }
    }
}
