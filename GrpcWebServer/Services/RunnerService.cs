using Grpc.Core;
using Grpc.Net.Client;
using GrpcWebServer;

namespace GrpcWebServer.Services
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
            _logger.LogInformation("Run..");
            _logger.LogInformation("Deadline:[{v}]", context.Deadline.ToString());
            _logger.LogInformation("[{v}]", context.RequestHeaders.GetValue("user-agent"));
            _logger.LogInformation("bearer:[{v}]", context.RequestHeaders.GetValue("bearer"));
            _logger.LogInformation("request:[{v}]", request.Message);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var response = new RunnerResponse()
            {
                Message = Guid.NewGuid().ToString()
            };
            _logger.LogInformation("...Run");
            return response;
        }
        //public override async Task ServerStreaming(RunnerRequest request,
        //    IServerStreamWriter<RunnerResponse> responseStream, ServerCallContext context)
        //{
        //    _logger.LogInformation("ServerStreaming...");
        //    _logger.LogInformation("{v}", request.Message);
        //    for (var i = 0; i < 5; i++)
        //    {
        //        await responseStream.WriteAsync(new RunnerResponse()
        //        {
        //            Message = Guid.NewGuid().ToString()
        //        });
        //        await Task.Delay(TimeSpan.FromSeconds(1));
        //    }
        //    _logger.LogInformation("...ServerStreaming");
        //}
        public override async Task ServerStreaming(RunnerRequest request,
            IServerStreamWriter<RunnerResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("ServerStreaming...");
            _logger.LogInformation("Deadline:[{v}]", context.Deadline.ToString());
            _logger.LogInformation("[{v}]", context.RequestHeaders.GetValue("user-agent"));
            _logger.LogInformation("bearer:[{v}]", context.RequestHeaders.GetValue("bearer"));
            _logger.LogInformation("request:[{v}]", request.Message);
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new RunnerResponse()
                {
                    Message = Guid.NewGuid().ToString()
                });
                //await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            _logger.LogInformation("...ServerStreaming");
        }
    }
}