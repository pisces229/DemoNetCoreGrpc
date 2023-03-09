using GrpcWebServer.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

Console.WriteLine("GrpcWebServer");

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

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
});

// Cors
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});

var app = builder.Build();

app.UseCors();
// app.UseRouting();
// app.UseMiddleware();
app.UseGrpcWeb();
app.MapGrpcService<RunnerService>().EnableGrpcWeb().RequireCors("AllowAll");

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
