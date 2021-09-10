using Xunit;
using Xunit.Sdk;

namespace XUnitFirstSeleniumProject
{
    [XunitTestCaseDiscoverer("XUnitFirstSeleniumProject.repeat.RepeatFactDiscoverer", "XUnitFirstSeleniumProject")]
    public class RepeatFactAttribute : FactAttribute
    {
        public int TimesToRepeat { get; set; }
    }
}