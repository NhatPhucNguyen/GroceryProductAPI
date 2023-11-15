using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;

namespace GroceryProductAPI.Services
{
    public interface IProductRepository
    {
        Task<bool> ProductExistAsync(string upc);

        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByUPCAsync(string upc);

        Task AddProductAsync(Product product);

        Task<bool> DeleteProductAsync(string upc);

        Task<bool> SaveAsync();
    }
}
