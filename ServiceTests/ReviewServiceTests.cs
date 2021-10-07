using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bnd.RestaurantReviews.Data;
using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using Bnd.RestaurantReviews.Services;
using Bnd.RestaurantReviews.Services.Helpers;
using Bnd.RestaurantReviews.ServiceTests.Fixtures;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.ServiceTests
{
    [TestClass]
    public class ReviewServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public ReviewServiceTests()
        {
            var serviceProvider = TestsDependencyResolver.GetServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceHelper = serviceProvider.GetService<IServiceHelper>();
        }

        [TestMethod]
        public async Task GetAll_should_return_all_reviews()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            List<Review> reviews;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                reviews = (await reviewService.GetAll()).ToList();
            }

            //Assert
            Assert.IsTrue(reviews.Count == 3);
        }

        [TestMethod]
        public async Task GetById_should_return_review_by_id()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Review review;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review = (await reviewService.GetById(1));
            }

            //Assert
            Assert.IsTrue(review.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetById_should_throw_key_not_found_exception_when_missing()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Review review;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review = (await reviewService.GetById(23));
            }

            //Assert
            Assert.IsTrue(review == null);
        }

        [TestMethod]
        public async Task Update_should_update_review()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Review review;

            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review = (await reviewService.GetById(1));
            }

            Assert.IsTrue(review.Id == 1 && review.Description == "Awesome");

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                review.Description = "Yuck";
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                await reviewService.Update(review.Id, _mapper.Map(review, new ReviewRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review = (await reviewService.GetById(1));
            }

            Assert.IsTrue(review.Id == 1 && review.Description == "Yuck");
        }

        [TestMethod]
        public async Task Create_should_create_review()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var review = new Review
            {
                Description = "Tasty!!!"
            };

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                await reviewService.Create(_mapper.Map(review, new ReviewRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review =
                    (await reviewService.GetAll()).FirstOrDefault(x => x.Description == "Tasty!!!");
            }

            Assert.IsTrue(review is { Description: "Tasty!!!" });
        }

        [TestMethod]
        public async Task Delete_should_delete_review()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var review = new Review
            {
                Description = "Delete Me"
            };

            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                await reviewService.Create(_mapper.Map(review, new ReviewRequest()));
            }

            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review =
                    (await reviewService.GetAll()).FirstOrDefault(x => x.Description == "Delete Me");
            }

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                await reviewService.Delete(review.Id);
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var reviewService = new ReviewService(context, _mapper, _serviceHelper);
                review =
                    (await reviewService.GetAll()).FirstOrDefault(x => x.Description == "Delete Me");

                Assert.IsTrue(review == null);
            }
        }

        private static DbContextOptions<ReviewsDataContext> SetupInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReviewsDataContext>()
                .UseInMemoryDatabase(databaseName: "ReviewsInMemoryDb")
                .Options;

            using var context = new ReviewsDataContext(options);
            context.Reviews.Add(new Review
            {
                Date = System.DateTime.Now.AddDays(-1),
                Description = "Awesome",
                Stars = 4.7D,
                UserId = 1,
                RestaurantId = 1
            });
            context.Reviews.Add(new Review
            {
                Date = System.DateTime.Now.AddMonths(-1),
                Description = "OK",
                Stars = 2.5D,
                UserId = 1,
                RestaurantId = 2
            });
            context.Reviews.Add(new Review
            {
                Date = System.DateTime.Now.AddMonths(-2),
                Description = "Lousy",
                Stars = 0.5D,
                UserId = 1,
                RestaurantId = 3
            });
            _ = context.SaveChangesAsync();

            return options;
        }
    }
}
