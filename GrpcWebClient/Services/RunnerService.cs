using Grpc.Core;
using Grpc.Net.Client;
using GrpcWebServer;
using Microsoft.Extensions.Logging;

namespace GrpcWebClient.Services
{
    public class RunnerService(ILogger<RunnerService> _logger, GrpcChannel _channel)
    {
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
            using var unaryCall = 
                client.RunAsync(request, metadata, deadLine, cancellationTokenSource.Token);
            var response = await unaryCall.ResponseAsync;
            _logger.LogInformation(response.Message);
        }
        public async Task ServerStreaming()
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
            using var serverStreamingCall = 
                client.ServerStreaming(request, metadata, deadLine, cancellationTokenSource.Token);
            var responseStream = serverStreamingCall.ResponseStream;
            var count = 0;
            while (await responseStream.MoveNext(cancellationTokenSource.Token))
            {
                _logger.LogInformation(responseStream.Current.Message);
                if (++count == 5)
                {
                    cancellationTokenSource.Cancel(false);
                    break;
                }
            }
        }
    }
}
