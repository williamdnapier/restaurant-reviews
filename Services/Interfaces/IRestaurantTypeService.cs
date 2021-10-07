using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Services.Interfaces
{
    public interface IRestaurantTypeService
    {
        Task<IEnumerable<RestaurantType>> GetAll();
        Task<RestaurantType> GetById(int id);
        Task Update(int id, RestaurantTypeRequest dto);
        Task Create(RestaurantTypeRequest dto);
        Task Delete(int id);
    }
}