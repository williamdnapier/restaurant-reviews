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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _reviewService.GetAll();
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewService.GetById(id);
            return Ok(review);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutReview(int id, ReviewRequest dto)
        {
            await _reviewService.Update(id, dto);
            return Ok(new { message = "Review updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewRequest dto)
        {
            await _reviewService.Create(dto);
            return Ok(new { message = "Review created successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewService.Delete(id);
            return Ok(new { message = "Review deleted successfully" });
        }
    }
}