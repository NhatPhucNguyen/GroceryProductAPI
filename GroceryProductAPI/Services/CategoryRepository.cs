using GroceryProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryProductAPI.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GroceryProductContext _context;

        public CategoryRepository(GroceryProductContext context)
        {
            _context = context;
        }

        public Task AddCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == id);
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
