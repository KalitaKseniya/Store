using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;

namespace Store.Controllers
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
        public ProductController(ILoggerManager logger, IProductRepository productRepository,
            ICategoryRepository categoryRepository, IProviderRepository providerRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _providerRepository = providerRepository;
        }

        /// <summary>
        /// Get the list of all products for the specified category 
        /// </summary>
        [HttpGet]
        public IActionResult GetProductsForCategory(int category_id, [FromQuery] ProductParams productParams)
        {
            if (!productParams.PriceRangeValid())
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
            PagedList<Product> products = _productRepository.Get(category_id, productParams);
            
            if (products == null)
            {
                _logger.Warn($"Category with category_id = {category_id} contains no products in db.");
                return NotFound($"Category with category_id = {category_id} contains no products in db.");
            }
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(products);
        }

        /// <summary>
        /// Get the product by id for the specified category
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetProduct(int category_id, int id)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }

            var product = _productRepository.GetById(category_id, id);
            if (product == null)
            {
                _logger.Error($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
            }

            return Ok(product);
        }


        /// <summary>
        /// Create a product for the specified category
        /// </summary>
        [HttpPost, Authorize(Roles = UserRoles.Administrator)]
        public IActionResult CreateProduct(int category_id, [FromBody] ProductForCreationDto productDTO)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"No category with the given id = {category_id}");
                return NotFound($"No category with the given id = {category_id}");
            }
            if (productDTO == null)
            {
                _logger.Error("Can't create product = null.");
                return BadRequest("Product can't be null");
            }
            var providerId = productDTO.ProviderId;
            var provider = _providerRepository.GetById(providerId);
            if (provider == null)
            {
                _logger.Error($"No provider with the given id = {providerId}");
                return NotFound($"No provider with the given id = {providerId}");
            }
            Product product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                CategoryId = category_id,
                ImagePath = productDTO.ImagePath,
                ProviderId = productDTO.ProviderId
            };

            _productRepository.Create(product);
            _productRepository.Save();

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
            var product = _productRepository.GetById(category_id, id);
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
        public IActionResult UpdateProductForCategory(int category_id, int id, [FromBody] ProductForUpdateDto productForUpdateDto)
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with the given id = {category_id} in db.");
                return NotFound($"There is no category with the given id = {category_id} in db.");
            }
            var provider = _providerRepository.GetById(productForUpdateDto.ProviderId);
            if(provider == null)
            {
                _logger.Error($"There is no provider with the given id = {productForUpdateDto.ProviderId} in db.");
                return NotFound($"There is no category with the given id = {productForUpdateDto.ProviderId} in db.");
            }

            var product = _productRepository.GetById(category_id, id);
            if (product == null)
            {
                _logger.Error($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for category with the given category_id = {category_id} in db.");
            }

            product.Name = productForUpdateDto.Name;
            product.Description = productForUpdateDto.Description;
            product.Price = productForUpdateDto.Price;
            product.ImagePath = productForUpdateDto.ImagePath;
            product.ProviderId = productForUpdateDto.ProviderId;

            _productRepository.Update(product);
            _productRepository.Save();

            return NoContent();
        }

    }
}
