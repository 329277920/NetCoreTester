using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester.Services
{
    public interface ITraceService
    {
        void Debug(string content);
    }
}
