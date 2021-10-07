using Bnd.RestaurantReviews.Data;
using Bnd.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Services.Helpers
{
    public class ServiceHelper : IServiceHelper
    {
        private readonly ReviewsDataContext _context;

        public ServiceHelper(ReviewsDataContext context)
        {
            _context = context;
        }

        public async Task<Restaurant> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found");
            return restaurant;
        }

        public async Task<RestaurantType> GetRestaurantType(int id)
        {
            var restaurantType = await _context.RestaurantTypes.FindAsync(id);
            if (restaurantType == null) throw new KeyNotFoundException("RestaurantType not found");
            return restaurantType;
        }

        public async Task<City> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) throw new KeyNotFoundException("City not found");
            return city;
        }

        public async Task<Menu> GetMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null) throw new KeyNotFoundException("Menu not found");
            return menu;
        }

        public async Task<Critic> GetCritic(int id)
        {
            var critic = await _context.Critics.FindAsync(id);
            if (critic == null) throw new KeyNotFoundException("Critic not found");
            return critic;
        }

        public async Task<Review> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) throw new KeyNotFoundException("Review not found");
            return review;
        }
    }
}