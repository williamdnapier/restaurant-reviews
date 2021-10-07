using Bnd.RestaurantReviews.PrivateModels;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

[assembly: InternalsVisibleTo("Data")]
namespace Bnd.RestaurantReviews.Models
{
    public class Critic : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        internal ICollection<Review> Reviews { get; set; }
    }
}
