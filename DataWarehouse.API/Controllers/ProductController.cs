using DataWarehouse.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Core.Shared;
using System;
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

        /// <summary>
        /// Get the list of all products from MongoDB
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
             var products = await _productsService.GetProductsAsync();
             return Ok(products);
        }

        /// <summary>
        /// Get the product by id from MongoDB
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productsService.GetProductAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Create a product in MongoDB
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var product = new Product
            {
                CategoryId = productDto.CategoryId,
                Price = productDto.Price,
                ProductId = productDto.EntityId,
                Description = productDto.Description,
                ImagePath = productDto.ImagePath,
                ProviderId = productDto.ProviderId,
                Name = productDto.Name,
                Operation = productDto.Operation,
                Type = productDto.Type
            };
            await _productsService.CreateAsync(product);
            return Ok();
        }

        /// <summary>
        /// Delete the product from MongoDB
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productsService.GetProductAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            await _productsService.RemoveAsync(id);
            return Ok();
        }

        /// <summary>
        /// Update the product in MongoDB
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, ProductDto productDto)
        {
            var product = await _productsService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.CategoryId = productDto.CategoryId;
            product.Price = productDto.Price;
            product.ProductId = productDto.EntityId;
            product.Description = productDto.Description;
            product.ImagePath = productDto.ImagePath;
            product.ProviderId = productDto.ProviderId;
            product.Name = productDto.Name;
            product.Operation = productDto.Operation;
            product.Type = productDto.Type;
            
            await _productsService.UpdateAsync(product);
            return Ok();
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> StoreImageForProduct(string id, string url)
        {
            var product = await _productsService.GetProductAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            var imageId = await _productsService.StoreImage(id, product.ImagePath, product.Name + DateTime.Now);
            return Ok(new {imageId = imageId });
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImageForProduct(string id)
        {
            var product = await _productsService.GetProductAsync(id);
            if(product == null || !product.HasImage())
            {
                return NotFound();
            }

            var img = await _productsService.GetImage(product.ImageId);
            
            return File(img, "image/jpg");
        } 

    }
}
