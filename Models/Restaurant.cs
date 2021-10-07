using Bnd.RestaurantReviews.PrivateModels;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

[assembly: InternalsVisibleTo("Data")]
namespace Bnd.RestaurantReviews.Models
{
    public class Restaurant : EntityBase
    {
        public int CityId { get; set; }

        public int MenuId { get; set; }

        public string Name { get; set; }

        public int RestaurantTypeId { get; set; }

        [JsonIgnore]
        internal City City { get; set; }

        [JsonIgnore]
        internal Menu Menu { get; set; }

        [JsonIgnore]
        internal RestaurantType RestaurantType { get; set; }

        [JsonIgnore]
        internal ICollection<Review> Reviews { get; set; }
    }
}