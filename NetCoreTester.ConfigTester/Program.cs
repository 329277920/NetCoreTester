using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

 

namespace NetCoreTester.ConfigTester
{
    // 包
    // Install-Package Microsoft.Extensions.Configuration
    // Install-Package Microsoft.Extensions.Configuration.Json
    // Install-Package Microsoft.Extensions.Configuration.Xml
    // Install-Package Microsoft.Extensions.Options.ConfigurationExtensions
    // Install-Package Microsoft.Extensions.DependencyInjection
    public class MYY
    {
        public string Name { get; set; }
    }

    class Program
    {
        private static bool IsCustomType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }

        private static void TestTry()
        {
            try
            {
                return;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Console.WriteLine("OOOO");                
            }
        }

        private static void CopyTest()
        {
            byte[] b1 = new byte[4];
            byte[] b2 = new byte[] { 1, 2, 3 };

            b2.CopyTo(b1, 1);
        }

        static void Main(string[] args)
        {
            // Demo2();
            // Demo3();
            // Demo4();
            // Demo5();
            // Demo6();     

            // 定义一个服务容器
            var services = new ServiceCollection();

            // 定义一个json配置源，并捕获变更
            var source = new JsonConfigurationSource()
            {
                ReloadOnChange = true,
                ReloadDelay = 100,
                OnLoadException = (exceptionContext) =>
                    Console.WriteLine(exceptionContext.Exception.Message),
                Path = "config.json"                 
            };          
                      
            var config = new ConfigurationBuilder().Add(source).Build();

            using (config.GetReloadToken().RegisterChangeCallback((state) =>
            {
                var refConfig = state as IConfiguration;

                var regConfig = refConfig.Get<ConfigInfo>();

                services.AddSingleton(regConfig);

            }, config)) { }
               

            // 注册一个继承了 IOptions<> 接口的成员，OptionsManager<>,用于读取配置
            // services.AddOptions();

            // 将配置信息绑定到 ConfigInfo
            // services.Configure<ConfigInfo>(config);

            var serviceProvider = services.BuildServiceProvider();

            while (true)
            {
                // Console.WriteLine(serviceProvider.GetService<IOptions<ConfigInfo>>().Value.Version);
                Console.WriteLine(serviceProvider.GetService<ConfigInfo>().Version);
                Thread.Sleep(1000);
            }

            Console.ReadKey();
        }

        static void Demo4()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json").AddXmlFile("config.xml");
            var config = builder.Build();
            Console.WriteLine(config["version"]);
            Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[0]["Name"]);
            Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[1]["Name"]);
            Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[2]["Name"]);            
            for (var i = 0; i < 10; i++)
            {
                var info = config.Get<ConfigInfo>();
                Console.WriteLine(info.InstanceCount);
            }                      
            Console.ReadKey();
        }

        static void Demo6()
        {                            
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            var config = builder.Build();

            Console.ReadKey();

            
        }

        static void Demo5()
        {          
            var source = new JsonConfigurationSource()
            {
                Path = "config.json",
                ReloadDelay = 100,
                ReloadOnChange = true
            };
            var builder = new ConfigurationBuilder().Add(source);            
            var config = builder.Build();
            

            var services = new ServiceCollection();
            services.AddOptions();
            services.Configure<ConfigInfo>(config);
            var serviceProvider = services.BuildServiceProvider();
            for (var i = 0; i < 10; i++)
            {

            }
            serviceProvider.GetService<IOptions<ConfigInfo>>();
        }

        /// <summary>
        /// 演示IConfigurationBuilder、IConfiguration、IConfigurationSource的关系
        /// </summary>
        static void Demo1()
        {
            // 定义 IConfigurationSource
            var source = new JsonConfigurationSource() { Path = "config.json" };

            // 定义 IConfigurationBuilder
            var builder = new ConfigurationBuilder().Add(source);

            // 定义 IConfiguration
            var config = builder.Build();

            // 读取配置，配置名称不区分大小写
            Console.WriteLine(config["Version"]);
            Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[0]["Name"]);
            Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[1]["Name"]);

        }

        /// <summary>
        /// 演示IConfigurationBuilder、IConfiguration、IConfigurationSource的关系
        /// </summary>
        static void Demo2()
        {
            // 定义 IConfigurationSource json
            var source = new JsonConfigurationSource()
            {
                Path = "config.json",
                ReloadOnChange = true,
                ReloadDelay = 200
            };

            // 定义 IConfigurationSource xml
            var source2 = new XmlConfigurationSource() { Path = "config.xml" };

            // 定义 IConfigurationBuilder
            // var builder = new ConfigurationBuilder().Add(source).Add(source2);

            var builder = new ConfigurationBuilder().Add(source);

            // 定义 IConfiguration
            var config = builder.Build();

            var services = new ServiceCollection();

            Action changeCallBack = () =>
            {
                ConfigInfo options = services
         .AddOptions()
         .Configure<ConfigInfo>(config)
         .BuildServiceProvider()
         .GetService<IOptions<ConfigInfo>>()
         .Value;
                Console.WriteLine(options.Version);
            };

            var opts = services.BuildServiceProvider().GetService<IOptions<ConfigInfo>>().Value;

            ChangeToken.OnChange(() => config.GetReloadToken(), changeCallBack);




            //// 读取配置，配置名称不区分大小写
            //Console.WriteLine(config["Version"]);
            //Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[0]["Name"]);
            //Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[1]["Name"]);
            //// Console.WriteLine(config.GetSection("Items").GetChildren().ToArray()[2]["Name"]);
            //Console.WriteLine(config["Extend"]);

            // 从 IConfiguration 中创建类型  
            //ConfigInfo oldCfgInfo = null;
            //while (true)
            //{
            //    var cfgInfo = config.Get<ConfigInfo>();
            //    if (oldCfgInfo != cfgInfo)
            //    {
            //        oldCfgInfo = cfgInfo;
            //        Console.WriteLine("创建实例");
            //    }
            //    Console.WriteLine(cfgInfo.Version);
            //    Thread.Sleep(1000);
            //}           

            // 读取配置
            // Console.WriteLine(cfgInfo.Version);
        }

        static void Demo3()
        {
            // IServiceProvider
            // IServiceCollection
            // ServiceProvider
            var sc = new ServiceCollection();
            
            sc.AddSingleton<ConfigInfo, ConfigInfo>();
            var bp = sc.BuildServiceProvider().GetService<IServiceProvider>();

            sc.AddSingleton<ConfigItemInfo, ConfigItemInfo>();

            var info = bp.GetService<ConfigInfo>();

            var bp2 = sc.BuildServiceProvider().GetService<IServiceProvider>();

            var info3 = bp2.GetService<ConfigInfo>();
            var info4 = bp2.GetService<ConfigItemInfo>();

            info.Version = "cnf";
            var info2 = bp2.GetService<ConfigInfo>();
            var info5 = bp.GetService<ConfigInfo>();

            

        }
    }
}
