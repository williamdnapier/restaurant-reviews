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
    public class ReviewsControllerTests
    {
        [TestMethod]
        public async Task GetReviews_should_get_all_reviews()
        {
            //Arrange
            var mockService = new Mock<IReviewService>();
            mockService.Setup(x => x.GetAll())
                .ReturnsAsync(GetTestReviews);
            var controller = new ReviewsController(mockService.Object);

            //Act
            var actionResult = await controller.GetReviews();
            var objectResult = (OkObjectResult)actionResult.Result;
            var reviews = (IEnumerable<Review>)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IEnumerable<Review>>));
            Assert.AreEqual(3, reviews.Count());
        }

        [TestMethod]
        public async Task GetReview_should_get_review_by_id()
        {
            //Arrange
            var mockService = new Mock<IReviewService>();
            mockService.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(GetTestReviews().FirstOrDefault);
            var controller = new ReviewsController(mockService.Object);

            //Act
            var actionResult = await controller.GetReview(1);
            var objectResult = (OkObjectResult)actionResult.Result;
            var review = (Review)objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Review>));
            Assert.AreEqual(1, review.Id);
        }

        [TestMethod]
        public async Task PutReview_should_update_review()
        {
            //Arrange
            var mockService = new Mock<IReviewService>();

            mockService.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<ReviewRequest>()))
                .Returns(ReviewAction);
            var controller = new ReviewsController(mockService.Object);

            //Act
            var actionResult = await controller.PutReview(It.IsAny<int>(), It.IsAny<ReviewRequest>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Review updated successfully"));
        }

        [TestMethod]
        public async Task PostReview_should_create_review()
        {
            //Arrange
            var mockService = new Mock<IReviewService>();

            mockService.Setup(x => x.Create(It.IsAny<ReviewRequest>()))
                .Returns(ReviewAction);
            var controller = new ReviewsController(mockService.Object);

            //Act
            var actionResult = await controller.PostReview(It.IsAny<ReviewRequest>());
            var objectResult = (OkObjectResult)actionResult.Result;
            var msg = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Review>));
            Assert.IsTrue(msg.ToString().Contains("Review created successfully"));
        }

        [TestMethod]
        public async Task DeleteReview_should_delete_review()
        {
            //Arrange
            var mockService = new Mock<IReviewService>();

            mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(ReviewAction);
            var controller = new ReviewsController(mockService.Object);

            //Act
            var actionResult = await controller.DeleteReview(It.IsAny<int>());
            var objectResult = (OkObjectResult)actionResult;

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            Assert.IsTrue(objectResult.Value.ToString().Contains("Review deleted successfully"));
        }

        private static async Task<IActionResult> ReviewAction()
        {
            await Task.Delay(10);
            return null;
        }

        private static List<Review> GetTestReviews()
        {
            var reviews = new List<Review>
            {
                new()
                {
                    Id = 1,
                    Date = System.DateTime.Now.AddDays(-1),
                    Description = "Awesome",
                    Stars = 4.7D,
                    UserId = 1,
                    RestaurantId = 1
                },
                new()
                {
                    Id = 2,
                    Date = System.DateTime.Now.AddMonths(-1),
                    Description = "OK",
                    Stars = 2.5D,
                    UserId = 1,
                    RestaurantId = 2
                },
                new()
                {
                    Id = 3,
                    Date = System.DateTime.Now.AddMonths(-2),
                    Description = "Lousy",
                    Stars = 0.5D,
                    UserId = 1,
                    RestaurantId = 3
                }
            };
            return reviews;
        }
    }
}
