using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace forum.middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await this._next.Invoke(context);
            int statusCode = context.Response.StatusCode;
            if (statusCode.Equals(403))
            {
                await context.Response.WriteAsync("Access denied");
            }
            else if (statusCode.Equals(404))
            {
                await context.Response.WriteAsync("Page not found");
            }
        }
    }
}
