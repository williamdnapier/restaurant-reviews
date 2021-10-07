using AutoMapper;
using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;

namespace Bnd.RestaurantReviews.Services.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityRequest, City>();
            CreateMap<CriticRequest, Critic>();
            CreateMap<MenuRequest, Menu>();
            CreateMap<RestaurantRequest, Restaurant>();
            CreateMap<RestaurantTypeRequest, RestaurantType>();
            CreateMap<ReviewRequest, Review>();
        }
    }
}