﻿using System;

namespace XUnitFirstSeleniumProject.second
{
    public class FirefoxDriverFixture : DriverFixture
    {
        public override int WaitForElementTimeout => 60;

        protected override void IntializeDriver()
        {
            Driver.Start(BrowserType.Firefox);
        }
    }
}
