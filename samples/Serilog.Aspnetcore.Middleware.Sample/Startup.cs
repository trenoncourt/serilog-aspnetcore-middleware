﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Serilog.Aspnetcore.Middleware.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpContextLogger();
            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}