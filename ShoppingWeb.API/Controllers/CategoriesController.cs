using Microsoft.AspNetCore.Mvc;
using ShoppingWeb.Shared.DTOs;
using ShoppingWeb.Shared.Interfaces;
using ShoppingWeb.Shared.Models;

namespace ShoppingWeb.API.Controllers
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
        public ActionResult<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            if (categories == null || !categories.Any())
            {
                return NotFound(new { Message = "No categories available." });
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound(new { Message = "Can not fined category with this id" });
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryDto categoryDto)
        {
            _categoryService.AddCategory(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, CategoryDto categoryDto)
        {
            var existingCategory = _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound(new { Message = "Can not modify category" });
            }

            _categoryService.UpdateCategory(id, categoryDto);
            return Ok(new { Message = "Update category successful!" });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var existingCategory = _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound(new { Message = "Can not delete category" });
            }

            _categoryService.DeleteCategory(id);
            return Ok(new { Message = "Delete category successful!" });
        }
    }
}
