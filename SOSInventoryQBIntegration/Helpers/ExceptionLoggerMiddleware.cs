using Common.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace SOSInventoryQBIntegration.Helpers
{
    public class ExceptionLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggerMiddleware> _logger;
        private readonly bool _isDevelopment = false;
        public ExceptionLoggerMiddleware(RequestDelegate next, ILogger<ExceptionLoggerMiddleware> logger, IOptions<ExecptionLoggingOption> options)
        {
            _next = next;
            _logger = logger;
            _isDevelopment = options?.Value?.isDevelopment ?? false;
        }

        /// <summary>
        /// Invoke in every request
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //If exception, then log in database.
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception: exception, exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDTO<string>()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = "Something went wrong, please contact admin",
                Data = _isDevelopment ? exception.ToString() : ""
            }));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLoggerMiddleware(this IApplicationBuilder builder, ExecptionLoggingOption options = null)
        {
            return builder.UseMiddleware<ExceptionLoggerMiddleware>(Options.Create(options));
        }
    }

    public class ExecptionLoggingOption
    {
        public bool isDevelopment { get; set; }
    }
}
