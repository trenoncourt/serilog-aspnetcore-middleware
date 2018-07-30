using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Serilog.Aspnetcore.Middleware
{
    public class HttpContextLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HttpContextLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory.CreateLogger<HttpContextLoggerMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            string responseBody;
            Stream originalBodyStream = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                await _next(context);

                responseBody = await GetResponseBody(context.Response.Body);
                
                await memoryStream.CopyToAsync(originalBodyStream);
            }

            string requestBody = await GetRequestBody(context.Request);
            
            var logContext = new 
            {
                IpAddress = context.Connection.RemoteIpAddress.ToString(),
                Host = context.Request.Host.ToString(),
                Path = context.Request.Path.ToString(),
                context.Request.Method,
                QueryString = context.Request.QueryString.ToString(),
                Headers = context.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
                RequestBody = requestBody,
                ResponseBody = responseBody
            };
            
            _logger.LogInformation("{@logContext}", logContext);
        }
        
        private Task<string> GetRequestBody(HttpRequest request)
        {
            if (request.ContentLength.HasValue && request.ContentLength > 0)
            {
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    return reader.ReadToEndAsync();
                }
            }

            return Task.FromResult("");
        }

        private async Task<string> GetResponseBody(Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);
            string bodyString = await new StreamReader(body).ReadToEndAsync(); 
            body.Seek(0, SeekOrigin.Begin);

            return bodyString;
        }
    }
}