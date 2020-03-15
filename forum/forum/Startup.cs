using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using forum.middlewares;

namespace forum
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseToken("12345");

            app.MapWhen(context => {
                return context.Request.Query.ContainsKey("product") &&
                    context.Request.Query.ContainsKey("id") &&
                    context.Request.Query["id"].Equals("10");
            }, IssetParamCont);

            app.Map("/home", home => {
                home.Map("/default", DefaultCont);
                home.Map("/index", IndexCont);
                home.Map("/show", ShowCont);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/index", async context =>
                {
                    await context.Response.WriteAsync("Index page");
                });
            }); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/show", async context =>
                {
                    await context.Response.WriteAsync("Show page");
                });
            });
            app.Run(async context => { 
                await context.Response.WriteAsync("Page not found");
            });
        }

        public static void DefaultCont(IApplicationBuilder app)
        {
            app.Run(async context => {
                await context.Response.WriteAsync("Default Page"); 
            });
        }

        public static void IndexCont(IApplicationBuilder app)
        {
            app.Run(async context => {
                await context.Response.WriteAsync("Index page");
            });
        }
        public static void ShowCont(IApplicationBuilder app)
        {
            app.Run(async context => {
                await context.Response.WriteAsync("Show page");
            });
        }

        public static void IssetParamCont(IApplicationBuilder app)
        {
            app.Run(async context => {
                await context.Response.WriteAsync("All param isset page");
            });
        }
    }
}
