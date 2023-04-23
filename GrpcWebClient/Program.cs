using GrpcWebClient.Services;
using Grpc.Net.Client;

Console.WriteLine("GrpcWebClient");

await Task.Delay(3000);

//using var channel = GrpcChannel.ForAddress("http://localhost:50080");
using var channel = GrpcChannel.ForAddress("https://localhost:50443");

var runnerService = new RunnerService(channel);

//await runnerService.Run();
await runnerService.ServerStreaming();

await channel.ShutdownAsync();

Console.ReadLine();