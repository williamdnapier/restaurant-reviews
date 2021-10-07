using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bnd.RestaurantReviews.Api.Controllers;
using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using Bnd.RestaurantReviews.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.ApiTests
{
    [TestClass]
    public class MenusControllerTests
    {
        [TestMethod]
        public async Task GetMenus_should_get_all_menus()
        {
            //Arrange
            var mockService = new Mock<IMenuService>();
            mockService.Setup(x => x.GetAll())
                .ReturnsAsync(GetTestMenus);
            var controller = new MenusController(mockService.Object);

            //Act
            var actionResult = await controller.GetMenus();
            var objectResult = (OkObjectResult)actionResult.Result;
            var menus = (IEnumerable<Menu>)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<Menu>>));
            Assert.AreEqual(3, menus.Count());
        }

        [TestMethod]
        public async Task GetMenu_should_get_menu_by_id()
        {
            //Arrange
            var mockService = new Mock<IMenuService>();
            mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(GetTestMenus().FirstOrDefault);
            var controller = new MenusController(mockService.Object);

            //Act
            var actionResult = await controller.GetMenu(1);
            var objectResult = (OkObjectResult)actionResult.Result;
            var menu = (Menu)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Menu>));
            Assert.AreEqual(1, menu.Id);
        }

        [TestMethod]
        public async Task PutMenu_should_update_menu()
        {
            //Arrange
            var mockService = new Mock<IMenuService>();

            mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<MenuRequest>()))
                .Returns(MenuAction);
            var controller = new MenusController(mockService.Object);

            //Act
            var actionResult = await controller.PutMenu(It.IsAny<int>(), It.IsAny<MenuRequest>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Menu updated successfully"));
        }

        [TestMethod]
        public async Task PostMenu_should_create_menu()
        {
            //Arrange
            var mockService = new Mock<IMenuService>();

            mockService.Setup(x => x.Create(It.IsAny<MenuRequest>()))
                .Returns(MenuAction);
            var controller = new MenusController(mockService.Object);

            //Act
            var actionResult = await controller.PostMenu(It.IsAny<MenuRequest>());
            var objectResult = (OkObjectResult)actionResult.Result;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Menu>));
            Assert.IsTrue(msg.ToString().Contains("Menu created successfully"));
        }

        [TestMethod]
        public async Task DeleteMenu_should_delete_menu()
        {
            //Arrange
            var mockService = new Mock<IMenuService>();

            mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(MenuAction);
            var controller = new MenusController(mockService.Object);

            //Act
            var actionResult = await controller.DeleteMenu(It.IsAny<int>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Menu deleted successfully"));
        }

        private static async Task<IActionResult> MenuAction()
        {
            await Task.Delay(10);
            return null;
        }

        private static List<Menu> GetTestMenus()
        {
            var menus = new List<Menu>
            {
                new() { Id = 1, Items = "Chicken pot pie, Mashed potatoes, Fried chicken, Burgers, Chicken soup", Name = "Standard Fare" },
                new() { Id = 2, Items = "Meatloaf, Lasagna, Spaghetti with meatballs, Chicken burger, Chicken parmesan, Chicken Pesto, Burger Sliders", Name = "Basic Options" },
                new() { Id = 3, Items = "Burgers with locally sourced beef or chicken, Salads with ingredients from nearby farms, Lobster rolls, shrimp, grilled fish if you are next to a fresh body of water", Name = "Locally Sourced" }
            };
            return menus;
        }
    }
}
