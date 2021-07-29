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
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = categoryRepository.Get();
            if(categories == null)
            {
                return Ok("No categories in db.");
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            if(category == null)
            {
                return NotFound($"No category with given id = {id}.");
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if(category == null)
            {
                return BadRequest("Can't add category=null");
            }
            categoryRepository.Create(category);
            categoryRepository.Save();

            return CreatedAtAction(nameof(CreateCategory), category);
        }
    }
}
