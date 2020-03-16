using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace forum.middlewares
{
    public class RouteMiddleware
    {
        private readonly RequestDelegate _next;

        public RouteMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();
            if (path.Equals("/rindex"))
            {
                await context.Response.WriteAsync("Index page from route middleware");
            }
            else if (path.Equals("/rshow"))
            {
                await context.Response.WriteAsync("Show page from route middleware");
            }
            else
            {
                context.Response.StatusCode = 404;
            }
        } 
    }
}
