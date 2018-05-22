using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetCoreTester.StartupTester.Services;
using NetCoreTester.StartupTester.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace NetCoreTester.StartupTester
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;          
        }

        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            // services.AddOptions().Configure<AppSettings>(Configuration);

            //var cfg1 = Configuration.Get<AppSettings>();

            //var cfg = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<MiddlewareConfiguration>();
            //app.Run(async (context) =>
            //{               
            //    await context.Response.WriteAsync(this.Configuration.Get<ConfigInfo>().Version);
            //});            
        }
    }
}
