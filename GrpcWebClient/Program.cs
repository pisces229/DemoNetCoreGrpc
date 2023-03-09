using Grpc.Net.Client;
using GrpcWebClient.Services;
using Microsoft.Extensions.Logging;

Console.WriteLine("GrpcWebClient");

await Task.Delay(3000);

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();
});

using var channel = GrpcChannel.ForAddress("http://localhost:50080");
//using var channel = GrpcChannel.ForAddress("https://localhost:50443");

var runnerService = new RunnerService(channel);

//await runnerService.Run();
await runnerService.ServerStreaming();

await channel.ShutdownAsync();

Console.ReadLine();