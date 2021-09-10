using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Xunit;

namespace SeleniumTestProject
{
    public class AttributesSeleniumTests : IDisposable
    {
        private IWebDriver _driver;

        public AttributesSeleniumTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        ////[Fact]
        [Trait("Category", "CI")]
        [Trait("Priority", "1")]
        [RetryFact(MaxRetries = 2)]
        [UseCulture("bg-BG")]
        [TestPriority(0)]
        public void ProperCheckboxSelected()
        {
            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            IWebElement todoInput = _driver.FindElement(By.Id("sampletodotext"));

            DateTime birthDay = new DateTime(1990, 10, 20);
            // us 10/20/1990
            todoInput.SendKeys(birthDay.ToString("d"));

            var addButton = _driver.FindElement(By.Id("addbutton"));
            addButton.Click();

            var todoCheckboxes = _driver.FindElements(By.XPath("//li[@ng-repeat]/input"));

            todoCheckboxes.Last().Click();

            var todoInfos = _driver.FindElements(By.XPath("//li[@ng-repeat]/span"));

            Assert.Equal("20.10.1990 г.", todoInfos.Last().Text);
        }

        [Trait("Category", "CI")]
        [Trait("Priority", "1")]
        [RetryFact(MaxRetries = 2)]
        [UseCulture("bg-BG")]
        [TestPriority(1)]
        public void SecondTest()
        {
            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            IWebElement todoInput = _driver.FindElement(By.Id("sampletodotext"));

            DateTime birthDay = new DateTime(1990, 10, 20);
            // us 10/20/1990
            todoInput.SendKeys(birthDay.ToString("d"));

            var addButton = _driver.FindElement(By.Id("addbutton"));
            addButton.Click();

            var todoCheckboxes = _driver.FindElements(By.XPath("//li[@ng-repeat]/input"));

            todoCheckboxes.Last().Click();

            var todoInfos = _driver.FindElements(By.XPath("//li[@ng-repeat]/span"));

            Assert.Equal("20.10.1990 г.", todoInfos.Last().Text);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
