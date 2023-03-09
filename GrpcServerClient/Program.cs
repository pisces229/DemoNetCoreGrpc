using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcServerClient.Services;
using Microsoft.Extensions.Logging;

Console.WriteLine("GrpcServerClient");

await Task.Delay(3000);

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders();
    builder.AddConsole();
});

var channel = GrpcChannel.ForAddress("http://localhost:50080");
//using var channel = GrpcChannel.ForAddress("https://localhost:50443");

var callInvoker = channel.Intercept(new ClientLoggerInterceptor(loggerFactory.CreateLogger<ClientLoggerInterceptor>()));

var runnerService = new RunnerService(loggerFactory.CreateLogger<RunnerService>(), callInvoker);

await runnerService.Run();
await runnerService.ClientStreaming();
await runnerService.ServerStreaming();
await runnerService.BidirectionalStreaming();

await channel.ShutdownAsync();

Console.ReadLine();