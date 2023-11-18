using AutoMapper;
using Azure;
using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;
using GroceryProductAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GroceryProductAPI.Controllers
{
    [Route("/api/products")]
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

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _repository.GetProductsAsync();
                var results = _mapper.Map<IEnumerable<ProductDTO>>(products);
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong in the server");
            }
            
        }

        [Route("{upc}")]
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProductByUPCAsync(string upc)
        {
            try
            {
                var product = await _repository.GetProductByUPCAsync(upc);

                if (product == null)
                {
                    return NotFound($"Product with UPC {upc} not found.");
                }

                var result = _mapper.Map<ProductDTO>(product);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong in the server");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> AddProducts([FromBody] ProductForCreationDTO newProduct)
        {
            try
            {
                if (await _repository.ProductExistAsync(newProduct.Upc))
                {
                    return StatusCode(409, $"Product with UPC {newProduct.Upc} already existed");
                }
                var productToAdd = _mapper.Map<Product>(newProduct);
                foreach (var item in newProduct.IngredientsList)
                {
                    Ingredient ingredient = new Ingredient() { Name = item };
                    productToAdd.Ingredients.Add(ingredient);
                }
                productToAdd.CategoryId = newProduct.CategoryId;
                await _repository.AddProductAsync(_mapper.Map<Product>(productToAdd));
                await _repository.SaveAsync();
                return StatusCode(201, "Product is sucessfully created");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong in the server");
            }           
            
        }
        [Route("{upc}")]
        [HttpPut]
        public async Task<ActionResult> UpdateProduct(string upc, [FromBody] ProductForUpdateDTO updatedProduct)
        {
            try
            {
                if (!await _repository.ProductExistAsync(upc))
                {
                    return NotFound($"Product with UPC {upc} not found.");
                }
                var productToUpdate = _mapper.Map<Product>(updatedProduct);
                productToUpdate.Upc = upc;
                foreach (var item in updatedProduct.IngredientsList)
                {
                    Ingredient ingredient = new Ingredient() { Name = item };
                    productToUpdate.Ingredients.Add(ingredient);
                }
                await _repository.UpdateProductAsync(productToUpdate);
                await _repository.SaveAsync();
                return Ok("Product is updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
            
        }
        [Route("{upc}")]
        [HttpPatch]
        public async Task<ActionResult> UpdatePrice(string upc,[FromBody] JsonPatchDocument<ProductForUpdateDTO> patchProduct)
        {
            try
            {
                if (!await _repository.ProductExistAsync(upc))
                {
                    return NotFound($"Product with UPC {upc} not found.");
                }
                var productFound = await _repository.GetProductByUPCAsync(upc);
                var productToPatch = _mapper.Map<ProductForUpdateDTO>(productFound);
                patchProduct.ApplyTo(productToPatch, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!TryValidateModel(productToPatch))
                {
                    return BadRequest(ModelState);
                }
                _mapper.Map(productToPatch, productFound);
                await _repository.SaveAsync();
                return Ok("Product is updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong in the server");
            }
            
        }
        [Route("{upc}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteProductAsync(string upc)
        {
            try
            {
                if (!await _repository.ProductExistAsync(upc))
                {
                    return NotFound($"Product with UPC {upc} not found");
                }
                await _repository.DeleteProductAsync(upc);
                return Ok("Product is successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong in the server");
            }
            
        }
    }
}
