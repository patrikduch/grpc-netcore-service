using RabbitMQ.Client;
using System.Text;

namespace GrpcNetCoreService.RabbitMq.Producer
{
    public class RabbitMqProducer : IRabbitMqProducer
    {

        public void Publish(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq"
            };

            using (var conn = factory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "BasicTest", null, body);
            }
        }
    }
}
