using GroceryProductAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GroceryProductAPI.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly GroceryProductContext _context;
        public ProductsController(GroceryProductContext context)
        {
            _context = context;
        }

        [Route("/api/products")]
        [HttpGet]        
        public async Task<ActionResult<ICollection<Product>>> GetProducts()
        {
            if(_context.Products == null)
            {
                return NotFound();
            }
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}
