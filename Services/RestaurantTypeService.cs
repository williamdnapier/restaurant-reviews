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
    public class RestaurantTypeService : IRestaurantTypeService
    {
        private readonly ReviewsDataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public RestaurantTypeService(ReviewsDataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<RestaurantType>> GetAll()
        {
            return await _context.RestaurantTypes.ToListAsync();
        }

        public async Task<RestaurantType> GetById(int id)
        {
            return await _serviceHelper.GetRestaurantType(id);
        }

        public async Task Update(int id, RestaurantTypeRequest dto)
        {
            var restaurantType = await _serviceHelper.GetRestaurantType(id);
            _ = _mapper.Map(dto, restaurantType);
            _context.RestaurantTypes.Update(restaurantType);
            await _context.SaveChangesAsync();
        }

        public async Task Create(RestaurantTypeRequest dto)
        {
            var restaurantType = _mapper.Map<RestaurantType>(dto);
            _context.RestaurantTypes.Add(restaurantType);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var restaurantType = await _serviceHelper.GetRestaurantType(id);
            _context.RestaurantTypes.Remove(restaurantType);
            await _context.SaveChangesAsync();
        }
    }
}
