using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester.Services
{
    public class TraceService : ITraceService, IDisposable
    {
        private static readonly FileStream _fs;

        static TraceService()
        {
            _fs = new FileStream("log.log", FileMode.Append, FileAccess.Write, FileShare.Read);            
        }

        private StringBuilder _traceBuffer;
        private string _traceId;

        public TraceService()
        {
            _traceBuffer = new StringBuilder();
            _traceId = Guid.NewGuid().ToString().Replace("-", "");
        }

        public void Debug(string content)
        {
            _traceBuffer.Append($"{content}\r\n");
        }

        public void Dispose()
        {
            if (_traceBuffer != null)
            {
                lock (_fs)
                {
                    var buffer = Encoding.Default.GetBytes($"{_traceId}\r\n{_traceBuffer.ToString()}");
                    _fs.Write(buffer, 0, buffer.Length);
                    _fs.Flush();
                }
                _traceBuffer = null;
            }
        }
    }
}
