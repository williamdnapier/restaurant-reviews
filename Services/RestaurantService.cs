using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bnd.RestaurantReviews.Data;
using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using Bnd.RestaurantReviews.Services.Helpers;
using Bnd.RestaurantReviews.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ReviewsDataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public RestaurantService(ReviewsDataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            return await _serviceHelper.GetRestaurant(id);
        }

        public async Task Update(int id, RestaurantRequest dto)
        {
            var restaurant = await _serviceHelper.GetRestaurant(id);
            _ = _mapper.Map(dto, restaurant);
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task Create(RestaurantRequest dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var restaurant = await _serviceHelper.GetRestaurant(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
