using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreTester.DependencyInjectionTester
{
    public interface ISingletonService
    {
        int InstanceCount { get; }
    }
}
