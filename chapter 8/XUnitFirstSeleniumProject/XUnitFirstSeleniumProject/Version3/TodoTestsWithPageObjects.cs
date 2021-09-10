using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Xunit;

namespace XUnitFirstSeleniumProject.Version3
{
    public class TodoTestsWithPageObjects : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly Actions _actions;
        private readonly HomePage _homePage;
        private readonly ToDoPage _todoPage;

        public TodoTestsWithPageObjects()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
            _actions = new Actions(_driver);
            _homePage = new HomePage(_driver, _webDriverWait, _actions);
            _todoPage = new ToDoPage(_driver, _webDriverWait, _actions);
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
            _homePage.GoTo();
            _homePage.OpenTechnologyApp(technology);
            _todoPage.AddNewToDoItem("Clean the car");
            _todoPage.AddNewToDoItem("Clean the house");
            _todoPage.AddNewToDoItem("Buy Ketchup");
            _todoPage.CheckItem("Buy Ketchup");
            _todoPage.AssertLeftItems(2);
        }
    }
}
