using Microsoft.AspNetCore.Mvc;
using System.Net;
using warehouseapi.DTOs;
using warehouseapi.Models;
using warehouseapi.Repositories;

namespace warehouseapi.Controllers
{
    [Route("api/Item")]
    [ApiController]
    [Produces("application/json")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        /// <summary>
        /// Gets all Items.
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Item>), 200)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_itemsRepository.GetAll());
            }
            catch (Exception _)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a specific Item.
        /// </summary>
        /// <param name="id">Id of the searching item</param>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get(Guid id)
        {
            try
            {
                Item? item = _itemsRepository.GetById(id);

                return item is not null ? Ok(item) : NotFound();
            }
            catch (Exception _)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Creates an Item.
        /// </summary>
        /// <param name="itemDTO">Creating item parameters</param>
        /// <response code="201">Created a new item</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(Item), 201)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Post([FromBody] ItemDTO itemDTO)
        {
            try
            {
                Item item = _itemsRepository.Create(new Item(itemDTO));

                return Created("/api/Item", item);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates a specific Item.
        /// </summary>
        /// <param name="id">Id of the searching item</param>
        /// <param name="itemDTO">Updating item parameter</param>
        /// <response code="200">Updated a specific item</response>
        /// <response code="404">Can't fint specific item</response>
        /// <response code="404">Can't fint specific item</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Put(Guid id, [FromBody] ItemDTO itemDTO)
        {
            try
            {
                Item? item = _itemsRepository.Update(new Item(id, itemDTO));

                return item is not null ? Ok(item) : NotFound();
            }
            catch (Exception _)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <param name="id">Id of the searching item</param>
        /// <response code="200">Successful operation</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (!_itemsRepository.Delete(id))
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception _)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
