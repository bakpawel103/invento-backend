using System.Xml.Linq;
using warehouseapi.Models;

namespace warehouseapi.Tools
{
    public class ItemXDocumentHelper
    {
        private static readonly string ROOT = "items";
        private static readonly string ELEMENT = "item";
        private static readonly string ELEMENT_ID = "id";
        private static readonly string ELEMENT_NAME = "name";
        private static readonly string ELEMENT_CREATE_DATE = "createDate";
        private static readonly string ELEMENT_DESCRIPTION = "description";
        private static readonly string ELEMENT_QUANTITY = "quantity";
        private static readonly string ELEMENT_PRICE = "price";

        public static XDocument GetEmpty()
        {
            return new XDocument(
                new XElement(ROOT)
            );
        }

        public static XDocument GetFromItemsList(List<Item> items)
        {
            return new XDocument(
                new XElement(ROOT,
                    items.Select(item => GetXElementFromItem(item))
                )
            );
        }

        public static List<Item> GetItemsListFromXDocument(XDocument root)
        {
            List<Item> result = new List<Item>();

            foreach (var element in root.Root.Elements(ELEMENT))
            {
                result.Add(XElementToItem(element));
            }

            return result;
        }

        private static XElement GetXElementFromItem(Item item)
        {
            return new XElement(ELEMENT,
                new XAttribute(ELEMENT_ID, item.Id),
                new XAttribute(ELEMENT_NAME, item.Name ?? string.Empty),
                new XAttribute(ELEMENT_CREATE_DATE, item.CreateDate.ToString() ?? string.Empty),
                new XAttribute(ELEMENT_DESCRIPTION, item.Description ?? string.Empty),
                new XAttribute(ELEMENT_QUANTITY, item.Quantity),
                new XAttribute(ELEMENT_PRICE, item.Price)
            );
        }

        private static Item XElementToItem(XElement element)
        {
            Item item = new Item();

            item.Id = Guid.Parse(element.Attribute(ELEMENT_ID).Value);
            item.Name = element.Attribute(ELEMENT_NAME).Value;
            item.CreateDate = DateTime.Parse(element.Attribute(ELEMENT_CREATE_DATE).Value);
            item.Description = element.Attribute(ELEMENT_DESCRIPTION).Value;
            item.Quantity = int.Parse(element.Attribute(ELEMENT_QUANTITY).Value);
            item.Price = float.Parse(element.Attribute(ELEMENT_PRICE).Value);

            return item;
        }
    }
}
