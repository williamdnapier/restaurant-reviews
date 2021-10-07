using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAll();
        Task<Review> GetById(int id);
        Task Update(int id, ReviewRequest dto);
        Task Create(ReviewRequest dto);
        Task Delete(int id);
    }
}