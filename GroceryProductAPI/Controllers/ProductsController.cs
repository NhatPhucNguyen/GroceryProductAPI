using AutoMapper;
using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;
using GroceryProductAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GroceryProductAPI.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route("/api/products")]
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProductsAsync();
            var results = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(results);
        }
        [Route("/api/products")]
        [HttpPost]
        public async Task<ActionResult> AddProducts([FromBody] ProductForCreationDTO newProduct)
        {
            if(await _repository.ProductExistAsync(newProduct.Upc))
            {
                return StatusCode(409, $"Product with UPC {newProduct.Upc} already existed");
            }
            var productToAdd = _mapper.Map<Product>(newProduct);
            foreach (var item in newProduct.Ingredients)
            {
                Ingredient ingredient = new Ingredient() { Name = item };
                productToAdd.Ingredients.Add(ingredient);
            }
            productToAdd.CategoryId = newProduct.CategoryId;
            await _repository.AddProductAsync(_mapper.Map<Product>(productToAdd));

            if(!await _repository.SaveAsync())
            {
                return BadRequest("Some thing went wrong with server");
            }
            return StatusCode(201,"Product is successfully created");
        }

        [Route("/api/products/{upc}")]
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProductByUPCAsync(string upc)
        {
            var product = await _repository.GetProductByUPCAsync(upc);

            if (product == null)
            {
                return NotFound($"Product with UPC {upc} not found.");
            }

            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }
        [Route("/api/products/{upc}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteProductAsync(string upc)
        {
            if(!await _repository.ProductExistAsync(upc))
            {
                return NotFound($"Product with UPC {upc} not found");
            }
            var isDeleted = await _repository.DeleteProductAsync(upc);
            if (!isDeleted)
            {
                return BadRequest("Something went wrong with server");
            }
            return Ok("Product is successfully deleted");
        }
    }
}
