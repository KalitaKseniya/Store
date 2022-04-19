using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.ModelBinders;
using System.Collections.Generic;

namespace Store.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class EntityController : Controller
    {

        private readonly ILoggerManager _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public EntityController(ILoggerManager logger, IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Get the list of all products 
        /// </summary>
        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts([FromQuery] ProductParams productParams,
                                                    [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> CategoryIds)
        {
            if (!productParams.IsPriceRangeValid())
            {
                _logger.Error($"Invalid price range minPrice={productParams.MinPrice} > maxPrice = {productParams.MaxPrice}");
                return BadRequest($"Invalid price range minPrice ={ productParams.MinPrice} > maxPrice = {productParams.MaxPrice}");
            }
            PagedList<Product> products = _productRepository.GetForCategories(productParams, CategoryIds);

            if (products == null)
            {
                _logger.Warn($"Categories with ids = {CategoryIds} contains no products in db.");
                return NotFound($"Categories with ids = {CategoryIds} contains no products in db.");
            }
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(products);
        }
    }
}
