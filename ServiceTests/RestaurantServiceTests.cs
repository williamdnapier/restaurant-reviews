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
    public class RestaurantServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public RestaurantServiceTests()
        {
            var serviceProvider = TestsDependencyResolver.GetServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceHelper = serviceProvider.GetService<IServiceHelper>();
        }

        [TestMethod]
        public async Task GetAll_should_return_all_restaurants()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            List<Restaurant> restaurants;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurants = (await restaurantService.GetAll()).ToList();
            }

            //Assert
            Assert.IsTrue(restaurants.Count == 3);
        }

        [TestMethod]
        public async Task GetById_should_return_restaurant_by_id()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Restaurant restaurant;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant = (await restaurantService.GetById(1));
            }

            //Assert
            Assert.IsTrue(restaurant.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetById_should_throw_key_not_found_exception_when_missing()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Restaurant restaurant;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant = (await restaurantService.GetById(23));
            }

            //Assert
            Assert.IsTrue(restaurant == null);
        }

        [TestMethod]
        public async Task Update_should_update_restaurant()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Restaurant restaurant;

            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant = (await restaurantService.GetById(1));
            }

            Assert.IsTrue(restaurant.Id == 1 && restaurant.Name == "Bitter Ends Garden Luncheonette");

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                restaurant.Name = "Sweet Ends Garden Luncheonette";
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                await restaurantService.Update(restaurant.Id, _mapper.Map(restaurant, new RestaurantRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant = (await restaurantService.GetById(1));
            }

            Assert.IsTrue(restaurant.Id == 1 && restaurant.Name == "Sweet Ends Garden Luncheonette");
        }

        [TestMethod]
        public async Task Create_should_create_restaurant()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var restaurant = new Restaurant
            {
                CityId = 6,
                MenuId = 5,
                RestaurantTypeId = 3,
                Name = "Tasty Test Suite"
            };

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                await restaurantService.Create(_mapper.Map(restaurant, new RestaurantRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant =
                    (await restaurantService.GetAll()).FirstOrDefault(x => x.Name == "Tasty Test Suite");
            }

            Assert.IsTrue(restaurant is { Name: "Tasty Test Suite" });
        }

        [TestMethod]
        public async Task Delete_should_delete_restaurant()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var restaurant = new Restaurant
            {
                CityId = 6,
                MenuId = 5,
                RestaurantTypeId = 3,
                Name = "Delete Me"
            };

            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                await restaurantService.Create(_mapper.Map(restaurant, new RestaurantRequest()));
            }

            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant =
                    (await restaurantService.GetAll()).FirstOrDefault(x => x.Name == "Delete Me");
            }

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                await restaurantService.Delete(restaurant.Id);
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantService = new RestaurantService(context, _mapper, _serviceHelper);
                restaurant =
                    (await restaurantService.GetAll()).FirstOrDefault(x => x.Name == "Delete Me");

                Assert.IsTrue(restaurant == null);
            }
        }

        private static DbContextOptions<ReviewsDataContext> SetupInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReviewsDataContext>()
                .UseInMemoryDatabase(databaseName: "ReviewsInMemoryDb")
                .Options;

            using var context = new ReviewsDataContext(options);
            context.Restaurants.Add(new Restaurant { Id = 1, CityId = 1, MenuId = 1, RestaurantTypeId = 1, Name = "Bitter Ends Garden Luncheonette" });
            context.Restaurants.Add(new Restaurant { Id = 2, CityId = 1, MenuId = 2, RestaurantTypeId = 2, Name = "The Capital Grille" });
            context.Restaurants.Add(new Restaurant { Id = 3, CityId = 1, MenuId = 3, RestaurantTypeId = 3, Name = "Oak Hill Post" });
            _ = context.SaveChangesAsync();

            return options;
        }
    }
}
