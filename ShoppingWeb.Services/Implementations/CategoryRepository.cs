using ShoppingWeb.Shared.Interfaces;
using ShoppingWeb.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingWeb.Services.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShoppingDbContext _context;

        public CategoryRepository(ShoppingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
