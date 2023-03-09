using DemoNetCoreGrpcService;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Text;
using System.Threading;

namespace DemoNetCoreGrpcClient.Services
{
    public class AppService
    {
        private readonly GrpcChannel _channel;
        public AppService(GrpcChannel channel)
        {
            _channel = channel;
        }
        public async Task UnaryCall()
        {
            var client = new App.AppClient(_channel);
            var request = new AppRequest()
            {
                Message = Guid.NewGuid().ToString(),
            };
            var metadata = new Metadata
            {
                { "bearer", Guid.NewGuid().ToString() }
            };
            var deadLine = DateTime.UtcNow.AddMinutes(1);
            var cancellationTokenSource = new CancellationTokenSource();
            using var unaryCall = client.UnaryCallAsync(
                request, metadata, deadLine, cancellationTokenSource.Token);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine(response.Message);
        }
        public async Task StreamingFromClient()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new App.AppClient(_channel);
            using var clientCtreamingCall = client.StreamingFromClient();
            var requestStream = clientCtreamingCall.RequestStream;
            var count = 0;
            while (true)
            {
                await requestStream.WriteAsync(
                    new AppRequest() { Message = Guid.NewGuid().ToString() },
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
        public async Task StreamingFromServer()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new App.AppClient(_channel);
            using var serverStreamingCall = client.StreamingFromServer(new AppRequest()
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
        public async Task StreamingBothWays()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var client = new App.AppClient(_channel);
            using var duplexStreamingCall = client.StreamingBothWays();
            var requestStream = duplexStreamingCall.RequestStream;
            var responseStream = duplexStreamingCall.ResponseStream;
            var requestTask = Task.Run(async () =>
            {
                var count = 0;
                while (true)
                {
                    await requestStream.WriteAsync(
                        new AppRequest() { Message = Guid.NewGuid().ToString() },
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
        public async Task DoValue()
        {
            var client = new App.AppClient(_channel);
            var request = new AppDoValueRequest()
            {
               BoolValue = true,
               IntValue = 1,
               DoubleValue = 1.1,
               StringValue = Guid.NewGuid().ToString(),
            };
            using var unaryCall = client.DoValueAsync(request);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine($"BoolValue:[{request.BoolValue}]");
            Console.WriteLine($"IntValue:[{request.IntValue}]");
            Console.WriteLine($"DoubleValue:[{request.DoubleValue}]");
            Console.WriteLine($"StringValue:[{request.StringValue}]");
        }
        public async Task DoUpload()
        {
            var client = new App.AppClient(_channel);
            var request = new AppDoUploadRequest()
            {
                Name = Guid.NewGuid().ToString(),
                Data = ByteString.CopyFrom(Guid.NewGuid().ToString(), Encoding.Default),
            };
            using var unaryCall = client.DoUploadAsync(request);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine($"Message:[{response.Message}]");
        }
        public async Task DoDownload()
        {
            var client = new App.AppClient(_channel);
            var request = new AppDoDownloadRequest()
            {
                Name = Guid.NewGuid().ToString(),
            };
            using var unaryCall = client.DoDownloadAsync(request);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine($"Name:[{response.Name}]");
            Console.WriteLine($"Data:[{Encoding.Default.GetString(response.Data.ToArray())}]");
        }
        public async Task DoUploadStream()
        {
            var client = new App.AppClient(_channel);
            using var clientCtreamingCall = client.DoUploadStream();
            var requestStream = clientCtreamingCall.RequestStream;
            var values = Guid.NewGuid().ToString();
            foreach (var value in values)
            {
                await requestStream.WriteAsync(new AppDoUploadStreamRequest() 
                { 
                    Data = ByteString.CopyFrom(value.ToString(), Encoding.Unicode)
                });
            }
            await requestStream.CompleteAsync();
            var response = await clientCtreamingCall.ResponseAsync;
            Console.WriteLine(response.Message);
        }
        public async Task DoDownloadStream()
        {
            var client = new App.AppClient(_channel);
            using var serverCtreamingCall = client.DoDownloadStream(new AppDoDownloadStreamRequest()
            { 
                Message = Guid.NewGuid().ToString(),
            });
            var responseStream = serverCtreamingCall.ResponseStream;
            string message;
            using (var memoryStream = new MemoryStream())
            {
                var currentOffset = 0;
                await foreach (var request in responseStream.ReadAllAsync())
                {
                    await memoryStream.WriteAsync(request.Data.ToByteArray(), currentOffset, request.Data.Length);
                }
                message = Encoding.Default.GetString(memoryStream.ToArray());
            }
            Console.WriteLine(message);
        }
    }
}
