using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;
using warehouseapi.Databases;
using warehouseapi.Models;
using static warehouseapi.Models.Item;

namespace warehouseapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public const string ItemsDbPath = "./Databases/items.xml";

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return ItemsDatabaseController.LoadItemsDatabase(ItemsDbPath);
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<Item> Get(Guid id)
        {
            List<Item>? items = ItemsDatabaseController.LoadItemsDatabase(ItemsDbPath);
            if(items == null)
            {
                return NotFound();
            }

            Item? item = items.FirstOrDefault(item => item.Id == id.ToString());

            return item != null ? item : NotFound();
        }

        // POST api/<ItemsController>
        [HttpPost]
        public List<Item> Post([FromBody] ItemBody itemBody)
        {
            List<Item> items = ItemsDatabaseController.LoadItemsDatabase(ItemsDbPath);
            items.Add(new Item(itemBody));

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

            using (StreamWriter writer = new StreamWriter(ItemsDbPath))
            {
                writer.Write(root.ToString());
            }

            return items;
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public ActionResult<List<Item>> Put(Guid id, [FromBody] ItemBody item)
        {
            List<Item>? items = ItemsDatabaseController.LoadItemsDatabase(ItemsDbPath);
            if (items == null)
            {
                return NotFound();
            }

            Item foundItem = items.FirstOrDefault(item => item.Id == id.ToString());
            if(foundItem == null)
            {
                return NotFound();
            }

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

            using (StreamWriter writer = new StreamWriter(ItemsDbPath))
            {
                writer.Write(root.ToString());
            }

            return items;
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {

        }
    }
}
