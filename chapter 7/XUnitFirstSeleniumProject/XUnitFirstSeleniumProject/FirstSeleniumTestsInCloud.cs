using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit;

namespace XUnitFirstSeleniumProject
{
    public class FirstSeleniumTestsInCloud : IDisposable
    {
        private IWebDriver _driver;
        private bool _passed = true;

        public FirstSeleniumTestsInCloud()
        {
            ////new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            ////_driver = new ChromeDriver();
            string userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
            string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
            var options = new ChromeOptions();
            options.BrowserVersion = "91.0";
            options.AddAdditionalCapability("user", userName, true);
            options.AddAdditionalCapability("accessKey", accessKey, true);
            options.AddAdditionalCapability("build", "FirstSeleniumTestsInCloud", true);
            options.AddAdditionalCapability("name", "AddNewBirthDayItem", true);
            options.PlatformName = "Windows 10";

            options.AddAdditionalCapability("selenium_version", "3.13.0", true);
            options.AddAdditionalCapability("console", true, true);
            options.AddAdditionalCapability("network", true, true);
            options.AddAdditionalCapability("timezone", "UTC+03:00", true);


            _driver = new RemoteWebDriver(new Uri($"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub"), options);
            _driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Fact]
        [TestPriority(1)]
        [Trait("Category", "CI")]
        [Trait("Priority", "1")]
        ////[UseCulture("bg-BG")]
        public void AddNewBirthDayItem()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                _passed = false;
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-exceptions", new List<string>() { ex.Message, ex.StackTrace });
                throw;
            }
            finally
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-status=" + (_passed ? "passed" : "failed"));
            }
        }
    }
}
