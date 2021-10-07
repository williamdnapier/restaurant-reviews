using Bnd.RestaurantReviews.Models;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Services.Helpers
{
    public interface IServiceHelper
    {
        Task<Restaurant> GetRestaurant(int id);
        Task<RestaurantType> GetRestaurantType(int id);
        Task<City> GetCity(int id);
        Task<Menu> GetMenu(int id);
        Task<Critic> GetCritic(int id);
        Task<Review> GetReview(int id);
    }
}