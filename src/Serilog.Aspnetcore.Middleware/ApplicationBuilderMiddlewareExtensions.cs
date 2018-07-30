using System;
using Microsoft.AspNetCore.Builder;

namespace Serilog.Aspnetcore.Middleware
{
    public static class ApplicationBuilderMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextLogger(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<HttpContextLoggerMiddleware>();
        }
    }
}