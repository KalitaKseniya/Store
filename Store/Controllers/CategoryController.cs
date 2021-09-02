using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;

namespace Store.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILoggerManager _logger;
        public CategoryController(ICategoryRepository categoryRepository, ILoggerManager logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        /// <summary>
        /// Get the list of all categories
        /// </summary>
        [HttpGet]
        public IActionResult GetCategories([FromQuery] CategoryParams categoryParams)
        {
            var categories = _categoryRepository.Get(categoryParams);
            if (categories == null)
            {
                _logger.Warn("No categories in db.");
                return NotFound("No categories in db.");
            }
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(categories.MetaData));
            return Ok(categories);
        }

        /// <summary>
        /// Get the category by id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                _logger.Warn($"No category with given id = {id}.");
                return NotFound($"No category with given id = {id}.");
            }
            return Ok(category);
        }

        /// <summary>
        /// Create a category
        /// </summary>
        [HttpPost, Authorize(Roles = UserRoles.Administrator)]
        public IActionResult CreateCategory([FromBody] CategoryForCreationDto categoryForCreationDTO)
        {
            if (categoryForCreationDTO == null)
            {
                _logger.Error($"Can't add category=null.");
                return BadRequest("Can't add category=null");
            }
            var category = new Category
            {
                Name = categoryForCreationDTO.Name
            };
            _categoryRepository.Create(category);
            _categoryRepository.Save();

            return CreatedAtAction(nameof(CreateCategory), category);
        }

        /// <summary>
        /// Delete the category with id = id
        /// </summary>
        [HttpDelete("{id}"), Authorize(Roles = UserRoles.Administrator)]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                _logger.Warn($"No category with given id = {id}.");
                return NotFound($"No category with given id = {id}.");
            }
            _categoryRepository.Delete(category);
            _categoryRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Edit the Name of the category with id = id
        /// </summary>
        [HttpPut("{id}"), Authorize(Roles = UserRoles.Administrator)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryForUpdateDto categoryForUpdateDto)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                _logger.Warn($"No category with given id = {id}.");
                return NotFound($"No category with given id = {id}.");
            }

            category.Name = categoryForUpdateDto.Name;
            _categoryRepository.Update(category);
            _categoryRepository.Save();

            return NoContent();
        }
    }
}
