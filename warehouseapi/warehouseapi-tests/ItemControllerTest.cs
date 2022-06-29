using Microsoft.AspNetCore.Mvc;
using warehouseapi.Controllers;
using warehouseapi.DTOs;
using warehouseapi.Models;
using warehouseapi.Repositories;

namespace warehouseapi_tests
{
    public class ItemControllerTest
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly ItemController _itemController;

        public ItemControllerTest()
        {
            _itemsRepository = new ItemsRepository();
            _itemController = new ItemController(_itemsRepository);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _itemController.Get();

            Assert.IsType<OkObjectResult>((OkObjectResult) okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            List<Item> postItems = new List<Item>();

            for(int itemIndex = 0; itemIndex < 3; itemIndex++)
            {
                var itemDTO = new ItemDTO
                {
                    Name = $"Test item name {itemIndex}",
                    Description = $"Test item description {itemIndex}",
                    Quantity = itemIndex,
                    Price = itemIndex
                };

                postItems.Add((Item)((CreatedAtActionResult)_itemController.Post(itemDTO)).Value);
            }

            var okResult = (OkObjectResult) _itemController.Get();

            var items = Assert.IsType<List<Item>>(okResult.Value);
            Assert.True(items.Count >= 3);

            foreach (var postItem in postItems)
            {
                _itemController.Delete(postItem.Id);
            }
        }

        [Fact]
        public void Post_WhenCalled_ReturnsCreatedResult()
        {
            var itemDTO = new ItemDTO
            {
                Name = "Test item name 1",
                Description = "Test item description 1",
                Quantity = 1,
                Price = 1
            };

            Item postItem = (Item)((CreatedAtActionResult)_itemController.Post(itemDTO)).Value;

            _itemController.Delete(postItem.Id);
        }
    }
}