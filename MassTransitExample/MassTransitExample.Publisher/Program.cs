using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransitExample.Publisher.Event;

namespace MassTransitExample.Publisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string rabbitMqUri = "rabbitmq://localhost";
            const string entityName = "product-created";
            const string userName = "guest";
            const string password = "guest";
 
            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(userName);
                    configurator.Password(password);
                });
                factory.Message<ProductCreatedEvent>(x => x.SetEntityName(entityName));
            });
 
            await Task.Run(async () =>
            {
                int i = 1;
                while (true)
                {
                    Console.Write("Press enter button for add product to queue");
                    Console.ReadLine();
                    var message = new ProductCreatedEvent
                    {
                        Id = 1,
                        Name = $"Product-{i}"
                    };
                    i++;
                    await bus.Publish<ProductCreatedEvent>(message);
                    //Console.WriteLine("");
                }
            });
        }
 
    }
}