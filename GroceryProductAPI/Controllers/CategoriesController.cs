using AutoMapper;
using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;
using GroceryProductAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GroceryProductAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(string name)
        {
            try
            {
                var categories = await _repository.GetCategoriesAsync();
                var results = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                if (name.Length > 0)
                {
                    return Ok(results.Where(cat => cat.Name.Contains(name)));
                }
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
        {
            try
            {
                if(!await _repository.CategoryExistAsync(id))
                {
                    return NotFound($"Category with Id {id} not found");
                }
                var categoryFound = await _repository.GetCategoryByIdAsync(id);
                return Ok(_mapper.Map<CategoryDTO>(categoryFound));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryForCreationDTO category)
        {
            try
            {
                if(await _repository.CategoryExistAsync(category.Name))
                {
                    return Conflict($"Category name {category.Name} already existed");
                }
                var categoryToAdd = _mapper.Map<Category>(category);
                await _repository.AddCategoryAsync(categoryToAdd);
                await _repository.SaveAsync();
                return Ok($"Category {category.Name} is added successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateCategory(int id,[FromBody] CategoryForUpdateDTO category)
        {
            try
            {                
                if(!await _repository.CategoryExistAsync(id))
                {
                    return NotFound($"Category with id {id} not found");
                }
                var categoryToUpdate = _mapper.Map<Category>(category);
                categoryToUpdate.CategoryId = id;
                await _repository.UpdateCategoryAsync(categoryToUpdate);
                await _repository.SaveAsync();
                return Ok("Category updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [Route("{id}")]
        [HttpPatch]
        public async Task<ActionResult> PatchCategory(int id, [FromBody] JsonPatchDocument<CategoryForUpdateDTO> patchCategory)
        {
            try
            {
                if (!await _repository.CategoryExistAsync(id))
                {
                    return NotFound($"Category with id {id} not found");
                }
                var categoryFound = await _repository.GetCategoryByIdAsync(id);
                var categoryToPatch = _mapper.Map<CategoryForUpdateDTO>(categoryFound);
                patchCategory.ApplyTo(categoryToPatch,ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!TryValidateModel(categoryToPatch))
                {
                    return BadRequest(ModelState);
                }
                _mapper.Map(categoryToPatch, categoryFound);
                await _repository.SaveAsync();
                return Ok("Category updated successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                if (!await _repository.CategoryExistAsync(id))
                {
                    return NotFound($"Category with id {id} not found");
                }                
                await _repository.DeleteCategoryAsync(id);
                await _repository.SaveAsync();
                return Ok("Category deleted successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
