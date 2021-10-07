using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAll();
        Task<Menu> GetById(int id);
        Task Update(int id, MenuRequest dto);
        Task Create(MenuRequest dto);
        Task Delete(int id);
    }
}