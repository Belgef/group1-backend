using AutoMapper;
using MarketplaceBackend.Contracts.V1;
using MarketplaceBackend.Contracts.V1.Requests.Products;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers.V1
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        /// <response code="200">Returns list of categories</response>
        [HttpGet(ApiRoutes.Categories.GetAll)]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var response = await _categoryService.GetCategoriesAsync();
            return Ok(response);
        }
    }
}
