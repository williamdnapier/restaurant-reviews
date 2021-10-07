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
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _menuService.GetAll();
            return Ok(menus);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _menuService.GetById(id);
            return Ok(menu);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMenu(int id, MenuRequest dto)
        {
            await _menuService.Update(id, dto);
            return Ok(new { message = "Menu updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(MenuRequest dto)
        {
            await _menuService.Create(dto);
            return Ok(new { message = "Menu created successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _menuService.Delete(id);
            return Ok(new { message = "Menu deleted successfully" });
        }
    }
}