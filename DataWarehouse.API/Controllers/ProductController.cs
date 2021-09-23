using DataWarehouse.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataWarehouse.API.Controllers
{
    [ApiController]
    [Route("data-warehouse/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productsService;

        public ProductController(ILogger<ProductController> logger,
                                ProductService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsChanges()
        {
            var products = await _productsService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productsService.GetProductAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productsService.CreateAsync(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productsService.RemoveAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _productsService.UpdateAsync(product);
            return Ok();
        }



    }
}
