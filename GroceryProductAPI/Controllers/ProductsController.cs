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
    }
}
