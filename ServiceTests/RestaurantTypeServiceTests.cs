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
    public class RestaurantTypeServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public RestaurantTypeServiceTests()
        {
            var serviceProvider = TestsDependencyResolver.GetServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceHelper = serviceProvider.GetService<IServiceHelper>();
        }

        [TestMethod]
        public async Task GetAll_should_return_all_restaurantTypes()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            List<RestaurantType> restaurantTypes;

            //Acts
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantTypes = (await restaurantTypeService.GetAll()).ToList();
            }

            //Assert
            Assert.IsTrue(restaurantTypes.Count == 3);
        }

        [TestMethod]
        public async Task GetById_should_return_restaurantType_by_id()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            RestaurantType restaurantType;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType = (await restaurantTypeService.GetById(1));
            }

            //Assert
            Assert.IsTrue(restaurantType.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetById_should_throw_key_not_found_exception_when_missing()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            RestaurantType restaurantType;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType = (await restaurantTypeService.GetById(23));
            }

            //Assert
            Assert.IsTrue(restaurantType == null);
        }

        [TestMethod]
        public async Task Update_should_update_restaurantType()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            RestaurantType restaurantType;

            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType = (await restaurantTypeService.GetById(1));
            }

            Assert.IsTrue(restaurantType.Id == 1 && restaurantType.Name == "Fine Dining");

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                restaurantType.Name = "Slow Service";
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                await restaurantTypeService.Update(restaurantType.Id, _mapper.Map(restaurantType, new RestaurantTypeRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType = (await restaurantTypeService.GetById(1));
            }

            Assert.IsTrue(restaurantType.Id == 1 && restaurantType.Name == "Slow Service");
        }

        [TestMethod]
        public async Task Create_should_create_restaurantType()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var restaurantType = new RestaurantType
            {
                Name = "Tasty Type"
            };

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                await restaurantTypeService.Create(_mapper.Map(restaurantType, new RestaurantTypeRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType =
                    (await restaurantTypeService.GetAll()).FirstOrDefault(x => x.Name == "Tasty Type");
            }

            Assert.IsTrue(restaurantType is { Name: "Tasty Type" });
        }

        [TestMethod]
        public async Task Delete_should_delete_restaurantType()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var restaurantType = new RestaurantType
            {
                Name = "Delete Me"
            };

            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                await restaurantTypeService.Create(_mapper.Map(restaurantType, new RestaurantTypeRequest()));
            }

            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType =
                    (await restaurantTypeService.GetAll()).FirstOrDefault(x => x.Name == "Delete Me");
            }

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                await restaurantTypeService.Delete(restaurantType.Id);
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var restaurantTypeService = new RestaurantTypeService(context, _mapper, _serviceHelper);
                restaurantType =
                    (await restaurantTypeService.GetAll()).FirstOrDefault(x => x.Name == "Delete Me");

                Assert.IsTrue(restaurantType == null);
            }
        }

        private static DbContextOptions<ReviewsDataContext> SetupInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReviewsDataContext>()
                .UseInMemoryDatabase(databaseName: "ReviewsInMemoryDb")
                .Options;

            using var context = new ReviewsDataContext(options);
            context.RestaurantTypes.Add(new RestaurantType { Name = "Fine Dining" });
            context.RestaurantTypes.Add(new RestaurantType { Name = "Casual Dining" });
            context.RestaurantTypes.Add(new RestaurantType { Name = "Fast Casual" });
            _ = context.SaveChangesAsync();

            return options;
        }
    }
}
