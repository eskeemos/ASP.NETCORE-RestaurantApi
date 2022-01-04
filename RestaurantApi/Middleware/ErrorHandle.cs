using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestaurantApi.Exceptions;
using System;
using System.Threading.Tasks;

namespace RestaurantApi.Middleware
{
    public class ErrorHandle : IMiddleware
    {
        private readonly ILogger<ErrorHandle> logger;

        public ErrorHandle(ILogger<ErrorHandle> logger)
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException nfe )
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(nfe.Message);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
