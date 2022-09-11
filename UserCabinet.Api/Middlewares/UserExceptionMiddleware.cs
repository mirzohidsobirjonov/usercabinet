using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using UserCabinet.Service.Exceptions.Users;

namespace UserCabinet.Api.Middlewares
{
    public class UserExceptionMiddleware
    {
        private RequestDelegate next;
        public UserExceptionMiddleware(RequestDelegate next)
        {
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