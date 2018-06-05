using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.LogTest.Loggers.ExceptionLess
{
    public class LoggerProvider : ILoggerProvider
    {
        private Func<string, LogLevel, bool> _filter;
        private string _apiKey;
        private string _serverUrl;
        public LoggerProvider(Func<string, LogLevel, bool> filter, string apiKey, string serverUrl)
        {
            _filter = filter;
            _apiKey = apiKey;
            _serverUrl = serverUrl;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName, _apiKey, _serverUrl, _filter);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
