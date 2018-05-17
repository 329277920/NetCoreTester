using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.DependencyInjectionTester
{
    public class TransientService : ITransientService
    {
        private static int _instances;

        public TransientService() => ++_instances;

        public int InstanceCount => _instances;
    }
}
