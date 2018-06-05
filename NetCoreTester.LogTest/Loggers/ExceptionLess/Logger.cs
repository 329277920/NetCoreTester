using Exceptionless;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.LogTest.Loggers.ExceptionLess
{
    public class Logger : ILogger
    {
        private Func<string, LogLevel, bool> _filter;
        private string _name;
        private ExceptionlessClient _client;
        internal Logger(string categoryName, string apiKey, string serverUrl, Func<string, LogLevel, bool> filter)
        {
            _name = categoryName;
            _filter = filter;
            _client = new ExceptionlessClient(configure => 
            {
                configure.ApiKey = apiKey;
                configure.ServerUrl = serverUrl;
            });
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this._filter == null || this._filter(this._name, logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }
            var builder = exception != null ? _client.CreateException(exception)
                : _client.CreateEvent().SetType(Exceptionless.Models.Event.KnownTypes.Log);
            
            builder.SetMessage(formatter(state, exception));
            builder.SetProperty(Exceptionless.Models.Event.KnownDataKeys.Level, ConvertLogLevel(logLevel).ToString());
            builder.SetSource(_name);
              
            var logEntity = state as IExceptionLessLogEntity;
            if (logEntity != null)
            {
                if (logEntity.Data != null)
                {
                    builder.AddObject(logEntity.Data);
                }
                if (logEntity.Tags != null)
                {
                    builder.AddTags(logEntity.Tags);
                }
                if (logEntity.User != null)
                {
                    builder.SetUserIdentity(logEntity.User);
                }
            }
            builder.Submit();
        }

        /// <summary>
        /// 转换 ExceptionLess 支持的日志级别
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        private Exceptionless.Logging.LogLevel ConvertLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return Exceptionless.Logging.LogLevel.Fatal;
                case LogLevel.Debug:
                    return Exceptionless.Logging.LogLevel.Debug;
                case LogLevel.Error:
                    return Exceptionless.Logging.LogLevel.Error;
                case LogLevel.Information:
                    return Exceptionless.Logging.LogLevel.Info;
                case LogLevel.Trace:
                    return Exceptionless.Logging.LogLevel.Trace;
                case LogLevel.Warning:
                    return Exceptionless.Logging.LogLevel.Warn;
            }
            return Exceptionless.Logging.LogLevel.Other;
        }
    }
}
