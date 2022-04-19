using DataWarehouse.API.Models;
using MassTransit;
using Newtonsoft.Json;
using Store.Core.Interfaces;
using Store.Core.Shared;
using System;
using System.Threading.Tasks;

namespace DataWarehouse.API.Consumers
{
    public class ProductConsumer : IConsumer<ProductDto>
    {
        private readonly ILoggerManager _logger;
        private readonly ProductService _productsService;
        public ProductConsumer(ILoggerManager logger, ProductService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }
        public async Task Consume(ConsumeContext<ProductDto> context)
        {
            var productDto = context.Message;
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
            await Console.Out.WriteLineAsync("Get:" + JsonConvert.SerializeObject(product));
            _logger.Debug("Get:" + JsonConvert.SerializeObject(product));
            
            switch (productDto.Operation)
            {
                case "POST":
                case "PUT":
                case "DELETE":
                    try
                    {
                        string id = await _productsService.CreateAsync(product);
                        await _productsService.StoreImage(id, product.ImagePath, product.Name + DateTime.Now);
                    }
                    catch(Exception ex)
                    {
                        _logger.Error($"Exception: {ex}");
                    }
                    break;
                default:
                    _logger.Warn($"Operation {productDto.Operation} not allowed");
                    break;
            }
        }
    }
}
