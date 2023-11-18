using GroceryProductAPI.Models;

namespace GroceryProductAPI.Services
{
    public interface ICategoryRepository
    {
        Task<bool> CategoryExistAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
