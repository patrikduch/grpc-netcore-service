using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcNetCoreServiceApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest() 
            { 
                Name = "Patrik Duch" 
            });
            Console.WriteLine(response.Message);
            Console.ReadKey();
        }
    }
}
