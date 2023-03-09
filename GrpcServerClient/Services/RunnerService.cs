using Grpc.Core;
using GrpcServerServer;
using Microsoft.Extensions.Logging;

namespace GrpcServerClient.Services
{
    public class RunnerService(ILogger<RunnerService> _logger, CallInvoker _callInvoker)
    {
        public async Task Run()
        {
            var client = new Runner.RunnerClient(_callInvoker);
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
            _logger.LogInformation(response.Message);
        }
        public async Task ClientStreaming()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new Runner.RunnerClient(_callInvoker);
            using var clientCtreamingCall = client.ClientStreaming(cancellationToken: cancellationTokenSource.Token);
            var requestStream = clientCtreamingCall.RequestStream;
            var count = 0;
            while (true)
            {
                await requestStream.WriteAsync(
                    new RunnerRequest() { Message = Guid.NewGuid().ToString() });
                if (++count == 3)
                {
                    await requestStream.CompleteAsync();
                    break;
                }
            }
            var response = await clientCtreamingCall.ResponseAsync;
            _logger.LogInformation(response.Message);
        }
        public async Task ServerStreaming()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new Runner.RunnerClient(_callInvoker);
            using var serverStreamingCall = client.ServerStreaming(new RunnerRequest()
            {
                Message = Guid.NewGuid().ToString(),
            }, 
            cancellationToken: cancellationTokenSource.Token);
            var responseStream = serverStreamingCall.ResponseStream;
            var count = 0;
            while (await responseStream.MoveNext())
            {
                _logger.LogInformation(responseStream.Current.Message);
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
            var client = new Runner.RunnerClient(_callInvoker);
            using var duplexStreamingCall = client.BidirectionalStreaming(cancellationToken: cancellationTokenSource.Token);
            var requestStream = duplexStreamingCall.RequestStream;
            var responseStream = duplexStreamingCall.ResponseStream;
            var requestTask = Task.Run(async () =>
            {
                var count = 0;
                while (true)
                {
                    await requestStream.WriteAsync(
                        new RunnerRequest() { Message = Guid.NewGuid().ToString() });
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
                    _logger.LogInformation(response.Message);
                }
            });
            await requestTask;
            await responsTask;
        }
    }
}
