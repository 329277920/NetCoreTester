
using System;
using Unity.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreTester.DependencyInjectionTester
{
    class Program
    {
        static void Main(string[] args)
        {            
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ISingletonService, SingletonService>();
            services.AddTransient<ITransientService, TransientService>();
            services.AddScoped<IScopedService, ScopedService>();

            IServiceProvider provider = services.BuildServiceProvider();

            // 创建第一个Scope
            var scope1 = provider.CreateScope();
            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine($"Scope1-Singleton:{scope1.ServiceProvider.GetService<ISingletonService>().InstanceCount}");
                Console.WriteLine($"Scope1-Transient:{scope1.ServiceProvider.GetService<ITransientService>().InstanceCount}");
                Console.WriteLine($"Scope1-Scoped:{scope1.ServiceProvider.GetService<IScopedService>().InstanceCount}");
            }
            Console.WriteLine("------------------------------------");
            // 创建第二个Scope
            var scope2 = provider.CreateScope();
            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine($"Scope2-Singleton:{scope2.ServiceProvider.GetService<ISingletonService>().InstanceCount}");
                Console.WriteLine($"Scope2-Transient:{scope2.ServiceProvider.GetService<ITransientService>().InstanceCount}");
                Console.WriteLine($"Scope2-Scoped:{scope2.ServiceProvider.GetService<IScopedService>().InstanceCount}");
            }

            Console.ReadKey();
        }         
    }
}
