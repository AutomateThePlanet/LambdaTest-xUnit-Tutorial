using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace XUnitFirstSeleniumProject
{
    public class TodoTestsInCloud : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private readonly WebDriverWait _webDriverWait;
        private IWebDriver _driver;
        private bool _passed = true;
      

        public TodoTestsInCloud()
        {
            string userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
            string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
            var options = new ChromeOptions();
            options.BrowserVersion = "91.0";
            options.AddAdditionalCapability("user", userName, true);
            options.AddAdditionalCapability("accessKey", accessKey, true);
            options.AddAdditionalCapability("build", "TodoTestsInCloud", true);
            options.AddAdditionalCapability("name", "VerifyTodoListCreatedSuccessfully-TECH", true);
            options.PlatformName = "Windows 10";

            options.AddAdditionalCapability("selenium_version", "3.13.0", true);
            options.AddAdditionalCapability("console", true, true);
            options.AddAdditionalCapability("network", true, true);
            options.AddAdditionalCapability("timezone", "UTC+03:00", true);


            _driver = new RemoteWebDriver(new Uri($"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub"), options);
            _driver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Theory]
        [InlineData("Backbone.js")]
        [InlineData("AngularJS")]
        [InlineData("React")]
        [InlineData("Vue.js")]
        [InlineData("CanJS")]
        [InlineData("Ember.js")]
        [InlineData("KnockoutJS")]
        [InlineData("Marionette.js")]
        [InlineData("Polymer")]
        [InlineData("Angular 2.0")]
        [InlineData("Dart")]
        [InlineData("Elm")]
        [InlineData("Closure")]
        [InlineData("Vanilla JS")]
        [InlineData("jQuery")]
        [InlineData("cujoJS")]
        [InlineData("Spine")]
        [InlineData("Dojo")]
        [InlineData("Mithril")]
        [InlineData("Kotlin + React")]
        [InlineData("Firebase + AngularJS")]
        [InlineData("Vanilla ES6")]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            try
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript($"lambda-name=VerifyTodoListCreatedSuccessfully(string technology: {technology})");
                _driver.Navigate().GoToUrl("https://todomvc.com/");
                OpenTechnologyApp(technology);
                AddNewToDoItem("Clean the car");
                AddNewToDoItem("Clean the house");
                AddNewToDoItem("Buy Ketchup");
                GetItemCheckBox("Buy Ketchup").Click();
                AssertLeftItems(2);
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

        private void AssertLeftItems(int expectedCount)
        {
            var resultSpan = WaitAndFindElement(By.XPath("//footer/*/span | //footer/span"));
            if (expectedCount <= 0)
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
        }

        private void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            _webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
        }

        private IWebElement GetItemCheckBox(string todoItem)
        {
            return WaitAndFindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        private void AddNewToDoItem(string todoItem)
        {
            var todoInput = WaitAndFindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            todoInput.SendKeys(Keys.Enter);
        }

        private void OpenTechnologyApp(string name)
        {
            var technologyLink = WaitAndFindElement(By.LinkText(name));
            technologyLink.Click();
        }

        private IWebElement WaitAndFindElement(By locator)
        {
            return _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}
