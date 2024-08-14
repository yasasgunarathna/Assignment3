using ShoppingWeb.Shared.DTOs;
using System.Collections.Generic;

namespace ShoppingWeb.Shared.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
        void AddCategory(CategoryDto categoryDto);
        void UpdateCategory(int id, CategoryDto categoryDto);
        void DeleteCategory(int id);
    }
}
