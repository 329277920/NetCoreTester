using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.DependencyInjectionTester
{
    public class ScopedService : IScopedService
    {
        private static int _instances;

        public ScopedService() => ++_instances;

        public int InstanceCount => _instances;
    }
}
