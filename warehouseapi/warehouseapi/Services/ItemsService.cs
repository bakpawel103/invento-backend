using System.Xml.Linq;
using warehouseapi.Models;
using warehouseapi.Repositories;

namespace warehouseapi.Services
{
    public class ItemsService : IService<Item>
    {
        private List<Item> items = new List<Item>();
        private readonly IRepository<Item> _itemsRepository;

        public ItemsService(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public Item Create(Item item)
        {
            items = _itemsRepository.LoadDatabase();

            items.Add(item);

            _itemsRepository.Save(items);

            return item;
        }

        public Item? Update(Item item)
        {
            items = _itemsRepository.LoadDatabase();

            Item? foundItem = items.FirstOrDefault(itemELem => itemELem.Id == item.Id);
            if (foundItem == null)
            {
                return null;
            }

            foundItem.Name = item.Name;
            foundItem.Description = item.Description;
            foundItem.Quantity = item.Quantity;
            foundItem.Price = item.Price;

            _itemsRepository.Save(items);

            return foundItem;
        }

        public List<Item> GetAll()
        {
            items = _itemsRepository.LoadDatabase();

            return items;
        }

        public Item? GetById(Guid id)
        {
            items = _itemsRepository.LoadDatabase();

            return items.FirstOrDefault(item => item.Id == id);
        }

        public bool Delete(Guid id)
        {
            items = _itemsRepository.LoadDatabase();

            Item? foundItem = items.FirstOrDefault(itemELem => itemELem.Id == id);
            if (foundItem == null)
            {
                return false;
            }

            items.Remove(foundItem);

            _itemsRepository.Save(items);

            return true;
        }
    }
}
