using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

using forum.middlewares;

namespace forum
{
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string validToken)
        {
            return builder.UseMiddleware<TokenMiddleware>(validToken);
        }
    }
}
