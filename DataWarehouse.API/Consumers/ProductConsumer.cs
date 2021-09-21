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
        public ProductConsumer(ILoggerManager logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ProductDto> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
            _logger.Debug(JsonConvert.SerializeObject(context.Message));
        }
    }
}
