using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.LogTest.Loggers.ExceptionLess
{
    public static class ExceptionLessExtensions
    {
        public static ILoggerFactory AddExceptionLess(this ILoggerFactory factory, string serverUrl, string apiKey, Func<string, LogLevel, bool> filter = null)
        {
            factory.AddProvider(new LoggerProvider(filter, apiKey, serverUrl));
            return factory;
        }

        public static ILoggingBuilder AddExceptionLess(this ILoggingBuilder builder, string serverUrl, string apiKey, Func<string, LogLevel, bool> filter = null)
        {
            return builder.AddProvider(new LoggerProvider(filter, apiKey, serverUrl));            
        }
    }
}
