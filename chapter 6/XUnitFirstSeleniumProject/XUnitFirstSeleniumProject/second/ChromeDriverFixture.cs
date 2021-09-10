using System;

namespace XUnitFirstSeleniumProject.second
{
    public class ChromeDriverFixture : DriverFixture
    {
        public override int WaitForElementTimeout => 40;

        protected override void IntializeDriver()
        {
            Driver.Start(BrowserType.Chrome);
        }
    }
}
