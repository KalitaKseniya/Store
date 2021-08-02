using Microsoft.AspNetCore.Mvc;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{  
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories/{category_id}/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public ProductController(ILoggerManager _logger, IProductRepository _productRepository, 
            ICategoryRepository _categoryRepository)
        {
            logger = _logger;
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
        }

        /// <summary>
        /// Get the list of all products for the specified category
        /// </summary>
        [HttpGet]
        public IActionResult GetProductsForCategory(int category_id)
        {
            var category = categoryRepository.GetById(category_id);
            if (category == null)
            {
                logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }
            var products = productRepository.Get(category_id);
            if(products == null)
            {
                logger.Warn($"Category with category_id = {category_id} contains no products in db.");
                return NotFound($"Category with category_id = {category_id} contains no products in db.");
            }
            return Ok(products);
        }
        
        /// <summary>
        /// Get the product by id for the specified category
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetProduct(int category_id, int id)
        {
            var category = categoryRepository.GetById(category_id);
            if(category == null)
            {
                logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }

            var product = productRepository.GetById(category_id, id);
            if(product == null)
            {
                logger.Error($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
            }

            return Ok(product);
        }


        /// <summary>
        /// Create a product for the specified category
        /// </summary>
        [HttpPost]
        public IActionResult CreateProduct(int category_id, ProductForCreationDTO productDTO)
        {
            var category = categoryRepository.GetById(category_id);
            if(category == null)
            {
                logger.Error($"No category with the given id = {category_id}");
                return NotFound($"No category with the given id = {category_id}");
            }
            if(productDTO == null)
            {
                logger.Error("Can't create product = null.");
                return BadRequest("Product can't be null");
            }
            Product product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                CategoryId = category_id
            };

            productRepository.Create(product);
            productRepository.Save();

            return Ok("Product created");
        }
       
    }
}
