using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Xunit;

namespace XUnitFirstSeleniumProject
{
    public class FirstSeleniumTests : IDisposable
    {
        private IWebDriver _driver;

        public FirstSeleniumTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [SkippableFact(typeof(DriverServiceNotFoundException))]
        [TestPriority(0)]
        public void TitleIsEqualToSamplePage_When_NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            _driver.Manage().Window.Maximize();

            Assert.Equal("Sample page - lambdatest.com", _driver.Title);
        }

        [Fact]
        [TestPriority(1)]
        [Trait("Category", "CI")]
        [Trait("Priority", "1")]
        ////[UseCulture("bg-BG")]
        public void AddNewBirthDayItem()
        {
            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            _driver.Manage().Window.Maximize();

            IWebElement newItemInput = _driver.FindElement(By.Id("sampletodotext"));
            var birthdaydate = new DateTime(1990, 10, 20);
            newItemInput.SendKeys(birthdaydate.ToString("d"));

            var addButton = _driver.FindElement(By.Id("addbutton"));
            addButton.Click();

            var checkBoxesOptions = _driver.FindElements(By.XPath("//li[@ng-repeat]"));

            checkBoxesOptions.Last().Click();

            var optionsText = _driver.FindElements(By.XPath("//li[@ng-repeat]/span"));

            // us 10/20/1990
            // bg 20.10.1990 г.
            Assert.Equal("10/20/1990", optionsText.Last().Text);
        }
    }
}
