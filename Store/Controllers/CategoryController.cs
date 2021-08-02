using Microsoft.AspNetCore.Mvc;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Store.Controllers
{    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ILoggerManager logger;
        public CategoryController(ICategoryRepository _categoryRepository, ILoggerManager _logger)
        {
            categoryRepository = _categoryRepository;
            logger = _logger;
        }
        /// <summary>
        /// Get the list of all categories
        /// </summary>
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = categoryRepository.Get();
            if(categories == null)
            {
                logger.Warn("No categories in db.");
                return NotFound("No categories in db.");
            }
            return Ok(categories);
        }

        /// <summary>
        /// Get the category by id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                logger.Warn($"No category with given id = {id}.");
                return NotFound($"No category with given id = {id}.");
            }
            return Ok(category);
        }

        /// <summary>
        /// Create a category
        /// </summary>
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if(category == null)
            {
                logger.Error($"Can't add category=null.");
                return BadRequest("Can't add category=null");
            }
            categoryRepository.Create(category);
            categoryRepository.Save();

            return CreatedAtAction(nameof(CreateCategory), category);
        }
    }
}
