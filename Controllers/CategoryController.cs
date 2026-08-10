using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStore.API.Models.Domain;
using SmartStore.API.Models.DTO;
using SmartStore.API.Repository.Interfaces;

namespace SmartStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                CategoryName = createCategoryDto.CategoryName,
                Description = createCategoryDto.Description
            };

            category = await categoryRepository.CreateAsync(category);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = category.Id },
                category);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = new Category
            {
                Id = id,
                CategoryName = updateCategoryDto.CategoryName,
                Description = updateCategoryDto.Description
            };

            var updatedCategory = await categoryRepository.UpdateAsync(category);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCategory = await categoryRepository.DeleteAsync(id);

            if (deletedCategory == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
