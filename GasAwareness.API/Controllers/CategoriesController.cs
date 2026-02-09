using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasAwareness.API.Models.Category.Requests;
using GasAwareness.API.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasAwareness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Policy = "RequireAllRoles")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            return Ok(await _categoryService.GetCategoriesAsync());
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryRequestDto request)
        {
            var response = await _categoryService.CreateCategoryAsync(request);

            if (!response) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdminAndEditor")]
        public async Task<IActionResult> DeleteCategoryAsync([FromQuery] Guid id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);

            if (!response) return BadRequest();

            return Ok();
        }
    }
}