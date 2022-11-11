using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BrainstormSessions.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(config =>
            {
                var logger = loggerFactory.CreateLogger("ExceptionHandlerMiddleware");

                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        logger.LogError("Caught unhandled exception '{Message}' with stack trace: '{StackTrace}'.", exception.Message, exception.StackTrace);

                        if (environment.IsDevelopment())
                        {
                            await context.Response.WriteAsync($"{exception.Message} {Environment.NewLine} {exception.StackTrace}");
                        }
                    }
                });
            });

            return app;
        }
    }
}
