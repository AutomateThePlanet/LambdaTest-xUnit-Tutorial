using System;
using System.Linq;
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
    public class SettingsCloudDriverAdapter : IDriverAdapter
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
                    options.BrowserVersion = Settings.GetExecutionSettings().BrowserVersion;
                    break;
                case BrowserType.Firefox:
                    options = new FirefoxOptions();
                    options.BrowserVersion = Settings.GetExecutionSettings().BrowserVersion;
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

            var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";
            options.AddAdditionalCapability("build", timestamp, true);
            options.AddAdditionalCapability("name", testName, true);
            options.PlatformName = Settings.GetExecutionSettings().PlatformName;

            InitializeGridOptionsFromConfiguration(options);

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

        private void InitializeGridOptionsFromConfiguration(dynamic options)
        {
            if (Settings.GetExecutionSettings().Arguments == null)
            {
                return;
            }

            if (Settings.GetExecutionSettings().Arguments.Any())
            {
                foreach (var item in Settings.GetExecutionSettings().Arguments[0])
                {
                    if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                    {
                        options.AddAdditionalCapability(item.Key, FormatGridOptions(item.Value), true);
                    }
                }
            }
        }

        private dynamic FormatGridOptions(string option)
        {
            if (bool.TryParse(option, out bool result))
            {
                return result;
            }
            else if (int.TryParse(option, out int resultNumber))
            {
                return resultNumber;
            }
            else if (double.TryParse(option, out double resultRealNumber))
            {
                return resultRealNumber;
            }
            else
            {
                return option;
            }
        }
    }
}
