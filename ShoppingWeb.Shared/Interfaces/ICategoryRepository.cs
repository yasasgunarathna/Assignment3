using ShoppingWeb.Shared.Models;
using System.Collections.Generic;

namespace ShoppingWeb.Shared.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
