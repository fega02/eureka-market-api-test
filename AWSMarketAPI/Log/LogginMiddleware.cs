using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWSMarketAPI.Log
{
    public class LogginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogginMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LogginMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();

                var reader = new StreamReader(context.Request.Body);

                string body = await reader.ReadToEndAsync();
                _logger.LogInformation(
                    $"Request {context.Request?.Method}: {context.Request?.Path.Value}\n{body}");

                context.Request.Body.Position = 0L;
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    $"Request {context.Request?.Method}: {context.Request?.Path.Value} failed");
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}
