using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> GetProductByUPCAsync(string upc)
        {
            var product = await _context.Products.Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefaultAsync(p => p.Upc == upc) ?? throw new Exception("The product does not exists");
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var results = await _context.Products.Include(p => p.Category).Include(p => p.Ingredients).ToListAsync();
            return results.OrderBy(p => p.Name);
        }

        public async Task<bool> ProductExistAsync(string upc)
        {
            return await _context.Products.AnyAsync(p => p.Upc == upc);
        }
        public async Task<bool> DeleteProductAsync(string upc)
        {
            var productToDelete = await _context.Products.Include(p=>p.Ingredients).SingleAsync(p => p.Upc == upc);
            var temp = new List<Ingredient>();
            temp.AddRange(productToDelete.Ingredients);
            if (productToDelete != null)
            {
                productToDelete.Ingredients.Clear();
                _context.Products.Remove(productToDelete);
                _context.Ingredients.RemoveRange(temp);
            }
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
