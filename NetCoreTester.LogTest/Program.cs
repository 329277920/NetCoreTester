using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless.Models.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;

using NetCoreTester.LogTest.Loggers.ExceptionLess;

namespace NetCoreTester.LogTest
{
    public class Program
    {
        public class Test
        {
            public string Message { get; set; }

            public override string ToString()
            {
                return Message?.ToString();
            }
        }


        public static void Main(string[] args)
        {
            //var logger = new LoggerFactory().AddConsole(true).CreateLogger<Program>();

            //using (logger.BeginScope("订单: {ID}", "20160520001"))
            //{
            //    logger.Log(LogLevel.Information, 0, new Test() { Message = "cnf" }, null, (state, ex) => state.ToString());
            //    //logger.LogWarning("商品库存不足(商品ID: {0}, 当前库存:{1}, 订购数量:{2})", "9787121237812", 20, 50);
            //    //logger.LogError("商品ID录入错误(商品ID: {0})", "9787121235368");
            //}
            //Console.ReadKey();

            // 初始化日志工厂，提供Console，和ExceptionLess
            //var factory = new LoggerFactory()
            //    .AddExceptionLess("http://exceptionless.manjinba.cn",
            //    "gtoRHHuReUWfJDgfALpI4R6Zdy7pjtooIYv9IJfe")
            //    .AddLog4Net("log4net.config");
          
         
            //// 写入普通日志
            //var logger = factory.CreateLogger<Program>();           
            //logger.LogDebug("这是调试信息");
            //logger.LogInformation("这是提示信息");
            //logger.LogError(1, new Exception("报异常咯。"), "这是异常信息");

            //// 写入 ExceptionLess 扩展特性的日志
            //var logEntity = new LogEntity();
            //logEntity.Tags = new string[] { "tag1", "tag2" };
            //logEntity.User = new UserInfo() { Identity = "1", Name = "cnf" };
            //logEntity.Data = new { Msg = "这是 EceptionLess 的附加对象。" };

            //logEntity.Message = "这是调试信息";
            //logger.Log(LogLevel.Debug, 0, logEntity, null, LogEntity.Formatter);
            //logEntity.Message = "这是提示信息";
            //logger.Log(LogLevel.Information, 0, logEntity, null, LogEntity.Formatter);
            //logEntity.Message = "这是异常信息";
            //logger.Log(LogLevel.Error, 0, logEntity, new Exception("报异常咯。"), LogEntity.Formatter);
            //logEntity.Message = "这是异常信息，空异常";
            //logger.Log(LogLevel.Error, 0, logEntity, null, LogEntity.Formatter);

            //Console.ReadKey();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {           
            string[] addresses = null ;
            var host = WebHost.CreateDefaultBuilder(args)              
               .ConfigureAppConfiguration(configurationBuilder =>
               {
                   var strAddress = configurationBuilder.Build()["server:urls"];
                   if (!string.IsNullOrEmpty(strAddress))
                   {
                       addresses = strAddress.Split(new char[] { ',' },
                           StringSplitOptions.RemoveEmptyEntries);
                   }
               })
               .ConfigureLogging(loggingBuilder => 
               {
                   loggingBuilder.ClearProviders();
                   loggingBuilder.AddExceptionLess("http://exceptionless.manjinba.cn",
                   "gtoRHHuReUWfJDgfALpI4R6Zdy7pjtooIYv9IJfe")
                   // .AddConsole()                  
                    .AddLog4Net("log4net.config")
                    .AddFilter("System", LogLevel.Information);
               })
               .UseStartup<Startup>()
               .Build();
            
            var featureAddresses = host.ServerFeatures.Get<IServerAddressesFeature>().Addresses;
            if (addresses != null && addresses.Length > 0 && featureAddresses != null)
            {
                featureAddresses.Clear();
                foreach (var address in addresses)
                {
                    featureAddresses.Add(address);
                }
            }
            return host;            
        }                       
    }

    public class LogEntity : IExceptionLessLogEntity
    {
        public string[] Tags { get; set; }
        public UserInfo User { get; set; }        
        public object Data { get; set; }
        public string Message { get; set; }

        public static string Formatter(LogEntity logEntity, Exception ex)
        {
            return logEntity.Message;
        }
    }
}
