using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RemitaMiddleWare.Helpers
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string authHeader = context.Request.Headers["CUSTOMAUTH"];
                if (authHeader != null && authHeader.StartsWith("CUSTOM"))
                {
                    string authString = authHeader.Substring("CUSTOM ".Length).Trim();

                    if (authString == "*******")
                    {
                        await _next(context);
                    }
                    else
                    {
                        // no authorization header
                        context.Response.StatusCode = 401; //Unauthorized
                        return;
                    }
                }
                else
                {
                    // wrong key
                    context.Response.StatusCode = 401; //Unauthorized
                    return;
                }
            }
            catch (Exception ex)
            {
                //no header
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder Authenticate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
