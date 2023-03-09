namespace DemoNetCoreGrpcService.Middlewares
{
    public class DefaultMiddleware
    {
        private readonly RequestDelegate _dequestDelegate;
        public DefaultMiddleware(RequestDelegate requestDelegate)
        {
            _dequestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            await _dequestDelegate(context);
        }
    }
}
