using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Xunit;
using XUnitFirstSeleniumProject.first;

//[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]
namespace XUnitFirstSeleniumProject.second
{
    public class PureJsTodoChromeTests : IClassFixture<ChromeDriverFixture>
    {
        private readonly DriverFixture _fixture;

        public PureJsTodoChromeTests(ChromeDriverFixture fixture)
        {
            _fixture = fixture;
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
        [InlineData("Vanilla JS")]
        [InlineData("jQuery")]
        [InlineData("Dojo")]   
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            _fixture.Driver.GoToUrl("https://todomvc.com/");
            OpenTechnologyApp(technology);
            AddNewToDoItem("Clean the car");
            AddNewToDoItem("Clean the house");
            AddNewToDoItem("Buy Ketchup");
            GetItemCheckBox("Buy Ketchup").Click();
            AssertLeftItems(2);
        }

        private void AssertLeftItems(int expectedCount)
        {
            var resultSpan = _fixture.Driver.FindElement(By.XPath("//footer/*/span | //footer/span"));
            if (expectedCount <= 0)
            {
                _fixture.Driver.ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                _fixture.Driver.ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
        }

        private IWebElement GetItemCheckBox(string todoItem)
        {
            return _fixture.Driver.FindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        private void AddNewToDoItem(string todoItem)
        {
            var todoInput = _fixture.Driver.FindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            todoInput.SendKeys(Keys.Enter);
        }

        private void OpenTechnologyApp(string name)
        {
            var technologyLink = _fixture.Driver.FindElement(By.LinkText(name));
            technologyLink.Click();
        }
    }
}
