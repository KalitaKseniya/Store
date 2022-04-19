using DataWarehouse.API.Models;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Interfaces;
using Store.Core.Shared;
using System;
using System.Threading.Tasks;

namespace DataWarehouse.API.Controllers
{
    [ApiController]
    [Route("data-warehouse/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ProductService _productsService;

        public ProductController(ILoggerManager logger,
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
            try
            {
                var products = await _productsService.GetProductsAsync();
                return Ok(products);
            }
            catch(Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get the product by id from MongoDB
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            try
            {
                var product = await _productsService.GetProductAsync(id);
                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Create a product in MongoDB
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Delete the product from MongoDB
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var product = await _productsService.GetProductAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                await _productsService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Update the product in MongoDB
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, ProductDto productDto)
        {
            try
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
            catch(Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Save the image located on url to MongoDB for product with id = id
        /// </summary>
        [HttpPost("{id}/image")]
        public async Task<IActionResult> StoreImageForProduct(string id, string url)
        {
            try
            {
                var product = await _productsService.GetProductAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                var imageId = await _productsService.StoreImage(id, product.ImagePath, product.Name + DateTime.Now);
                return Ok(new { imageId = imageId });
            }
            catch (Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Get the image for product with id = id
        /// </summary>
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImageForProduct(string id)
        {
            try
            {
                var product = await _productsService.GetProductAsync(id);
                if (product == null || !product.HasImage())
                {
                    return NotFound();
                }

                var img = await _productsService.GetImage(product.ImageId);

                return File(img, "image/jpg");
            }
            catch (Exception ex)
            {
                _logger.Error($"Controller: {ControllerContext.ActionDescriptor.ControllerName}, action: {ControllerContext.ActionDescriptor.ActionName};{ex}");
                return StatusCode(500, ex);
            }
        } 

    }
}
