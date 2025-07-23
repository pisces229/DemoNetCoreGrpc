using Grpc.Net.Client;
using GrpcGatewayClient.Services;
using Microsoft.Extensions.Logging;

Console.WriteLine("GrpcGatewayClient");

await Task.Delay(3000);

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();
});

//using var channel = GrpcChannel.ForAddress("http://localhost:50080");
using var channel = GrpcChannel.ForAddress("https://localhost:50443");
//using var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:51080") };
using var httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:51443") };

var runnerService = new RunnerService(loggerFactory.CreateLogger<RunnerService>(), channel, httpClient);

await runnerService.GrpcRunGet();
await runnerService.GrpcRunPost();
await runnerService.HttpRunGet();
await runnerService.HttpRunPost();

await channel.ShutdownAsync();

Console.ReadLine();
