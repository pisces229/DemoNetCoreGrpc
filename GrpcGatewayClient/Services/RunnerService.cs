using Grpc.Core;
using Grpc.Net.Client;
using GrpcGatewayServer;
using System.Text;
using System.Text.Json;

namespace GrpcGatewayClient.Services
{
    public class RunnerService
    {
        private readonly GrpcChannel _channel;
        private readonly HttpClient _httpClient;
        public RunnerService(GrpcChannel channel, HttpClient httpClient)
        {
            _channel = channel;
            _httpClient = httpClient;
        }
        public async Task GrpcRunGet() 
        {
            var client = new Runner.RunnerClient(_channel);
            var request = new RunnerRequest()
            {
                Name = Guid.NewGuid().ToString(),
            };
            var metadata = new Metadata
            {
                //{ "bearer", Guid.NewGuid().ToString() }
            };
            var deadLine = DateTime.UtcNow.AddMinutes(1);
            var cancellationTokenSource = new CancellationTokenSource();
            using var unaryCall = client.RunGetAsync(
                request, metadata, deadLine, cancellationTokenSource.Token);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine(response.Message);
        }
        public async Task GrpcRunPost()
        {
            var client = new Runner.RunnerClient(_channel);
            var request = new RunnerRequest()
            {
                Name = Guid.NewGuid().ToString(),
            };
            var metadata = new Metadata
            {
                //{ "bearer", Guid.NewGuid().ToString() }
            };
            var deadLine = DateTime.UtcNow.AddMinutes(1);
            var cancellationTokenSource = new CancellationTokenSource();
            using var unaryCall = client.RunPostAsync(
                request, metadata, deadLine, cancellationTokenSource.Token);
            var response = await unaryCall.ResponseAsync;
            Console.WriteLine(response.Message);
        }
        public async Task HttpRunGet()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/api/Runner/RunGet/{Guid.NewGuid()}");
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(response);
        }
        public async Task HttpRunPost()
        {
            var requestJson = JsonSerializer.Serialize(new
            {
                name = Guid.NewGuid().ToString(),
            });
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PostAsync($"/api/Runner/RunPost", requestContent);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(response);
        }
    }
}
