using Microsoft.AspNetCore.Mvc;
using warehouseapi.DTOs;
using warehouseapi.Models;
using warehouseapi.Repositories;

namespace warehouseapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemsRepository.GetAll());
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Item? item = _itemsRepository.GetById(id);

            return item is not null ? Ok(item) : NotFound();
        }

        // POST api/<ItemController>
        [HttpPost]
        public IActionResult Post([FromBody] ItemDTO itemDTO)
        {
            Item item = _itemsRepository.Create(new Item(itemDTO));

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ItemDTO itemDTO)
        {
            Item? item = _itemsRepository.Update(new Item(id, itemDTO));
            
            return item is not null ? Ok(item) : NotFound();
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if(!_itemsRepository.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
