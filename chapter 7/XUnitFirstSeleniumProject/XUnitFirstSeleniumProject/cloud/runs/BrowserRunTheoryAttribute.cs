using System;
using Xunit;
using Xunit.Sdk;

namespace XUnitFirstSeleniumProject.cloud
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [XunitTestCaseDiscoverer("XUnitFirstSeleniumProject.cloud.BrowserRunTheoryDiscoverer", "XUnitFirstSeleniumProject")]
    public class BrowserRunTheoryAttribute : TheoryAttribute
    {
    }
}