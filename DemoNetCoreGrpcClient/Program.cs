using DemoNetCoreGrpcClient.Services;
using Grpc.Net.Client;

Console.WriteLine("DemoNetCoreGrpcClient");

await Task.Delay(3000);

using var channel = GrpcChannel.ForAddress("http://localhost:5204");
//using var channel = GrpcChannel.ForAddress("https://localhost:7059");

//await new GreeterService(channel).SayHello();

//await new AppService(channel).UnaryCall();
//await new AppService(channel).StreamingFromClient();
//await new AppService(channel).StreamingFromServer();
//await new AppService(channel).StreamingBothWays();

//await new AppService(channel).DoValue();
//await new AppService(channel).DoUpload();
//await new AppService(channel).DoDownload();

//await new AppService(channel).DoUploadStream();
//await new AppService(channel).DoDownloadStream();

await channel.ShutdownAsync();