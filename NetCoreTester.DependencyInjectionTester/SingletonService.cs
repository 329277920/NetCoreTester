using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.DependencyInjectionTester
{
    public class SingletonService : ISingletonService
    {
        private static int _instances;

        public SingletonService() => ++_instances;

        public int InstanceCount => _instances;
    }
}
