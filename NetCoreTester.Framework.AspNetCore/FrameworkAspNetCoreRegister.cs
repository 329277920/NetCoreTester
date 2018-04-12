using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreTester.Framework.Configuration;
using System;
using System.Linq;

namespace NetCoreTester.Framework.AspNetCore
{
    /// <summary>
    /// 框架注册
    /// </summary>
    public static class FrameworkAspNetCoreRegister
    {
        public static IServiceCollection AddFramework(this IServiceCollection services)
        {
            // 注册配置
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration)service.ImplementationInstance;
            services.Configure<FrameworkConfig>(configuration);            

            // 注册一个单例服务
            services.AddSingleton(typeof(IHelper), typeof(Helper));
         
            return services;
        }
    }
}
