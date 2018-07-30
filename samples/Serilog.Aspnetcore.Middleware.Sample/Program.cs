using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog.Events;

namespace Serilog.Aspnetcore.Middleware.Sample
{
    public class Program
    {
        public static void Main()
        {
            CreateWebHostBuilder().Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder() =>
            new WebHostBuilder()
                .UseKestrel(options => options.AddServerHeader = false)
                .UseSetting(WebHostDefaults.SuppressStatusMessagesKey, "True")
                .UseStartup<Startup>()
                .UseSerilog((context, configuration) => configuration
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .WriteTo.Console());
    }
}