using System;

namespace XUnitFirstSeleniumProject.third
{
    public class EdgeDriverFixture : DriverFixture
    {
        public override int WaitForElementTimeout => 20;

        protected override void IntializeDriver()
        {
            Driver.Value.Start(BrowserType.Edge);
        }
    }
}
