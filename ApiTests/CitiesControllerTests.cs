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
    public class CitiesControllerTests
    {
        [TestMethod]
        public async Task GetCities_should_get_all_cities()
        {
            //Arrange
            var mockService = new Mock<ICityService>();
            mockService.Setup(x => x.GetAll())
                .ReturnsAsync(GetTestCities);
            var controller = new CitiesController(mockService.Object);

            //Act
            var actionResult = await controller.GetCities();
            var objectResult = (OkObjectResult)actionResult.Result;
            var cities = (IEnumerable<City>)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<City>>));
            Assert.AreEqual(3, cities.Count());
        }

        [TestMethod]
        public async Task GetCity_should_get_city_by_id()
        {
            //Arrange
            var mockService = new Mock<ICityService>();
            mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(GetTestCities().FirstOrDefault);
            var controller = new CitiesController(mockService.Object);

            //Act
            var actionResult = await controller.GetCity(1);
            var objectResult = (OkObjectResult)actionResult.Result;
            var city = (City)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<City>));
            Assert.AreEqual(1, city.Id);
        }

        [TestMethod]
        public async Task PutCity_should_update_city()
        {
            //Arrange
            var mockService = new Mock<ICityService>();

            mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<CityRequest>()))
                .Returns(CityAction);
            var controller = new CitiesController(mockService.Object);

            //Act
            var actionResult = await controller.PutCity(It.IsAny<int>(), It.IsAny<CityRequest>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("City updated successfully"));
        }

        [TestMethod]
        public async Task PostCity_should_create_city()
        {
            //Arrange
            var mockService = new Mock<ICityService>();

            mockService.Setup(x => x.Create(It.IsAny<CityRequest>()))
                .Returns(CityAction);
            var controller = new CitiesController(mockService.Object);

            //Act
            var actionResult = await controller.PostCity(It.IsAny<CityRequest>());
            var objectResult = (OkObjectResult)actionResult.Result;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<City>));
            Assert.IsTrue(msg.ToString().Contains("City created successfully"));
        }

        [TestMethod]
        public async Task DeleteCity_should_delete_city()
        {
            //Arrange
            var mockService = new Mock<ICityService>();

            mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(CityAction);
            var controller = new CitiesController(mockService.Object);

            //Act
            var actionResult = await controller.DeleteCity(It.IsAny<int>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("City deleted successfully"));
        }

        private static async Task<IActionResult> CityAction()
        {
            await Task.Delay(10);
            return null;
        }

        private static List<City> GetTestCities()
        {
            var cities = new List<City>
            {
                new() { Id = 1, Name = "Pittsburgh, PA" },
                new() { Id = 2, Name = "Barboursville, WV" },
                new() { Id = 3, Name = "New York, NY" }
            };
            return cities;
        }
    }
}
