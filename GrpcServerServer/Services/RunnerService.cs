using Grpc.Core;

namespace GrpcServerServer.Services
{
    public class RunnerService : Runner.RunnerBase
    {
        private readonly ILogger<RunnerService> _logger;
        public RunnerService(ILogger<RunnerService> logger)
        {
            _logger = logger;
        }
        public override async Task<RunnerResponse> Run(RunnerRequest request,
                    ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation("{v}", context.Deadline.ToString());
            _logger.LogInformation("{v}", context.RequestHeaders.GetValue("user-agent"));
            _logger.LogInformation("{v}", context.RequestHeaders.GetValue("bearer"));
            _logger.LogInformation("{v}", request.Message);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var response = new RunnerResponse()
            {
                Message = Guid.NewGuid().ToString()
            };
            _logger.LogInformation("...>");
            return response;
        }
        //public override async Task<RunnerResponse> ClientStreaming(
        //    IAsyncStreamReader<RunnerRequest> requestStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("<...");
        //    while (await requestStream.MoveNext())
        //    {
        //        var request = requestStream.Current;
        //        _logger.LogInformation("{v}", request.Message);
        //    }
        //    _logger.LogInformation("...>");
        //    return new RunnerResponse()
        //    {
        //        Message = Guid.NewGuid().ToString(),
        //    };
        //}
        public override async Task<RunnerResponse> ClientStreaming(
            IAsyncStreamReader<RunnerRequest> requestStream, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            await foreach (var request in requestStream.ReadAllAsync())
            {
                _logger.LogInformation("{v}", request.Message);
            }
            var response = new RunnerResponse()
            {
                Message = Guid.NewGuid().ToString(),
            };
            _logger.LogInformation("...>");
            return response;
        }
        //public override async Task ServerStreaming(RunnerRequest request,
        //    IServerStreamWriter<RunnerResponse> responseStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("<...");
        //    _logger.LogInformation("{v}", request.Message);
        //    for (var i = 0; i < 5; i++)
        //    {
        //        await responseStream.WriteAsync(new RunnerResponse()
        //        {
        //            Message = Guid.NewGuid().ToString()
        //        });
        //        await Task.Delay(TimeSpan.FromSeconds(1));
        //    }
        //    _logger.LogInformation("...>");
        //}
        public override async Task ServerStreaming(RunnerRequest request,
            IServerStreamWriter<RunnerResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation("{v}", request.Message);
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new RunnerResponse()
                {
                    Message = Guid.NewGuid().ToString()
                });
                //await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            _logger.LogInformation("...>");
        }
        //public override async Task BidirectionalStreaming(IAsyncStreamReader<RunnerRequest> requestStream,
        //    IServerStreamWriter<RunnerResponse> responseStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("<...");
        //    await foreach (var request in requestStream.ReadAllAsync())
        //    {
        //        _logger.LogInformation("{v}", request.Message);
        //        await responseStream.WriteAsync(new RunnerResponse()
        //        { 
        //            Message = Guid.NewGuid().ToString(),
        //        });
        //    }
        //    _logger.LogInformation("...>");
        //}
        public override async Task BidirectionalStreaming(IAsyncStreamReader<RunnerRequest> requestStream,
            IServerStreamWriter<RunnerResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            // Read requests in a background task.
            var requestTask = Task.Run(async () =>
            {
                await foreach (var request in requestStream.ReadAllAsync())
                {
                    _logger.LogInformation("{v}", request.Message);
                }
            });
            // Send responses until the client signals that it is complete.
            var responseTask = Task.Run(async () =>
            {
                while (!requestTask.IsCompleted)
                {
                    await responseStream.WriteAsync(new RunnerResponse()
                    {
                        Message = Guid.NewGuid().ToString()
                    });
                    await Task.Delay(TimeSpan.FromSeconds(0.5), context.CancellationToken);
                }
            });
            await responseTask;
            _logger.LogInformation("...>");
        }
    }
}