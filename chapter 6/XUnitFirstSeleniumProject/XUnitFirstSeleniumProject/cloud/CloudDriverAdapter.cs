using System;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace XUnitFirstSeleniumProject.cloud
{
    public class CloudDriverAdapter : IDriverAdapter
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private IWebDriver _driver;
        private WebDriverWait _webDriverWait;

        public void Start(BrowserType browserType, string testName = "")
        {
            string userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
            string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
            dynamic options = default(ChromeOptions);

            switch (browserType)
            {
                case BrowserType.Chrome:
                    options = new ChromeOptions();
                    options.BrowserVersion = "91.0";
                    break;
                case BrowserType.Firefox:
                    options = new FirefoxOptions();
                    options.BrowserVersion = "90.0";
                    break;
                case BrowserType.Edge:
                    options = new EdgeOptions();
                    break;
                case BrowserType.Opera:
                    options = new OperaOptions();
                    break;
                case BrowserType.Safari:
                    options = new SafariOptions();
                    break;
            }

            options.AddAdditionalCapability("user", userName, true);
            options.AddAdditionalCapability("accessKey", accessKey, true);
            options.AddAdditionalCapability("build", Guid.NewGuid().ToString(), true);
            options.AddAdditionalCapability("name", testName, true);
            options.PlatformName = Settings.GetExecutionSettings().PlatformName;

            options.AddAdditionalCapability("selenium_version", "3.13.0", true);
            options.AddAdditionalCapability("console", true, true);
            options.AddAdditionalCapability("network", true, true);
            options.AddAdditionalCapability("timezone", "UTC+03:00", true);
           

            _driver = new RemoteWebDriver(new Uri($"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub"), options);
            _driver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            _webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
        }

        public IWebElement FindElement(By locator)
        {
            return _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }

        public void ExecuteScript(string script, params object[] args)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)_driver;
            javaScriptExecutor.ExecuteScript(script, args);
        }

        public void Dispose()
        {
            _driver?.Quit();
        }
    }
}
