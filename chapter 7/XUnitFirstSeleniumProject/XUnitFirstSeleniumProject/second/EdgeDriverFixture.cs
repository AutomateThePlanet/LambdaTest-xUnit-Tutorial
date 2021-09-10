using System;

namespace XUnitFirstSeleniumProject.second
{
    public class EdgeDriverFixture : DriverFixture
    {
        public override int WaitForElementTimeout => 20;

        protected override void IntializeDriver()
        {
            Driver.Start(BrowserType.Edge);
        }
    }
}
