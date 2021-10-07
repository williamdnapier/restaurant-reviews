using Bnd.RestaurantReviews.PrivateModels;

namespace Bnd.RestaurantReviews.Dto
{
    public class CriticRequest : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
