using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Infrastructure.Middleware
{
    public static class GlobalErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var errorFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeatures != null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "An error occurred while processing your request.",
                            // You can add more details here, such as:
                            // stackTrace = errorFeatures.Error.StackTrace, 
                            // innerException = errorFeatures.Error.InnerException?.Message 
                        });
                    }
                });
            });
        }
    }
}
