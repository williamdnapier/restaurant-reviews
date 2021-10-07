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
    public class RestaurantsControllerTests
    {
        [TestMethod]
        public async Task GetRestaurants_should_get_all_restaurants()
        {
            //Arrange
            var mockService = new Mock<IRestaurantService>();
            mockService.Setup(x => x.GetAll())
                .ReturnsAsync(GetTestRestaurants);
            var controller = new RestaurantsController(mockService.Object);

            //Act
            var actionResult = await controller.GetRestaurants();
            var objectResult = (OkObjectResult)actionResult.Result;
            var restaurants = (IEnumerable<Restaurant>)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<Restaurant>>));
            Assert.AreEqual(3, restaurants.Count());
        }

        [TestMethod]
        public async Task GetRestaurant_should_get_restaurant_by_id()
        {
            //Arrange
            var mockService = new Mock<IRestaurantService>();
            mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(GetTestRestaurants().FirstOrDefault);
            var controller = new RestaurantsController(mockService.Object);

            //Act
            var actionResult = await controller.GetRestaurant(1);
            var objectResult = (OkObjectResult)actionResult.Result;
            var restaurant = (Restaurant)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Restaurant>));
            Assert.AreEqual(1, restaurant.Id);
        }

        [TestMethod]
        public async Task PutRestaurant_should_update_restaurant()
        {
            //Arrange
            var mockService = new Mock<IRestaurantService>();

            mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<RestaurantRequest>()))
                .Returns(RestaurantAction);
            var controller = new RestaurantsController(mockService.Object);

            //Act
            var actionResult = await controller.PutRestaurant(It.IsAny<int>(), It.IsAny<RestaurantRequest>());
            var objectResult = (OkObjectResult)actionResult;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Restaurant updated successfully"));
        }

        [TestMethod]
        public async Task PostRestaurant_should_create_restaurant()
        {
            //Arrange
            var mockService = new Mock<IRestaurantService>();

            mockService.Setup(x => x.Create(It.IsAny<RestaurantRequest>()))
                .Returns(RestaurantAction);
            var controller = new RestaurantsController(mockService.Object);

            //Act
            var actionResult = await controller.PostRestaurant(It.IsAny<RestaurantRequest>());
            var objectResult = (OkObjectResult)actionResult.Result;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Restaurant>));
            Assert.IsTrue(msg.ToString().Contains("Restaurant created successfully"));
        }

        [TestMethod]
        public async Task DeleteRestaurant_should_delete_restaurant()
        {
            //Arrange
            var mockService = new Mock<IRestaurantService>();

            mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(RestaurantAction);
            var controller = new RestaurantsController(mockService.Object);

            //Act
            var actionResult = await controller.DeleteRestaurant(It.IsAny<int>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Restaurant deleted successfully"));
        }

        private static async Task<IActionResult> RestaurantAction()
        {
            await Task.Delay(10);
            return null;
        }

        private static List<Restaurant> GetTestRestaurants()
        {
            var restaurants = new List<Restaurant>
            {
                new()
                {
                    Id = 1,
                    CityId = 1,
                    MenuId = 1,
                    RestaurantTypeId = 1,
                    Name = "Bitter Ends Garden Luncheonette"
                },
                new()
                {
                    Id = 2,
                    CityId = 1,
                    MenuId = 2,
                    RestaurantTypeId = 2,
                    Name = "The Capital Grille"
                },
                new()
                {
                    Id = 3,
                    CityId = 1,
                    MenuId = 3,
                    RestaurantTypeId = 3,
                    Name = "Oak Hill Post"
                }
            };
            return restaurants;
        }
    }
}
