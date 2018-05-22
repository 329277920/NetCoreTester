using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.ConfigTester
{
    public class ConfigInfo
    {
        private static int _instanceCount = 0;
        public ConfigInfo() { ++_instanceCount; }
        public int InstanceCount => _instanceCount;
        public string Version { get; set; }
        public List<ConfigItemInfo> Items { get; set; }        
        public int Extend { get; set; }
    }
}
