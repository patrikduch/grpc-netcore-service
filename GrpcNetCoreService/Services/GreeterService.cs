using Grpc.Core;
using System.Threading.Tasks;

namespace GrpcNetCoreService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return new HelloReply()
            {
                Message = "Bc. " + request.Name
            };
        }

    }
}
