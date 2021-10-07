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
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _cityService.GetAll();
            return Ok(cities);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _cityService.GetById(id);
            return Ok(city);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCity(int id, CityRequest dto)
        {
            await _cityService.Update(id, dto);
            return Ok(new { message = "City updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityRequest dto)
        {
            await _cityService.Create(dto);
            return Ok(new { message = "City created successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _cityService.Delete(id);
            return Ok(new { message = "City deleted successfully" });
        }
    }
}