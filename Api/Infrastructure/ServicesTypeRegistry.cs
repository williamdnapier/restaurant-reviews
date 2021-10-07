using Microsoft.Extensions.DependencyInjection;
using Bnd.RestaurantReviews.Services;
using Bnd.RestaurantReviews.Services.Helpers;
using Bnd.RestaurantReviews.Services.Interfaces;

namespace Bnd.RestaurantReviews.Api.Infrastructure
{
    public static class ServicesTypeRegistry
    {
        public static void UpdateServiceCollection(IServiceCollection services)
        {
            services.AddScoped<IServiceHelper, ServiceHelper>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICriticService, CriticService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IRestaurantTypeService, RestaurantTypeService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
