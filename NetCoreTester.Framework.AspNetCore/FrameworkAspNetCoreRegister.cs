using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCoreTester.Framework.AspNetCore
{
    /// <summary>
    /// 框架注册
    /// </summary>
    public static class FrameworkAspNetCoreRegister
    {
        public static IServiceCollection AddFramework(this IServiceCollection services)
        {
            // 注册一个单例服务
            services.AddSingleton(typeof(IHelper), typeof(Helper));

            return services;
        }
    }
}
