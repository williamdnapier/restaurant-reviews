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
    public class RestaurantTypesControllerTests
    {
        [TestMethod]
        public async Task GetRestaurantTypes_should_get_all_restaurantTypes()
        {
            //Arrange
            var mockService = new Mock<IRestaurantTypeService>();
            mockService.Setup(x => x.GetAll())
                .ReturnsAsync(GetTestRestaurantTypes);
            var controller = new RestaurantTypesController(mockService.Object);

            //Act
            var actionResult = await controller.GetRestaurantTypes();
            var objectResult = (OkObjectResult)actionResult.Result;
            var restaurantTypes = (IEnumerable<RestaurantType>)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<RestaurantType>>));
            Assert.AreEqual(3, restaurantTypes.Count());
        }

        [TestMethod]
        public async Task GetRestaurantType_should_get_restaurantType_by_id()
        {
            //Arrange
            var mockService = new Mock<IRestaurantTypeService>();
            mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(GetTestRestaurantTypes().FirstOrDefault);
            var controller = new RestaurantTypesController(mockService.Object);

            //Act
            var actionResult = await controller.GetRestaurantType(1);
            var objectResult = (OkObjectResult)actionResult.Result;
            var restaurantType = (RestaurantType)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<RestaurantType>));
            Assert.AreEqual(1, restaurantType.Id);
        }

        [TestMethod]
        public async Task PutRestaurantType_should_update_restaurantType()
        {
            //Arrange
            var mockService = new Mock<IRestaurantTypeService>();

            mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<RestaurantTypeRequest>()))
                .Returns(RestaurantTypeAction);
            var controller = new RestaurantTypesController(mockService.Object);

            //Act
            var actionResult = await controller.PutRestaurantType(It.IsAny<int>(), It.IsAny<RestaurantTypeRequest>());
            var objectResult = (OkObjectResult)actionResult;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Restaurant Type updated successfully"));
        }

        [TestMethod]
        public async Task PostRestaurantType_should_create_restaurantType()
        {
            //Arrange
            var mockService = new Mock<IRestaurantTypeService>();

            mockService.Setup(x => x.Create(It.IsAny<RestaurantTypeRequest>()))
                .Returns(RestaurantTypeAction);
            var controller = new RestaurantTypesController(mockService.Object);

            //Act
            var actionResult = await controller.PostRestaurantType(It.IsAny<RestaurantTypeRequest>());
            var objectResult = (OkObjectResult)actionResult.Result;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<RestaurantType>));
            Assert.IsTrue(msg.ToString().Contains("Restaurant Type created successfully"));
        }

        [TestMethod]
        public async Task DeleteRestaurantType_should_delete_restaurantType()
        {
            //Arrange
            var mockService = new Mock<IRestaurantTypeService>();

            mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(RestaurantTypeAction);
            var controller = new RestaurantTypesController(mockService.Object);

            //Act
            var actionResult = await controller.DeleteRestaurantType(It.IsAny<int>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Restaurant Type deleted successfully"));
        }

        private static async Task<IActionResult> RestaurantTypeAction()
        {
            await Task.Delay(10);
            return null;
        }

        private static List<RestaurantType> GetTestRestaurantTypes()
        {
            var restaurantTypes = new List<RestaurantType>
            {
                new() { Id = 1, Name = "Fine Dining" },
                new() { Id = 2, Name = "Casual Dining" },
                new() { Id = 3, Name = "Fast Casual" }
        };
            return restaurantTypes;
        }
    }
}
