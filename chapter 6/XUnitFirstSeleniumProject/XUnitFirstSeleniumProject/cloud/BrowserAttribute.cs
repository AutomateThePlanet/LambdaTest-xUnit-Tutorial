using System;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using Xunit.Sdk;

namespace XUnitFirstSeleniumProject.cloud
{
    public class BrowserAttribute : BeforeAfterTestAttribute
    {
        public BrowserAttribute(BrowserType browser) => Browser = browser;

        public BrowserType Browser { get; set; } = BrowserType.Chrome;

        public override void Before(MethodInfo methodUnderTest)
        {
            DriverFixture.Driver.Value?.Dispose();
            DriverFixture.Driver.Value?.Start(Browser, methodUnderTest.Name);
            MainPage.Driver = DriverFixture.Driver;
            ToDoAppPage.Driver = DriverFixture.Driver;
        }

        ////public override void After(MethodInfo methodUnderTest)
        ////{
        ////    DriverFixture.Driver.Value.Dispose();
        ////}
    }
}
