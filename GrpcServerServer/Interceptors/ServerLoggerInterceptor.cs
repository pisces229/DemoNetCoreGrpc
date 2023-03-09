using Grpc.Core;
using Grpc.Core.Interceptors;

public class ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> _logger) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Server Unary Call Started: {context.Method}");
        _logger.LogDebug($"Request: {request}");

        try
        {
            var response = await continuation(request, context);
            _logger.LogInformation($"Server Unary Call Completed: {context.Method}");
            _logger.LogDebug($"Response: {response}");
            return response;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, $"Server Unary Call Error: {context.Method} - Status: {ex.Status.StatusCode}, Detail: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Server Unary Call Unexpected Error: {context.Method}");
            throw;
        }
    }

    public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        ServerCallContext context,
        ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Server Client Streaming Call Started: {context.Method}");

        var wrappedRequestStream = new ServerStreamReader<TRequest>(requestStream, context.Method, _logger);

        try
        {
            var response = await continuation(wrappedRequestStream, context);
            _logger.LogInformation($"Server Client Streaming Call Completed: {context.Method}");
            _logger.LogInformation($"Response: {response}");
            return response;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, $"Server Client Streaming Call Error: {context.Method} - Status: {ex.Status.StatusCode}, Detail: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Server Client Streaming Call Unexpected Error: {context.Method}");
            throw;
        }
    }

    public override async Task ServerStreamingServerHandler<TRequest, TResponse>(
        TRequest request,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Server Server Streaming Call Started: {context.Method}");
        _logger.LogInformation($"Request: {request}");

        var wrappedResponseStream = new ServerStreamWriter<TResponse>(responseStream, context.Method, _logger);

        try
        {
            await continuation(request, wrappedResponseStream, context);
            _logger.LogInformation($"Server Server Streaming Call Completed: {context.Method}");
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, $"Server Server Streaming Call Error: {context.Method} - Status: {ex.Status.StatusCode}, Detail: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Server Server Streaming Call Unexpected Error: {context.Method}");
            throw;
        }
    }

    public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Server Duplex Streaming Call Started: {context.Method}");

        var wrappedRequestStream = new ServerStreamReader<TRequest>(requestStream, context.Method, _logger);
        var wrappedResponseStream = new ServerStreamWriter<TResponse>(responseStream, context.Method, _logger);

        try
        {
            await continuation(wrappedRequestStream, wrappedResponseStream, context);
            _logger.LogInformation($"Server Duplex Streaming Call Completed: {context.Method}");
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, $"Server Duplex Streaming Call Error: {context.Method} - Status: {ex.Status.StatusCode}, Detail: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Server Duplex Streaming Call Unexpected Error: {context.Method}");
            throw;
        }
    }

    private class ServerStreamReader<TRequest>(
        IAsyncStreamReader<TRequest> _innerReader,
        string _method, 
        ILogger _logger) : IAsyncStreamReader<TRequest>
    {
        public TRequest Current => _innerReader.Current;

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            var moved = await _innerReader.MoveNext(cancellationToken);
            if (moved)
            {
                _logger.LogInformation($"Streaming Call [{_method}]: Received request message: {_innerReader.Current}");
            }
            else
            {
                _logger.LogInformation($"Streaming Call [{_method}]: End of request stream.");
            }
            return moved;
        }
    }

    private class ServerStreamWriter<TResponse>(
        IServerStreamWriter<TResponse> _innerWriter, 
        string _method, 
        ILogger _logger) : IServerStreamWriter<TResponse>
    {
        public WriteOptions? WriteOptions
        {
            get => _innerWriter.WriteOptions;
            set => _innerWriter.WriteOptions = value;
        }

        public async Task WriteAsync(TResponse message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Streaming Call [{_method}]: Writing response message: {message}");
            await _innerWriter.WriteAsync(message, cancellationToken);
        }

        public async Task WriteAsync(TResponse message)
        {
            _logger.LogInformation($"Streaming Call [{_method}]: Writing response message: {message}");
            await _innerWriter.WriteAsync(message);
        }
    }
}