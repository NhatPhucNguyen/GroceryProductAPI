using GroceryProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryProductAPI.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly GroceryProductContext _context;
        public ProductRepository(GroceryProductContext context)
        {
            _context = context;
        }
        public Task<Product> GetProductByUPCAsync(string upc)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var results = await _context.Products.Include(p => p.Ingredients).ToListAsync();
            return results.OrderBy(p => p.Name);
        }

        public Task<bool> ProductExistAsync(string upc)
        {
            throw new NotImplementedException();
        }
    }
}
