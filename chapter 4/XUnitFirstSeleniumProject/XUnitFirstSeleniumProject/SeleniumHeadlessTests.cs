using System;
using System.ComponentModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Xunit;

namespace XUnitFirstSeleniumProject
{
    public class SeleniumHeadlessTests
    {
        [Fact]
        [Category("CI")]
        ////[RetryFact(MaxRetries = 2)]
        public void TitleIsEqualToSamplePage_When_NavigateToHomePage1()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            // C# 8.0
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            using var _driver = new ChromeDriver(chromeOptions);

            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");
            _driver.Manage().Window.Maximize();

            Assert.Equal("Sample page - lambdatest.com1", _driver.Title);
        }
    }
}
