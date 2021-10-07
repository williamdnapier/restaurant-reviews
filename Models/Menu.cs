using Bnd.RestaurantReviews.PrivateModels;

namespace Bnd.RestaurantReviews.Models
{
    public class Menu : EntityBase
    {
        public string Name { get; set; }
        public string Items { get; set; }
    }
}
