using Microsoft.AspNetCore.Mvc;
using TimeWellSpent.Models;
using TimeWellSpent.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeWellSpent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]

        public IActionResult GetAllActivities()
        {
            return Ok(_categoryRepository.GetAllCategories());
        }

        [HttpGet("{id}")]

        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]

        public IActionResult InsertCategory(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            _categoryRepository.InsertCategory(category);
            return Created("Category created" + category.Id, category);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            _categoryRepository.UpdateCategory(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category is null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(category.Id);
            return NoContent();
        }
    }
}
