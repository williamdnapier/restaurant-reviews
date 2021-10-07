using Microsoft.AspNetCore.Mvc;
using Bnd.RestaurantReviews.Dto;
using Bnd.RestaurantReviews.Models;
using Bnd.RestaurantReviews.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bnd.RestaurantReviews.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantTypesController : ControllerBase
    {
        private readonly IRestaurantTypeService _restaurantTypeService;

        public RestaurantTypesController(IRestaurantTypeService restaurantTypeService)
        {
            _restaurantTypeService = restaurantTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantType>>> GetRestaurantTypes()
        {
            var restaurantTypes = await _restaurantTypeService.GetAll();
            return Ok(restaurantTypes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantType>> GetRestaurantType(int id)
        {
            var restaurantType = await _restaurantTypeService.GetById(id);
            return Ok(restaurantType);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRestaurantType(int id, RestaurantTypeRequest dto)
        {
            await _restaurantTypeService.Update(id, dto);
            return Ok(new { message = "Restaurant Type updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantType>> PostRestaurantType(RestaurantTypeRequest dto)
        {
            await _restaurantTypeService.Create(dto);
            return Ok(new { message = "Restaurant Type created successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurantType(int id)
        {
            await _restaurantTypeService.Delete(id);
            return Ok(new { message = "Restaurant Type deleted successfully" });
        }
    }
}