using GroceryProductAPI.Models;

namespace GroceryProductAPI.Services
{
    public interface IProductRepository
    {
        Task<bool> ProductExistAsync(string upc);

        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByUPCAsync(string upc);
    }
}
