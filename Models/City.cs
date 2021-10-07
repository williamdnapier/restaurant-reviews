using Bnd.RestaurantReviews.PrivateModels;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

[assembly: InternalsVisibleTo("Data")]
namespace Bnd.RestaurantReviews.Models
{
    public class City : EntityBase
    {
        public string Name { get; set; }

        [JsonIgnore]
        internal ICollection<Restaurant> Restaurants { get; set; }
    }
}
