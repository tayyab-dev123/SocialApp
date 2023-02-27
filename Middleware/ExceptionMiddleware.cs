using SocialApp.Errors;
using System.Net;
using System.Text.Json;

namespace SocialApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _Logger;
        private readonly IHostEnvironment _Env;

        // RequestDelecate=> what's coming next 
        // Ilogger to logout error in terminal or in a file and need <ExceptionMiddleware>
        // IHostEnvironment => Which environment our project is running 
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _Logger = logger;
            _Env = env;
        }

        //Create method 
        //Parameter 
        /*
          HttpContext: Access to HTTP incoming request 
          Create Try catch
          In this method in try block pass next(context)=> if any middleware has exception it pass it next(UP) where it get handled
          In catch block 
           1 log error
           2 set ContentType
           3 set StatusCode eg (int) HttpStatusCode.InternalServerError
           4 set env response if env id development then .... or in production then ...
           5 use JsonSerializerOption to set naming policy to CamelCase
           6 Serialize (resonse, option) 
           7 Write json response
         */

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _Env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Server Issue");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);

            }
        }
    }
}
