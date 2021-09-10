using System;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace XUnitFirstSeleniumProject
{
    public class DriverAdapter : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private IWebDriver _driver;
        private WebDriverWait _webDriverWait;

        public void Start(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);
                    _driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest);
                    _driver = new FirefoxDriver();
                    break;
                case BrowserType.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.Latest);
                    _driver = new EdgeDriver();
                    break;
                case BrowserType.Opera:
                    new DriverManager().SetUpDriver(new OperaConfig(), VersionResolveStrategy.Latest);
                    _driver = new OperaDriver();
                    break;
                case BrowserType.Safari:
                    _driver = new SafariDriver();
                    break;
            }

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

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
