using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.ConfigTester
{
    public class ConfigInfo
    {
        public string Version { get; set; }
        public List<ConfigItemInfo> Items { get; set; }        
        public int Extend { get; set; }
    }
}
