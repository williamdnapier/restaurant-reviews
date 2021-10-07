using Bnd.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bnd.RestaurantReviews.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReviewsDataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Reviews.Any())
            {
                return;
            }

            InsertCritics(context);
            InsertCities(context);
            InsertMenus(context);
            InsertRestaurantTypes(context);
            InsertRestaurants(context);
            InsertReviews(context);
        }

        private static void InsertRestaurants(ReviewsDataContext context)
        {
            var restaurants = new List<Restaurant>();
            var restaurant1 = new Restaurant
            {
                CityId = 1,
                MenuId = 1,
                RestaurantTypeId = 1,
                Name = "Bitter Ends Garden Luncheonette"
            };
            var restaurant2 = new Restaurant
            {
                CityId = 1,
                MenuId = 2,
                RestaurantTypeId = 2,
                Name = "The Capital Grille"
            };
            var restaurant3 = new Restaurant
            {
                CityId = 1,
                MenuId = 3,
                RestaurantTypeId = 3,
                Name = "Oak Hill Post"
            };
            var restaurant4 = new Restaurant
            {
                CityId = 1,
                MenuId = 4,
                RestaurantTypeId = 4,
                Name = "Savor Bread"
            };
            var restaurant5 = new Restaurant
            {
                CityId = 2,
                MenuId = 5,
                RestaurantTypeId = 5,
                Name = "Food Wish"
            };
            var restaurant6 = new Restaurant
            {
                CityId = 2,
                MenuId = 6,
                RestaurantTypeId = 6,
                Name = "Feed Logic"
            };
            var restaurant7 = new Restaurant
            {
                CityId = 3,
                MenuId = 7,
                RestaurantTypeId = 7,
                Name = "Burgerpad"
            };
            var restaurant8 = new Restaurant
            {
                CityId = 4,
                MenuId = 8,
                RestaurantTypeId = 1,
                Name = "Alluring Lo"
            };
            var restaurant9 = new Restaurant
            {
                CityId = 4,
                MenuId = 2,
                RestaurantTypeId = 2,
                Name = "Spices Mein"
            };
            var restaurant10 = new Restaurant
            {
                CityId = 5,
                MenuId = 4,
                RestaurantTypeId = 4,
                Name = "Tasty Noodle"
            };
            var restaurant11 = new Restaurant
            {
                CityId = 5,
                MenuId = 6,
                RestaurantTypeId = 7,
                Name = "Diced Bean"
            };
            var restaurant12 = new Restaurant
            {
                CityId = 5,
                MenuId = 7,
                RestaurantTypeId = 3,
                Name = "Cuisine Lemo"
            };

            restaurants.Add(restaurant1);
            restaurants.Add(restaurant2);
            restaurants.Add(restaurant3);
            restaurants.Add(restaurant4);
            restaurants.Add(restaurant5);
            restaurants.Add(restaurant6);
            restaurants.Add(restaurant7);
            restaurants.Add(restaurant8);
            restaurants.Add(restaurant9);
            restaurants.Add(restaurant10);
            restaurants.Add(restaurant11);
            restaurants.Add(restaurant12);

            foreach (var restaurant in restaurants)
            {
                context.Restaurants.Add(restaurant);
            }

            context.SaveChanges();
        }

        private static void InsertCities(ReviewsDataContext context)
        {
            var cities = new List<City>();
            var city1 = new City { Name = "Pittsburgh, PA" };
            var city2 = new City { Name = "Barboursville, WV" };
            var city3 = new City { Name = "New York, NY" };
            var city4 = new City { Name = "San Francisco, CA" };
            var city5 = new City { Name = "Dallas, TX" };
            cities.Add(city1);
            cities.Add(city2);
            cities.Add(city3);
            cities.Add(city4);
            cities.Add(city5);

            foreach (var city in cities)
            {
                context.Cities.Add(city);
            }

            context.SaveChanges();
        }

        private static void InsertMenus(ReviewsDataContext context)
        {
            var menus = new List<Menu>();
            var menu1 = new Menu
            {
                Items =
                    "Chicken pot pie, Mashed potatoes, Fried chicken, Burgers, Chicken soup",
                Name = "Standard Fare"
            };
            var menu2 = new Menu
            {
                Items =
                    "Meatloaf, Lasagna, Spaghetti with meatballs, Chicken burger, Chicken parmesan, Chicken Pesto, Burger Sliders",
                Name = "Basic Options"
            };
            var menu3 = new Menu
            {
                Items =
                    "Burgers with locally sourced beef or chicken, Salads with ingredients from nearby farms, Lobster rolls, shrimp, grilled fish if you are next to a fresh body of water",
                Name = "Locally Sourced"
            };
            var menu4 = new Menu
            {
                Items =
                    "Grilled cheese, Tomato soup, Chicken fingers, Flatbread pizza, Mac & cheese, Mini burgers, Mini pizzas",
                Name = "Kid Friendly"
            };
            var menu5 = new Menu
            {
                Items =
                    "Spaghetti with meatballs, Chicken burger, Mac & cheese, Mini burgers, Mashed potatoes, Fried chicken",
                Name = "Easy Breezy"
            };
            var menu6 = new Menu
            {
                Items =
                    "Mashed potatoes, Fried chicken, Mac & cheese, Mini burgers, Salads with ingredients from nearby farms, Tomato soup, Chicken fingers",
                Name = "Traditional"
            };
            var menu7 = new Menu
            {
                Items =
                    "Burgers, Chicken soup, Fries, Flatbread pizza, Mac & cheese, Mini burgers",
                Name = "No Brainer"
            };
            var menu8 = new Menu
            {
                Items =
                    "Apple pie, Pumpkin pie, Giant chocolate chip cookies, Banana split, Molten lava cakes, Cinnamon rolls, Cheesecake",
                Name = "Just Desserts"
            };
            menus.Add(menu1);
            menus.Add(menu2);
            menus.Add(menu3);
            menus.Add(menu4);
            menus.Add(menu5);
            menus.Add(menu6);
            menus.Add(menu7);
            menus.Add(menu8);

            foreach (var menu in menus)
            {
                context.Menus.Add(menu);
            }

            context.SaveChanges();
        }

        private static void InsertRestaurantTypes(ReviewsDataContext context)
        {
            var restaurantTypes = new List<RestaurantType>();

            var restaurantType1 = new RestaurantType { Name = "Fine Dining" };
            var restaurantType2 = new RestaurantType { Name = "Casual Dining" };
            var restaurantType3 = new RestaurantType { Name = "Fast Casual" };
            var restaurantType4 = new RestaurantType { Name = "Fast Food" };
            var restaurantType5 = new RestaurantType { Name = "Cafe" };
            var restaurantType6 = new RestaurantType { Name = "Food Truck, Cart, Or Stand" };
            var restaurantType7 = new RestaurantType { Name = "Family Style" };

            restaurantTypes.Add(restaurantType1);
            restaurantTypes.Add(restaurantType2);
            restaurantTypes.Add(restaurantType3);
            restaurantTypes.Add(restaurantType4);
            restaurantTypes.Add(restaurantType5);
            restaurantTypes.Add(restaurantType6);
            restaurantTypes.Add(restaurantType7);

            foreach (var restaurantType in restaurantTypes)
            {
                context.RestaurantTypes.Add(restaurantType);
            }

            context.SaveChanges();
        }

        private static void InsertCritics(ReviewsDataContext context)
        {
            var critics = new List<Critic>();

            var critic1 = new Critic { FirstName = "Bill", LastName = "Napier" };
            var critic2 = new Critic { FirstName = "Denise", LastName = "Napier" };
            var critic3 = new Critic { FirstName = "Grace", LastName = "Napier" };
            var critic4 = new Critic { FirstName = "Isaac", LastName = "Napier" };

            critics.Add(critic1);
            critics.Add(critic2);
            critics.Add(critic3);
            critics.Add(critic4);

            foreach (var critic in critics)
            {
                context.Critics.Add(critic);
            }

            context.SaveChanges();
        }

        private static void InsertReviews(ReviewsDataContext context)
        {
            var reviews = new List<Review>();

            var review1 = new Review
            {
                Date = System.DateTime.Now.AddDays(-1),
                Description = "Awesome",
                Stars = 4.7D,
                UserId = 1,
                RestaurantId = 1
            };

            var review2 = new Review
            {
                Date = System.DateTime.Now.AddMonths(-1),
                Description = "OK",
                Stars = 2.5D,
                UserId = 1,
                RestaurantId = 2
            };

            var review3 = new Review
            {
                Date = System.DateTime.Now.AddMonths(-2),
                Description = "Lousy",
                Stars = 0.5D,
                UserId = 1,
                RestaurantId = 3
            };

            var review4 = new Review
            {
                Date = System.DateTime.Now.AddDays(-1).AddMonths(-2),
                Description = "Not Great",
                Stars = 3.1D,
                UserId = 2,
                RestaurantId = 4
            };

            var review5 = new Review
            {
                Date = System.DateTime.Now.AddDays(-3).AddMonths(-1),
                Description = "So so",
                Stars = 2.5D,
                UserId = 2,
                RestaurantId = 5
            };

            var review6 = new Review
            {
                Date = System.DateTime.Now.AddDays(-7).AddMonths(-3),
                Description = "Top Notch",
                Stars = 4.9D,
                UserId = 3,
                RestaurantId = 6
            };

            var review7 = new Review
            {
                Date = System.DateTime.Now.AddDays(-9).AddMonths(-2),
                Description = "Middle of pack",
                Stars = 3.0D,
                UserId = 3,
                RestaurantId = 7
            };

            var review8 = new Review
            {
                Date = System.DateTime.Now.AddDays(-4).AddMonths(-5),
                Description = "Killin It",
                Stars = 4.3D,
                UserId = 4,
                RestaurantId = 8
            };

            var review9 = new Review
            {
                Date = System.DateTime.Now.AddDays(-7).AddMonths(-2),
                Description = "Could be better",
                Stars = 2.3D,
                UserId = 4,
                RestaurantId = 10
            };

            var review10 = new Review
            {
                Date = System.DateTime.Now.AddDays(-5),
                Description = "Fantastic",
                Stars = 4.1D,
                UserId = 4,
                RestaurantId = 1
            };

            var review11 = new Review
            {
                Date = System.DateTime.Now.AddDays(-11),
                Description = "Literally best ever!",
                Stars = 4.9D,
                UserId = 3,
                RestaurantId = 2
            };

            var review12 = new Review
            {
                Date = System.DateTime.Now.AddDays(-11),
                Description = "Boo. Its them not me.",
                Stars = 1.2D,
                UserId = 2,
                RestaurantId = 3
            };

            var review13 = new Review
            {
                Date = System.DateTime.Now.AddDays(-22),
                Description = "Yummy, sooo good",
                Stars = 3.6D,
                UserId = 3,
                RestaurantId = 5
            };

            var review14 = new Review
            {
                Date = System.DateTime.Now.AddDays(-17),
                Description = "Ah ok",
                Stars = 2.4D,
                UserId = 1,
                RestaurantId = 7
            };

            var review15 = new Review
            {
                Date = System.DateTime.Now.AddDays(-37),
                Description = "Last time",
                Stars = 1.1D,
                UserId = 2,
                RestaurantId = 8
            };

            var review16 = new Review
            {
                Date = System.DateTime.Now.AddDays(-15),
                Description = "Im not picky",
                Stars = 4.1D,
                UserId = 4,
                RestaurantId = 9
            };

            var review17 = new Review
            {
                Date = System.DateTime.Now.AddDays(-15),
                Description = "I am picky, not good!",
                Stars = 1.5D,
                UserId = 3,
                RestaurantId = 3
            };

            var review18 = new Review
            {
                Date = System.DateTime.Now.AddDays(-7),
                Description = "So not hungry",
                Stars = 2.8D,
                UserId = 3,
                RestaurantId = 7
            };

            var review19 = new Review
            {
                Date = System.DateTime.Now.AddDays(-3),
                Description = "Too tired to post",
                Stars = 2.0D,
                UserId = 1,
                RestaurantId = 10
            };

            var review20 = new Review
            {
                Date = System.DateTime.Now.AddDays(-1),
                Description = "Feed me this!!! Lets go!!!",
                Stars = 4.9D,
                UserId = 4,
                RestaurantId = 5
            };

            reviews.Add(review1);
            reviews.Add(review2);
            reviews.Add(review3);
            reviews.Add(review4);
            reviews.Add(review5);
            reviews.Add(review6);
            reviews.Add(review7);
            reviews.Add(review8);
            reviews.Add(review9);
            reviews.Add(review10);
            reviews.Add(review11);
            reviews.Add(review12);
            reviews.Add(review13);
            reviews.Add(review14);
            reviews.Add(review15);
            reviews.Add(review16);
            reviews.Add(review17);
            reviews.Add(review18);
            reviews.Add(review19);
            reviews.Add(review20);

            foreach (var review in reviews)
            {
                context.Reviews.Add(review);
            }

            context.SaveChanges();
        }
    }
}
