using Microsoft.AspNetCore.Mvc;
using warehouseapi.DTOs;
using warehouseapi.Models;
using warehouseapi.Repositories;

namespace warehouseapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _itemsRepository.GetAll();
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<Item> Get(Guid id)
        {
            Item? item = _itemsRepository.GetById(id);
            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public ActionResult<Item> Post([FromBody] ItemDTO itemDTO)
        {
            Item item = _itemsRepository.Create(new Item(itemDTO));

            return Created("Successfully created item", item);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public ActionResult<Item> Put(Guid id, [FromBody] ItemDTO itemDTO)
        {
            Item? item = _itemsRepository.Update(new Item(id, itemDTO));
            if(item == null)
            {
                NotFound();
            }

            return Ok(item);
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _itemsRepository.Delete(id);
        }
    }
}
