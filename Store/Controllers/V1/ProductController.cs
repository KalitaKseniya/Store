using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.Core.Shared;
using System.Threading.Tasks;

namespace Store.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories/{category_id}/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public ProductController(ILoggerManager logger, IProductRepository productRepository,
            ICategoryRepository categoryRepository, IProviderRepository providerRepository,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _providerRepository = providerRepository;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Get the list of all products for the specified category 
        /// </summary>
        [HttpGet]
        public IActionResult GetProductsForCategory(int category_id, [FromQuery] ProductParams productParams)
        {
            if (!productParams.IsPriceRangeValid())
            {
                _logger.Error($"Invalid price range minPrice={productParams.MinPrice} > maxPrice={productParams.MaxPrice}");
                return BadRequest($"Invalid price range minPrice ={ productParams.MinPrice} > maxPrice ={ productParams.MaxPrice}");
            }

            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }
            PagedList<Product> productsForCategory = _productRepository.GetForCategory(category_id, productParams);

            if (productsForCategory == null)
            {
                _logger.Warn($"Category with category_id = {category_id} contains no products in db.");
                return NotFound($"Category with category_id = {category_id} contains no products in db.");
            }
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(productsForCategory.MetaData));
            return Ok(productsForCategory);
        }

        /// <summary>
        /// Get the product by id for the specified category
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetProductForCategory(int category_id, int id)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }

            var productForCategory = _productRepository.GetForCategoryById(category_id, id);
            if (productForCategory == null)
            {
                _logger.Error($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
            }

            return Ok(productForCategory);
        }


        /// <summary>
        /// Create a product for the specified category
        /// </summary>
        [HttpPost, Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> CreateProductForCategory(int category_id, [FromBody] ProductForCreationDto productDto)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"No category with the given id = {category_id}");
                return NotFound($"No category with the given id = {category_id}");
            }
            if (productDto == null)
            {
                _logger.Error("Can't create product = null.");
                return BadRequest("Product can't be null");
            }
            var providerId = productDto.ProviderId;
            var provider = _providerRepository.GetById(providerId);
            if (provider == null)
            {
                _logger.Error($"No provider with the given id = {providerId}");
                return NotFound($"No provider with the given id = {providerId}");
            }
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = category_id,
                ImagePath = productDto.ImagePath,
                ProviderId = productDto.ProviderId
            };

            _productRepository.Create(product);
            _productRepository.Save();
            
            var productForRabbitMQ = new ProductDto(product, "POST");
            await _publishEndpoint.Publish(productForRabbitMQ);
            
            return Ok(product);
        }

        /// <summary>
        /// Delete the product with id = id for the specified category
        /// </summary>
        [HttpDelete("{id}"), Authorize(Roles = UserRoles.Administrator)]
        public IActionResult DeleteProductForCategory(int category_id, int id)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }
            var product = _productRepository.GetForCategoryById(category_id, id);
            if (product == null)
            {
                _logger.Error($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
            }

            _productRepository.Delete(product);
            _productRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// Update the product with id = id for the specified category
        /// </summary>
        [HttpPut("{id}"), Authorize(Roles = UserRoles.Administrator)]
        public IActionResult UpdateProductForCategory(int category_id, int id, [FromBody] ProductForUpdateDto productDto)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }
            var provider = _providerRepository.GetById(productDto.ProviderId);
            if (provider == null)
            {
                _logger.Error($"There is no provider with the given id = {productDto.ProviderId} in db.");
                return NotFound($"There is no category with the given id = {productDto.ProviderId} in db.");
            }

            var product = _productRepository.GetForCategoryById(category_id, id);
            if (product == null)
            {
                _logger.Error($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ImagePath = productDto.ImagePath;
            product.ProviderId = productDto.ProviderId;

            _productRepository.Update(product);
            _productRepository.Save();

            return NoContent();
        }

    
        
    }
}
