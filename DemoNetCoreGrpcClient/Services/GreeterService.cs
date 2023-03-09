using DemoNetCoreGrpcService;
using Grpc.Net.Client;

namespace DemoNetCoreGrpcClient.Services
{
    public class GreeterService
    {
        private readonly GrpcChannel _channel;
        public GreeterService(GrpcChannel channel) 
        {
            _channel = channel;
        }
        public async Task SayHello()
        {
            var client = new Greeter.GreeterClient(_channel);

            Console.WriteLine("Request:");

            var name = Console.ReadLine();

            var reply = await client.SayHelloAsync(new HelloRequest { Name = name });

            Console.WriteLine($"Response:{reply.Message}");
        }
    }
}
