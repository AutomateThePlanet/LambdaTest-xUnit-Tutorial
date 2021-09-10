using System;

namespace XUnitFirstSeleniumProject.third
{
    public class ChromeDriverFixture : DriverFixture
    {
        public override int WaitForElementTimeout => 40;

        protected override void IntializeDriver()
        {
            Driver.Value.Start(BrowserType.Chrome);
        }
    }
}
