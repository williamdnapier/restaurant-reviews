using Bnd.RestaurantReviews.PrivateModels;

namespace Bnd.RestaurantReviews.Dto
{
    public class MenuRequest : EntityBase
    {
        public string Name { get; set; }
        public string Items { get; set; }
    }
}
