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
    public class CityServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public CityServiceTests()
        {
            var serviceProvider = TestsDependencyResolver.GetServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceHelper = serviceProvider.GetService<IServiceHelper>();
        }

        [TestMethod]
        public async Task GetAll_should_return_all_cities()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            List<City> cities;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                cities = (await cityService.GetAll()).ToList();
            }

            //Assert
            Assert.IsTrue(cities.Count == 3);
        }

        [TestMethod]
        public async Task GetById_should_return_city_by_id()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            City city;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetById(1));
            }

            //Assert
            Assert.IsTrue(city.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetById_should_throw_key_not_found_exception_when_missing()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            City city;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetById(23));
            }

            //Assert
            Assert.IsTrue(city == null);
        }

        [TestMethod]
        public async Task Update_should_update_city()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            City city;

            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetById(1));
            }

            Assert.IsTrue(city.Id == 1 && city.Name == "Pittsburgh, PA");

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                city.Name = "Steel City, PA";
                var cityService = new CityService(context, _mapper, _serviceHelper);
                await cityService.Update(city.Id, _mapper.Map(city, new CityRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetById(1));
            }

            Assert.IsTrue(city.Id == 1 && city.Name == "Steel City, PA");
        }

        [TestMethod]
        public async Task Create_should_create_city()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var city = new City
            {
                Name = "New Jack City"
            };

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                await cityService.Create(_mapper.Map(city, new CityRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetAll()).FirstOrDefault(x => x.Name == "New Jack City");
            }

            Assert.IsTrue(city is { Name: "New Jack City" });
        }

        [TestMethod]
        public async Task Delete_should_delete_city()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var city = new City { Name = "DeleteMe, US" };

            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                await cityService.Create(_mapper.Map(city, new CityRequest()));
            }

            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetAll()).FirstOrDefault(x => x.Name == "DeleteMe, US");
            }

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                await cityService.Delete(city.Id);
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var cityService = new CityService(context, _mapper, _serviceHelper);
                city = (await cityService.GetAll()).FirstOrDefault(x => x.Name == "DeleteMe, US");

                Assert.IsTrue(city == null);
            }
        }

        private static DbContextOptions<ReviewsDataContext> SetupInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReviewsDataContext>()
                .UseInMemoryDatabase(databaseName: "ReviewsInMemoryDb")
                .Options;

            using var context = new ReviewsDataContext(options);
            context.Cities.Add(new City { Name = "Pittsburgh, PA" });
            context.Cities.Add(new City { Name = "Barboursville, WV" });
            context.Cities.Add(new City { Name = "New York, NY" });
            _ = context.SaveChangesAsync();

            return options;
        }
    }
}
