using MassTransit;
using Newtonsoft.Json;
using Store.Core.Shared;
using System;
using System.Threading.Tasks;

namespace DataWarehouse.API.Consumers
{
    public class ProductConsumer : IConsumer<ProductDto>
    {
        public async Task Consume(ConsumeContext<ProductDto> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(context.Message));
        }
    }
}
