using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransitExample.Consumer.Event;
using Microsoft.Extensions.Logging;

namespace MassTransitExample.Consumer.Consumer
{
    public class ProductCreatedEventConsumer: IConsumer<ProductCreatedEvent>
    {

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var product = context.Message;
            Console.WriteLine($"ProductId: {product.Id} ProductName: {product.Name}");
            await Task.CompletedTask;
        }
    }
}