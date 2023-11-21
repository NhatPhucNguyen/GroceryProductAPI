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

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<bool> CategoryExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == id);
        }

        public async Task<bool> CategoryExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var categoryToDelete = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (categoryToDelete != null)
            {
                categoryToDelete.Products.Clear();
                _context.Categories.Remove(categoryToDelete);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id) ?? throw new Exception("Product not found");
            return category;
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            var categoryFound = await _context.Categories.FirstOrDefaultAsync(category => category.Name.Contains(name));
            return categoryFound;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var categoryToUpdate = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);            
            if(categoryToUpdate != null)
            {
                category.CreatedAt = categoryToUpdate.CreatedAt;
                _context.Categories.Entry(categoryToUpdate).CurrentValues.SetValues(category);
                categoryToUpdate.UpdatedAt = DateTime.Now;
            }
        }
    }
}
