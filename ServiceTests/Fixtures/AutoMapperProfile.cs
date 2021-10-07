using AutoMapper;
using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;

namespace Bnd.RestaurantReviews.ServiceTests.Fixtures
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityRequest, City>();
            CreateMap<City, CityRequest>();

            CreateMap<CriticRequest, Critic>();
            CreateMap<Critic, CriticRequest>();

            CreateMap<MenuRequest, Menu>();
            CreateMap<Menu, MenuRequest>();

            CreateMap<RestaurantRequest, Restaurant>();
            CreateMap<Restaurant, RestaurantRequest>();

            CreateMap<RestaurantTypeRequest, RestaurantType>();
            CreateMap<RestaurantType, RestaurantTypeRequest>();

            CreateMap<ReviewRequest, Review>();
            CreateMap<Review, ReviewRequest>();
        }
    }
}