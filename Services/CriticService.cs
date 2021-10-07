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
    public class CriticService : ICriticService
    {
        private readonly ReviewsDataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public CriticService(ReviewsDataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<Critic>> GetAll()
        {
            return await _context.Critics.ToListAsync();
        }

        public async Task<Critic> GetById(int id)
        {
            return await _serviceHelper.GetCritic(id);
        }

        public async Task Update(int id, CriticRequest dto)
        {
            var critic = await _serviceHelper.GetCritic(id);
            _ = _mapper.Map(dto, critic);
            _context.Critics.Update(critic);
            await _context.SaveChangesAsync();
        }

        public async Task Create(CriticRequest dto)
        {
            var critic = _mapper.Map<Critic>(dto);
            _context.Critics.Add(critic);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var critic = await _serviceHelper.GetCritic(id);
            _context.Critics.Remove(critic);
            await _context.SaveChangesAsync();
        }
    }
}
