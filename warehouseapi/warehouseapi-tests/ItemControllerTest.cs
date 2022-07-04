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
            ItemDTO itemDTO = new ItemDTO
            {
                Name = "Test item name 1",
                Description = "Test item description 1",
                Quantity = 1,
                Price = 1
            };

            IActionResult createdAtActionResult = _itemController.Post(itemDTO);

            Guid existedItemGuid = ((Item)((CreatedAtActionResult)createdAtActionResult).Value).Id;

            string updatedItemName = $"Item {new Guid()}";

            itemDTO.Name = updatedItemName;

            IActionResult okResult = _itemController.Put(existedItemGuid, itemDTO);

            Assert.IsType<OkObjectResult>((OkObjectResult)okResult);

            //Clearing
            itemDTO.Name = $"Item {existedItemGuid}";
            _itemController.Put(existedItemGuid, itemDTO);

            _itemController.Delete(existedItemGuid);
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