using GrpcServerClient.Services;
using Grpc.Net.Client;

Console.WriteLine("GrpcServerClient");

await Task.Delay(3000);

using var channel = GrpcChannel.ForAddress("http://localhost:8080");
//using var channel = GrpcChannel.ForAddress("https://localhost:8443");

var runnerService = new RunnerService(channel);

await runnerService.Run();
//await runnerService.ClientStreaming();
//await runnerService.ServerStreaming();
//await runnerService.BidirectionalStreaming();

await channel.ShutdownAsync();

Console.ReadLine();