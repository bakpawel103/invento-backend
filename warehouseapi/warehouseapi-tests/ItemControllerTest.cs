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
            string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");
            _itemsRepository = new ItemsRepository(Path.Combine(LocalLowPath, "Invento", "Databases", "items-tests.xml"));
            _itemController = new ItemController(_itemsRepository);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            IActionResult okResult = _itemController.Get();

            Assert.IsType<OkObjectResult>((OkObjectResult) okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            OkObjectResult okResult = (OkObjectResult) _itemController.Get();

            List<Item> items = Assert.IsType<List<Item>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOneItem()
        {
            Guid existedItemGuid = new Guid("1e1756ff-4ff9-44b5-b973-d08c09c3ff67");

            OkObjectResult okResult = (OkObjectResult)_itemController.Get(existedItemGuid);

            Item item = Assert.IsType<Item>(okResult.Value);
            Assert.NotNull(item);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsZeroItem()
        {
            Guid notExistedItemGuid = new Guid("1efa4eff-4ff9-44b5-b973-d08c09c3ff67");

            NotFoundResult notFoundResult = (NotFoundResult)_itemController.Get(notExistedItemGuid);

            Assert.IsType<NotFoundResult>((NotFoundResult)notFoundResult);
        }

        [Fact]
        public void Post_WhenCalled_ReturnsCreatedResult()
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Name = "Test item name 1",
                Description = "Test item description 1",
                Quantity = 1,
                Price = 1
            };

            IActionResult createdAtActionResult = _itemController.Post(itemDTO);

            Assert.IsType<CreatedAtActionResult>((CreatedAtActionResult)createdAtActionResult);

            //Clearing
            _itemController.Delete(((Item)((CreatedAtActionResult)createdAtActionResult).Value).Id);
        }

        [Fact]
        public void Put_WhenCalled_ReturnsNotFoundResult()
        {
            Guid notExistedItemGuid = new Guid("1efa4eff-4ff9-44b5-b973-d08c09c3ff67");

            ItemDTO itemDTO = new ItemDTO
            {
                Name = "Test item name 1",
                Description = "Test item description 1",
                Quantity = 1,
                Price = 1
            };

            IActionResult notFoundResult = _itemController.Put(notExistedItemGuid, itemDTO);

            Assert.IsType<NotFoundResult>((NotFoundResult)notFoundResult);
        }

        [Fact]
        public void Put_WhenCalled_ReturnsOkResult()
        {
            Guid existedItemGuid = new Guid("f652bd2b-f456-455f-8b91-ac0fd1f190dd");

            string updatedItemName = $"Item {new Guid()}";

            ItemDTO itemDTO = new ItemDTO
            {
                Name = updatedItemName,
                Description = "Test item description 1",
                Quantity = 1,
                Price = 1
            };

            IActionResult okResult = _itemController.Put(existedItemGuid, itemDTO);

            Assert.IsType<OkObjectResult>((OkObjectResult)okResult);

            //Clearing
            itemDTO.Name = $"Item {existedItemGuid}";
            _itemController.Put(existedItemGuid, itemDTO);
        }

        [Fact]
        public void Delete_WhenCalled_ReturnsNoContentResult()
        {
            ItemDTO itemDTO = new ItemDTO
            {
                Name = "Test item name 1",
                Description = "Test item description 1",
                Quantity = 1,
                Price = 1
            };

            IActionResult createdAtActionResult = _itemController.Post(itemDTO);

            Assert.IsType<CreatedAtActionResult>((CreatedAtActionResult)createdAtActionResult);

            Guid createdItemId = ((Item)((CreatedAtActionResult)createdAtActionResult).Value).Id;

            IActionResult noContentResult = _itemController.Delete(createdItemId);

            Assert.IsType<NoContentResult>((NoContentResult)noContentResult);
        }

        [Fact]
        public void Delete_WhenCalled_ReturnsNotFoundResult()
        {
            Guid notExistedItemGuid = new Guid("1efa4eff-4ff9-44b5-b973-d08c09c3ff67");

            IActionResult notFoundResult = _itemController.Delete(notExistedItemGuid);

            Assert.IsType<NotFoundResult>((NotFoundResult)notFoundResult);
        }
    }
}