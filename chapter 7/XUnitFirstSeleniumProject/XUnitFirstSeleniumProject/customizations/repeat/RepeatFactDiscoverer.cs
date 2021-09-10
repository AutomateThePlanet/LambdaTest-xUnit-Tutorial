using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitFirstSeleniumProject.repeat
{
    public class RepeatFactDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;

        public RepeatFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var timesToRepeat = factAttribute.GetNamedArgument<int>("TimesToRepeat");
            if (timesToRepeat < 1)
            {
                timesToRepeat = 3;
            }

            yield return new RepeatTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod, timesToRepeat);
        }
    }
}
