using Grpc.Core;

namespace GrpcGatewayServer.Services
{
    public class RunnerService(ILogger<RunnerService> _logger) : Runner.RunnerBase
    {
        public override async Task<RunnerResponse> RunGet(RunnerRequest request, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation("{v}", context.Deadline.ToString());
            _logger.LogInformation("{v}", context.RequestHeaders.GetValue("user-agent"));
            _logger.LogInformation("{v}", context.RequestHeaders.GetValue("bearer"));
            _logger.LogInformation("{v}", request.Name);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var response = new RunnerResponse()
            {
                Message = Guid.NewGuid().ToString()
            };
            _logger.LogInformation("...>");
            return response;
        }
        public override async Task<RunnerResponse> RunPost(RunnerRequest request, ServerCallContext context)
        {
            _logger.LogInformation("<...");
            _logger.LogInformation("{v}", context.Deadline.ToString());
            _logger.LogInformation("{v}", context.RequestHeaders.GetValue("user-agent"));
            _logger.LogInformation("{v}", context.RequestHeaders.GetValue("bearer"));
            _logger.LogInformation("{v}", request.Name);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var response = new RunnerResponse()
            {
                Message = Guid.NewGuid().ToString()
            };
            _logger.LogInformation("...>");
            return response;
        }
    }
}