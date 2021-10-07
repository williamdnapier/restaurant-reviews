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
    public class ReviewService : IReviewService
    {
        private readonly ReviewsDataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceHelper _serviceHelper;

        public ReviewService(ReviewsDataContext context, IMapper mapper, IServiceHelper serviceHelper)
        {
            _context = context;
            _mapper = mapper;
            _serviceHelper = serviceHelper;
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetById(int id)
        {
            return await _serviceHelper.GetReview(id);
        }

        public async Task Update(int id, ReviewRequest dto)
        {
            var review = await _serviceHelper.GetReview(id);
            _ = _mapper.Map(dto, review);
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task Create(ReviewRequest dto)
        {
            var review = _mapper.Map<Review>(dto);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var review = await _serviceHelper.GetReview(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
