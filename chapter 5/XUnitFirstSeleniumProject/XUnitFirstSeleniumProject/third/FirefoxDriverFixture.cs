using System;

namespace XUnitFirstSeleniumProject.third
{
    public class FirefoxDriverFixture : DriverFixture
    {
        public override int WaitForElementTimeout => 60;

        protected override void IntializeDriver()
        {
            Driver.Value.Start(BrowserType.Firefox);
        }
    }
}
