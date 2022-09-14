using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserCabinet.Service.Exceptions.Users;

namespace UserCabinet.Api.Middlewares
{
    public class UserExceptionMiddleware
    {
        private RequestDelegate next;
        private readonly ILogger<UserExceptionMiddleware> logger;
        public UserExceptionMiddleware(RequestDelegate next, ILogger<UserExceptionMiddleware> logger)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (UserException ex)
            {
                await HandleExceptionAsync(context, ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, 500, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}