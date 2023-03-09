using DemoNetCoreGrpcService.Middlewares;
using DemoNetCoreGrpcService.Services;
using Microsoft.AspNetCore.Builder;

Console.WriteLine("DemoNetCoreGrpcService");

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
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

// gRPC
//app.UseCors("AllowAll");
//app.UseMiddleware<DefaultMiddleware>();
// Configure the HTTP request pipeline.
//app.MapGrpcService<AppService>();
//app.MapGrpcService<GreeterService>();

// gRPC-Web
app.UseCors();
// app.UseRouting();
// app.UseMiddleware<DefaultMiddleware>();
app.UseGrpcWeb();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller}/{action=Index}/{id?}");
//    endpoints.MapGrpcService<AppService>().EnableGrpcWeb();
//    endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
//});
app.MapGrpcService<AppService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<GreeterService>().EnableGrpcWeb().RequireCors("AllowAll");

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
