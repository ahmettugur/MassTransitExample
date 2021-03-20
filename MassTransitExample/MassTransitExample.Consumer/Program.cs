using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransit.Serialization;
using MassTransit.Serialization.JsonConverters;
using MassTransitExample.Consumer.Consumer;
// using MassTransitExample.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MassTransitExample.Consumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string rabbitMqUri = "rabbitmq://localhost";
            const string queue = "product-created";
            const string userName = "guest";
            const string password = "guest";
            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(userName);
                    configurator.Password(password);
                });
                factory.AddMessageDeserializer(new ContentType("application/vnd.masstransit+json"), () => new CustomJsonMessageDeserializer(JsonMessageSerializer.Deserializer));
                factory.ReceiveEndpoint(queue, endpoint=>
                {
                    endpoint.Consumer<ProductCreatedEventConsumer>();
                });
            }); 
            bus.Start();
            Console.ReadLine(); 
            bus.Stop();
        }
    }
}