using E_Commerce.API.Errors;
using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = environment.IsDevelopment()
                    ? new APIException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new APIException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
