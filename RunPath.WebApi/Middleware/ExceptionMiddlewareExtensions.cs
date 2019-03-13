using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }


            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Exceptions");
                        var msg = $"Something went wrong: {contextFeature.Error}";

                        logger.LogError(msg);

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}