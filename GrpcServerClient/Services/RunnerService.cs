using Grpc.Core;
using Grpc.Net.Client;
using GrpcServerServer;

namespace GrpcServerClient.Services
{
    public class RunnerService
    {
        private readonly GrpcChannel _channel;
        public RunnerService(GrpcChannel channel)
        {
            _channel = channel;
        }
        public async Task Run()
        {
            var client = new Runner.RunnerClient(_channel);
            var request = new RunnerRequest()
            {
                Message = Guid.NewGuid().ToString(),
            };
            var metadata = new Metadata
            {
                { "bearer", Guid.NewGuid().ToString() }
            };
            var deadLine = DateTime.UtcNow.AddMinutes(1);
            var cancellationTokenSource = new CancellationTokenSource();
            using var unaryCall = client.RunAsync(
                request, metadata, deadLine, cancellationTokenSource.Token);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine(response.Message);
        }
        public async Task ClientStreaming()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new Runner.RunnerClient(_channel);
            using var clientCtreamingCall = client.ClientStreaming();
            var requestStream = clientCtreamingCall.RequestStream;
            var count = 0;
            while (true)
            {
                await requestStream.WriteAsync(
                    new RunnerRequest() { Message = Guid.NewGuid().ToString() },
                    cancellationTokenSource.Token);
                if (++count == 3)
                {
                    await requestStream.CompleteAsync();
                    break;
                }
            }
            var response = await clientCtreamingCall.ResponseAsync;
            Console.WriteLine(response.Message);
        }
        public async Task ServerStreaming()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new Runner.RunnerClient(_channel);
            using var serverStreamingCall = client.ServerStreaming(new RunnerRequest()
            {
                Message = Guid.NewGuid().ToString(),
            });
            var responseStream = serverStreamingCall.ResponseStream;
            var count = 0;
            while (await responseStream.MoveNext(cancellationTokenSource.Token))
            {
                Console.WriteLine(responseStream.Current.Message);
                if (++count == 3)
                {
                    cancellationTokenSource.Cancel(false);
                    break;
                }
            }
        }
        public async Task BidirectionalStreaming()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new Runner.RunnerClient(_channel);
            using var duplexStreamingCall = client.BidirectionalStreaming();
            var requestStream = duplexStreamingCall.RequestStream;
            var responseStream = duplexStreamingCall.ResponseStream;
            var requestTask = Task.Run(async () =>
            {
                var count = 0;
                while (true)
                {
                    await requestStream.WriteAsync(
                        new RunnerRequest() { Message = Guid.NewGuid().ToString() },
                        cancellationTokenSource.Token);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    if (++count == 10)
                    {
                        await requestStream.CompleteAsync();
                        break;
                    }
                }
            });
            var responsTask = Task.Run(async () =>
            {
                await foreach (var response in responseStream.ReadAllAsync())
                {
                    Console.WriteLine(response.Message);
                }
            });
            await requestTask;
            await responsTask;
        }
    }
}
