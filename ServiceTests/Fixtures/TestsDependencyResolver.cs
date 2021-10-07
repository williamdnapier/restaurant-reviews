using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bnd.RestaurantReviews.Api.Infrastructure;
using Bnd.RestaurantReviews.Data;
using System;

namespace Bnd.RestaurantReviews.ServiceTests.Fixtures
{
    public static class TestsDependencyResolver
    {
        private static IServiceCollection GetServiceCollection()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ReviewsDataContext>(options =>
                options.UseInMemoryDatabase("ReviewsInMemoryDb"));

            ServicesTypeRegistry.UpdateServiceCollection(services);

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper());

            return services;
        }

        public static IServiceProvider GetServiceProvider()
        {
            return GetServiceCollection().BuildServiceProvider();
        }
    }
}
