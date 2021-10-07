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
    public class MenuServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public MenuServiceTests()
        {
            var serviceProvider = TestsDependencyResolver.GetServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
            _serviceHelper = serviceProvider.GetService<IServiceHelper>();
        }

        [TestMethod]
        public async Task GetAll_should_return_all_menus()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            List<Menu> menus;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menus = (await menuService.GetAll()).ToList();
            }

            //Assert
            Assert.IsTrue(menus.Count == 3);
        }

        [TestMethod]
        public async Task GetById_should_return_menu_by_id()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Menu menu;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu = (await menuService.GetById(1));
            }

            //Assert
            Assert.IsTrue(menu.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task GetById_should_throw_key_not_found_exception_when_missing()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Menu menu;

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu = (await menuService.GetById(23));
            }

            //Assert
            Assert.IsTrue(menu == null);
        }

        [TestMethod]
        public async Task Update_should_update_menu()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            Menu menu;

            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu = (await menuService.GetById(1));
            }

            Assert.IsTrue(menu.Id == 1 && menu.Name == "Standard Fare");

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                menu.Name = "Updated Options";
                menu.Items = "Chocolate Cake, Ice Cream, Halloween Candy";
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                await menuService.Update(menu.Id, _mapper.Map(menu, new MenuRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu = (await menuService.GetById(1));
            }

            Assert.IsTrue(menu.Id == 1 && menu.Name == "Updated Options" && menu.Items.Contains("Halloween Candy"));
        }

        [TestMethod]
        public async Task Create_should_create_menu()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var menu = new Menu
            {
                Items =
                    "Lentils, Bean Broth, Turkey, Roast Duck, Celery, Carrots, Potato",
                Name = "Options"
            };

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                await menuService.Create(_mapper.Map(menu, new MenuRequest()));
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu =
                    (await menuService.GetAll()).FirstOrDefault(x => x.Name == "Options");
            }

            Assert.IsTrue(menu is { Name: "Options" });
        }

        [TestMethod]
        public async Task Delete_should_delete_Menu()
        {
            //Arrange
            var options = SetupInMemoryDbOptions();
            var menu = new Menu
            {
                Name = "Delete Me"
            };

            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                await menuService.Create(_mapper.Map(menu, new MenuRequest()));
            }

            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu =
                    (await menuService.GetAll()).FirstOrDefault(x => x.Name == "Delete Me");
            }

            //Act
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                await menuService.Delete(menu.Id);
            }

            //Assert
            await using (var context = new ReviewsDataContext(options))
            {
                var menuService = new MenuService(context, _mapper, _serviceHelper);
                menu =
                    (await menuService.GetAll()).FirstOrDefault(x => x.Name == "Delete Me");

                Assert.IsTrue(menu == null);
            }
        }

        private static DbContextOptions<ReviewsDataContext> SetupInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReviewsDataContext>()
                .UseInMemoryDatabase(databaseName: "ReviewsInMemoryDb")
                .Options;

            using var context = new ReviewsDataContext(options);
            context.Menus.Add(new Menu
            {
                Items =
                    "Chicken pot pie, Mashed potatoes, Fried chicken, Burgers, Chicken soup",
                Name = "Standard Fare"
            });
            context.Menus.Add(new Menu
            {
                Items =
                    "Meatloaf, Lasagna, Spaghetti with meatballs, Chicken burger, Chicken parmesan, Chicken Pesto, Burger Sliders",
                Name = "Basic Options"
            });
            context.Menus.Add(new Menu
            {
                Items =
                    "Burgers with locally sourced beef or chicken, Salads with ingredients from nearby farms, Lobster rolls, shrimp, grilled fish if you are next to a fresh body of water",
                Name = "Locally Sourced"
            });
            _ = context.SaveChangesAsync();

            return options;
        }
    }
}
