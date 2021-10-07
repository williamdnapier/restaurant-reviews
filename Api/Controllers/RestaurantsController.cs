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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetAll();
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetById(id);
            return Ok(restaurant);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantRequest dto)
        {
            await _restaurantService.Update(id, dto);
            return Ok(new { message = "Restaurant updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantRequest dto)
        {
            await _restaurantService.Create(dto);
            return Ok(new { message = "Restaurant created successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await _restaurantService.Delete(id);
            return Ok(new { message = "Restaurant deleted successfully" });
        }
    }
}