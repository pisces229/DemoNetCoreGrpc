using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

public class ClientLoggerInterceptor(ILogger<ClientLoggerInterceptor> _logger) : Interceptor
{
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Client Unary Call Started: {context.Method.Name}");
        _logger.LogInformation($"Request: {request}");

        var call = continuation(request, context);

        return new AsyncUnaryCall<TResponse>(
            ResponseAsync(call.ResponseAsync, context.Method.Name),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Client Streaming Call Started: {context.Method.Name}");

        var call = continuation(context);

        return new AsyncClientStreamingCall<TRequest, TResponse>(
            new ClientStreamWriter<TRequest>(call.RequestStream, context.Method.Name, _logger),
            ResponseAsync(call.ResponseAsync, context.Method.Name),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Server Streaming Call Started: {context.Method.Name}");
        _logger.LogInformation($"Request: {request}");

        var call = continuation(request, context);

        return new AsyncServerStreamingCall<TResponse>(
            new AsyncStreamReader<TResponse>(call.ResponseStream, context.Method.Name, _logger),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Duplex Streaming Call Started: {context.Method.Name}");

        var call = continuation(context);

        return new AsyncDuplexStreamingCall<TRequest, TResponse>(
            new ClientStreamWriter<TRequest>(call.RequestStream, context.Method.Name, _logger),
            new AsyncStreamReader<TResponse>(call.ResponseStream, context.Method.Name, _logger),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }

    private async Task<TResponse> ResponseAsync<TResponse>(Task<TResponse> innerResponseTask, string method)
    {
        try
        {
            var response = await innerResponseTask;
            _logger.LogInformation($"Client Call Completed: {method}");
            _logger.LogInformation($"Response: {response}");
            return response;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, $"Client Call Error: {method} - Status: {ex.Status.StatusCode}, Detail: {ex.Status.Detail}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Client Call Unexpected Error: {method}");
            throw;
        }
    }

    private class ClientStreamWriter<TRequest>(
        IClientStreamWriter<TRequest> _innerWriter, 
        string _method, 
        ILogger _logger) : IClientStreamWriter<TRequest>
    {
        public WriteOptions? WriteOptions
        {
            get => _innerWriter.WriteOptions;
            set => _innerWriter.WriteOptions = value;
        }

        public async Task CompleteAsync()
        {
            _logger.LogInformation($"Client Streaming Call [{_method}]: Completing request stream.");
            await _innerWriter.CompleteAsync();
        }

        public async Task WriteAsync(TRequest message)
        {
            _logger.LogInformation($"Client Streaming Call [{_method}]: Writing message: {message}");
            await _innerWriter.WriteAsync(message);
        }
    }

    private class AsyncStreamReader<TResponse>(
        IAsyncStreamReader<TResponse> _innerReader, 
        string _method, 
        ILogger _logger) : IAsyncStreamReader<TResponse>
    {
        public TResponse Current => _innerReader.Current;

        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            var moved = await _innerReader.MoveNext(cancellationToken);
            if (moved)
            {
                _logger.LogInformation($"Streaming Call [{_method}]: Received response: {_innerReader.Current}");
            }
            else
            {
                _logger.LogInformation($"Streaming Call [{_method}]: End of response stream.");
            }
            return moved;
        }
    }
}