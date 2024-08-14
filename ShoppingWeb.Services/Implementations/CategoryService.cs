using AutoMapper;
using ShoppingWeb.Shared.Interfaces;
using ShoppingWeb.Shared.DTOs;
using ShoppingWeb.Shared.Models;
using System.Collections.Generic;

namespace ShoppingWeb.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public void AddCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _categoryRepository.Add(category);
        }

        public void UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _mapper.Map(categoryDto, category);
                _categoryRepository.Update(category);
            }
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
        }
    }
}
