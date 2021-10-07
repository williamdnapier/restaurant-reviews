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
    public class CriticServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public CriticServiceTests()
        {
            var serviceProvider = TestsDependencyResolver.GetServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceHelper = serviceProvider.GetService<IServiceHelper>();
        }

        [TestMethod]
        public async Task GetAll_should_return_all_critics()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            List<Critic> critics;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critics = (await criticService.GetAll()).ToList();
            }

            //Assert
            Assert.IsTrue(critics.Count == 3);
        }

        [TestMethod]
        public async Task GetById_should_return_critic_by_id()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Critic critic;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetById(1));
            }

            //Assert
            Assert.IsTrue(critic.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetById_should_throw_key_not_found_exception_when_missing()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Critic critic;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetById(23));
            }

            //Assert
            Assert.IsTrue(critic == null);
        }

        [TestMethod]
        public async Task Update_should_update_Critic()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Critic critic;

            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetById(1));
            }

            Assert.IsTrue(critic.Id == 1 && critic.FirstName == "Bill");

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                critic.FirstName = "William";
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                await criticService.Update(critic.Id, _mapper.Map(critic, new CriticRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetById(1));
            }

            Assert.IsTrue(critic.Id == 1 && critic.FirstName == "William");
        }

        [TestMethod]
        public async Task Create_should_create_Critic()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var critic = new Critic { FirstName = "Isaac", LastName = "Napier" };

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                await criticService.Create(_mapper.Map(critic, new CriticRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetAll()).FirstOrDefault(x => x.FirstName == "Isaac");
            }

            Assert.IsTrue(critic is { FirstName: "Isaac" });
        }

        [TestMethod]
        public async Task Delete_should_delete_Critic()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var critic = new Critic { FirstName = "Delete", LastName = "Me" };

            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                await criticService.Create(_mapper.Map(critic, new CriticRequest()));
            }

            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetAll()).FirstOrDefault(x => x.FirstName == "Delete");
            }

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                await criticService.Delete(critic.Id);
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var criticService = new CriticService(context, _mapper, _serviceHelper);
                critic = (await criticService.GetAll()).FirstOrDefault(x => x.FirstName == "Delete");

                Assert.IsTrue(critic == null);
            }
        }

        private static DbContextOptions<ReviewsDataContext> SetupInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReviewsDataContext>()
                .UseInMemoryDatabase(databaseName: "ReviewsInMemoryDb")
                .Options;

            using var context = new ReviewsDataContext(options);
            context.Critics.Add(new Critic { FirstName = "Bill", LastName = "Napier" });
            context.Critics.Add(new Critic { FirstName = "Denise", LastName = "Napier" });
            context.Critics.Add(new Critic { FirstName = "Grace", LastName = "Napier" });
            _ = context.SaveChangesAsync();

            return options;
        }
    }
}
