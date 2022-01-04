using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Middleware
{
    public class RequestTime : IMiddleware
    {
        private Stopwatch stopWatch;
        private readonly ILogger<RequestTime> logger;

        public RequestTime(ILogger<RequestTime> logger)
        {
            this.stopWatch = new Stopwatch();
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            stopWatch.Start();
            await next.Invoke(context);
            stopWatch.Stop();

            var ms = stopWatch.ElapsedMilliseconds;
            if(ms / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method} at {context.Request.Path} took {ms} ms]";
                logger.LogInformation(message);
            }
        }
    }
}
