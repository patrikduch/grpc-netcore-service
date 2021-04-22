using GrpcNetCoreService.RabbitMq.Model;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GrpcNetCoreService.RabbitMq.Consumer
{
    public class OrderConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
        }
    }
}
