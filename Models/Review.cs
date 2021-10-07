using Bnd.RestaurantReviews.PrivateModels;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

[assembly: InternalsVisibleTo("Data")]
namespace Bnd.RestaurantReviews.Models
{
    public class Review : EntityBase
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int RestaurantId { get; set; }

        public double Stars { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        internal Critic Critic { get; set; }

        [JsonIgnore]
        internal Restaurant Restaurant { get; set; }
    }
}
