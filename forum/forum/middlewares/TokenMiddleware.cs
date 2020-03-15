using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace forum.middlewares
{
    public class TokenMiddleware
    {
        private RequestDelegate _next;
        private string validToken;

        public TokenMiddleware(RequestDelegate next, string validToken)
        {
            this._next = next;
            this.validToken = validToken;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Query["token"].ToString() ?? "";
            if (!token.Equals(this.validToken))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid token");
            }
            else await _next.Invoke(context);
        }
    }
}
