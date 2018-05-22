using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTester.StartupTester
{
    public class AppSettings
    {
        public AppSettings() {
            ++_instanceCount;
        }
        private static int _instanceCount;
        public int InstanceCount => _instanceCount;
        public string Version { get; set; }
    }
}
