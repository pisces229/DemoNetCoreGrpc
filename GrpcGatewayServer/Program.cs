using GrpcGatewayServer.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

Console.WriteLine("GrpcGatewayServer");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddGrpc().AddJsonTranscoding();

builder.WebHost.ConfigureKestrel((builderContext, serverOptions) =>
{
    serverOptions.ListenAnyIP(50080, listenOptions =>
    {
        listenOptions.UseConnectionLogging();
        listenOptions.Protocols = HttpProtocols.Http2;
    });
    serverOptions.ListenAnyIP(50443, listenOptions =>
    {
        listenOptions.UseConnectionLogging();
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps();
    });
    serverOptions.ListenAnyIP(51080, listenOptions =>
    {
        listenOptions.UseConnectionLogging();
        listenOptions.Protocols = HttpProtocols.Http1;
    });
    serverOptions.ListenAnyIP(51443, listenOptions =>
    {
        listenOptions.UseConnectionLogging();
        listenOptions.Protocols = HttpProtocols.Http1;
        listenOptions.UseHttps();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<RunnerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
