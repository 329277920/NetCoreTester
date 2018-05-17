using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Controllers;
using NetCoreTester.Framework.AspNetCore;

namespace NetCoreTester
{  
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {          
            services.AddMvc();

            // 注册框架的所有服务
            services.AddFramework();

            // 注册数据层的所有服务（可通过反射配置）
            services.AddSingleton(typeof(NetCoreTester.Dal.Interfaces.IUser), typeof(NetCoreTester.Dal.Users));

            // 注册业务层的所有服务（可通过反射配置）
            services.AddSingleton(typeof(NetCoreTester.Bll.Interfaces.IUser), typeof(NetCoreTester.Bll.Users));

            // services.Configure<MyConfig>(Configuration);

            // var cc = Configuration.Get<MyConfig>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }           
            app.UseMvc();            
        }
    }
}
