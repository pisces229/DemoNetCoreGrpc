using Google.Protobuf;
using Grpc.Core;
using System.Text;

namespace DemoNetCoreGrpcService.Services
{
    public class AppService : App.AppBase
    {
        private readonly ILogger<AppService> _logger;
        public AppService(ILogger<AppService> logger)
        {
            _logger = logger;
        }
        public override async Task<AppResponse> UnaryCall(AppRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation(context.Deadline.ToString());
            _logger.LogInformation(context.RequestHeaders.GetValue("user-agent"));
            _logger.LogInformation(context.RequestHeaders.GetValue("bearer"));
            _logger.LogInformation(request.Message);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var response = new AppResponse()
            {
                Message = Guid.NewGuid().ToString()
            };
            _logger.LogInformation("...>");
            return response;
        }
        //public override async Task StreamingFromServer(AppRequest request,
        //    IServerStreamWriter<AppResponse> responseStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("<...");
        //    _logger.LogInformation(request.Message);
        //    for (var i = 0; i < 5; i++)
        //    {
        //        await responseStream.WriteAsync(new AppResponse()
        //        {
        //            Message = Guid.NewGuid().ToString()
        //        });
        //        await Task.Delay(TimeSpan.FromSeconds(1));
        //    }
        //    _logger.LogInformation("...>");
        //}
        public override async Task StreamingFromServer(AppRequest request,
            IServerStreamWriter<AppResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation(request.Message);
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new AppResponse()
                {
                    Message = Guid.NewGuid().ToString()
                });
                //await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            _logger.LogInformation("...>");
        }
        //public override async Task<AppResponse> StreamingFromClient(
        //    IAsyncStreamReader<AppRequest> requestStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("<...");
        //    while (await requestStream.MoveNext())
        //    {
        //        var request = requestStream.Current;
        //        _logger.LogInformation(request.Message);
        //    }
        //    _logger.LogInformation("...>");
        //    return new AppResponse()
        //    {
        //        Message = Guid.NewGuid().ToString(),
        //    };
        //}
        public override async Task<AppResponse> StreamingFromClient(
            IAsyncStreamReader<AppRequest> requestStream, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            await foreach (var request in requestStream.ReadAllAsync())
            {
                _logger.LogInformation(request.Message);
            }
            var response = new AppResponse()
            {
                Message = Guid.NewGuid().ToString(),
            };
            _logger.LogInformation("...>");
            return response;
        }
        //public override async Task StreamingBothWays(IAsyncStreamReader<AppRequest> requestStream,
        //    IServerStreamWriter<AppResponse> responseStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("<...");
        //    await foreach (var request in requestStream.ReadAllAsync())
        //    {
        //        _logger.LogInformation(request.Message);
        //        await responseStream.WriteAsync(new AppResponse()
        //        { 
        //            Message = Guid.NewGuid().ToString(),
        //        });
        //    }
        //    _logger.LogInformation("...>");
        //}
        public override async Task StreamingBothWays(IAsyncStreamReader<AppRequest> requestStream,
            IServerStreamWriter<AppResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            // Read requests in a background task.
            var requestTask = Task.Run(async () =>
            {
                await foreach (var request in requestStream.ReadAllAsync())
                {
                    _logger.LogInformation(request.Message);
                }
            });
            // Send responses until the client signals that it is complete.
            var responseTask = Task.Run(async () =>
            {
                while (!requestTask.IsCompleted)
                {
                    await responseStream.WriteAsync(new AppResponse()
                    {
                        Message = Guid.NewGuid().ToString()
                    });
                    await Task.Delay(TimeSpan.FromSeconds(0.5), context.CancellationToken);
                }
            });
            await responseTask;
            _logger.LogInformation("...>");
        }
        public override async Task<AppDoValueResponse> DoValue(AppDoValueRequest request, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation($"BoolValue:[{request.BoolValue}]");
            _logger.LogInformation($"IntValue:[{request.IntValue}]");
            _logger.LogInformation($"DoubleValue:[{request.DoubleValue}]");
            _logger.LogInformation($"StringValue:[{request.StringValue}]");
            await Task.Delay(TimeSpan.FromSeconds(1));
            var response = new AppDoValueResponse()
            {
                BoolValue = request.BoolValue,
                IntValue = request.IntValue,
                DoubleValue = request.DoubleValue,
                StringValue = request.StringValue,
            };
            _logger.LogInformation("...>");
            return response;
        }
        public override async Task<AppDoUploadResponse> DoUpload(AppDoUploadRequest request, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            var message = Encoding.Default.GetString(request.Data.ToArray());
            await Task.Delay(TimeSpan.FromSeconds(1));
            _logger.LogInformation(request.Name);
            _logger.LogInformation(message);
            var reponse = new AppDoUploadResponse()
            {
                Message = message,
            };
            _logger.LogInformation("...>");
            return reponse;
        }
        public override async Task<AppDoDownloadResponse> DoDownload(AppDoDownloadRequest request, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation(request.Name);
            var reponse = new AppDoDownloadResponse()
            {
                Name = request.Name,
                Data = ByteString.CopyFrom(Guid.NewGuid().ToString(), Encoding.Default),
            };
            _logger.LogInformation("...>");
            return await Task.FromResult(reponse);
        }
        public override async Task<AppDoUploadStreamResponse> DoUploadStream(IAsyncStreamReader<AppDoUploadStreamRequest> requestStream, ServerCallContext context)
        {
            string message;
            using (var memoryStream = new MemoryStream())
            {
                var currentOffset = 0;
                await foreach (var request in requestStream.ReadAllAsync())
                {
                    await memoryStream.WriteAsync(request.Data.ToByteArray(), currentOffset, request.Data.Length);
                }
                message = Encoding.Default.GetString(memoryStream.ToArray());
            }
            await Task.Delay(TimeSpan.FromSeconds(1));
            _logger.LogInformation(message);
            var reponse = new AppDoUploadStreamResponse()
            {
                Message = message,
            }; 
            _logger.LogInformation("...>");
            return reponse;
        }
        public override async Task DoDownloadStream(AppDoDownloadStreamRequest request,
            IServerStreamWriter<AppDoDownloadStreamResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation(request.Message);
            var values = request.Message;
            foreach (var value in values)
            {
                await responseStream.WriteAsync(new AppDoDownloadStreamResponse()
                {
                    Data = ByteString.CopyFrom(value.ToString(), Encoding.Unicode)
                });
            }
            _logger.LogInformation("<<<");
        }
    }
}
