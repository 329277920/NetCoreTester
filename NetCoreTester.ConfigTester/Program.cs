using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            var ss = new Regex(@"^/goods/m/recommend\?provinceid=[0-9]+&cityid=[0-9]+$").IsMatch("/goods/m/recommend?provinceid=10&cityid=60");

            TestTry();

            Console.ReadKey();

            CopyTest();

            JObject jobj = new JObject(new MYY() { Name = "CNF" });

            Console.WriteLine(IsCustomType("1".GetType()));
            Console.WriteLine(IsCustomType(1.GetType()));
            Console.WriteLine(IsCustomType(typeof(MYY)));

            Console.ReadKey();
            // Demo2();
            Demo3();
            Console.ReadKey();

            
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
