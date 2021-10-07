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
    public class CriticsControllerTests
    {
        [TestMethod]
        public async Task GetCritics_should_get_all_critics()
        {
            //Arrange
            var mockService = new Mock<ICriticService>();
            mockService.Setup(x => x.GetAll())
                .ReturnsAsync(GetTestCritics);
            var controller = new CriticsController(mockService.Object);

            //Act
            var actionResult = await controller.GetCritics();
            var objectResult = (OkObjectResult)actionResult.Result;
            var critics = (IEnumerable<Critic>)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<Critic>>));
            Assert.AreEqual(3, critics.Count());
        }

        [TestMethod]
        public async Task GetCritic_should_get_critic_by_id()
        {
            //Arrange
            var mockService = new Mock<ICriticService>();
            mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(GetTestCritics().FirstOrDefault);
            var controller = new CriticsController(mockService.Object);

            //Act
            var actionResult = await controller.GetCritic(1);
            var objectResult = (OkObjectResult)actionResult.Result;
            var critic = (Critic)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Critic>));
            Assert.AreEqual(1, critic.Id);
        }

        [TestMethod]
        public async Task PutCritic_should_update_critic()
        {
            //Arrange
            var mockService = new Mock<ICriticService>();

            mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<CriticRequest>()))
                .Returns(CriticAction);
            var controller = new CriticsController(mockService.Object);

            //Act
            var actionResult = await controller.PutCritic(It.IsAny<int>(), It.IsAny<CriticRequest>());
            var objectResult = (OkObjectResult)actionResult;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Critic updated successfully"));
        }

        [TestMethod]
        public async Task PostCritic_should_create_critic()
        {
            //Arrange
            var mockService = new Mock<ICriticService>();

            mockService.Setup(x => x.Create(It.IsAny<CriticRequest>()))
                .Returns(CriticAction);
            var controller = new CriticsController(mockService.Object);

            //Act
            var actionResult = await controller.PostCritic(It.IsAny<CriticRequest>());
            var objectResult = (OkObjectResult)actionResult.Result;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Critic>));
            Assert.IsTrue(msg.ToString().Contains("Critic created successfully"));
        }

        [TestMethod]
        public async Task DeleteCritic_should_delete_critic()
        {
            //Arrange
            var mockService = new Mock<ICriticService>();

            mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(CriticAction);
            var controller = new CriticsController(mockService.Object);

            //Act
            var actionResult = await controller.DeleteCritic(It.IsAny<int>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Critic deleted successfully"));
        }

        private static async Task<IActionResult> CriticAction()
        {
            await Task.Delay(10);
            return null;
        }

        private static List<Critic> GetTestCritics()
        {
            var critics = new List<Critic>
            {
                new() { Id = 1, FirstName = "Denise", LastName = "Napier" },
                new() { Id = 2, FirstName = "Grace", LastName = "Napier" },
                new() { Id = 3, FirstName = "Isaac", LastName = "Napier" }
        };
            return critics;
        }
    }
}
