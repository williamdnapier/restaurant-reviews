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
    public class CityService : ICityService
    {
        private readonly ReviewsDataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public CityService(ReviewsDataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetById(int id)
        {
            return await _serviceHelper.GetCity(id);
        }

        public async Task Update(int id, CityRequest dto)
        {
            var city = await _serviceHelper.GetCity(id);
            _ = _mapper.Map(dto, city);
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task Create(CityRequest dto)
        {
            var city = _mapper.Map<City>(dto);
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var city = await _serviceHelper.GetCity(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }
    }
}
