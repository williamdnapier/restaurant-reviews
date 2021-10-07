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
    public class CriticsController : ControllerBase
    {
        private readonly ICriticService _criticService;

        public CriticsController(ICriticService criticService)
        {
            _criticService = criticService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Critic>>> GetCritics()
        {
            var critics = await _criticService.GetAll();
            return Ok(critics);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Critic>> GetCritic(int id)
        {
            var critic = await _criticService.GetById(id);
            return Ok(critic);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCritic(int id, CriticRequest dto)
        {
            await _criticService.Update(id, dto);
            return Ok(new { message = "Critic updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<Critic>> PostCritic(CriticRequest dto)
        {
            await _criticService.Create(dto);
            return Ok(new { message = "Critic created successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCritic(int id)
        {
            await _criticService.Delete(id);
            return Ok(new { message = "Critic deleted successfully" });
        }
    }
}