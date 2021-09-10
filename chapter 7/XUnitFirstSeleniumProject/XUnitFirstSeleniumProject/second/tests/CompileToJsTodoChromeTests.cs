using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace XUnitFirstSeleniumProject.second
{
    public class CompileToJsTodoChromeTests : IClassFixture<ChromeDriverFixture>
    {
        private readonly ChromeDriverFixture _fixture;

        public CompileToJsTodoChromeTests(ChromeDriverFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("Closure")]
        [InlineData("Dart")]
        [InlineData("Elm")]
        [InlineData("cujoJS")]
        [InlineData("Spine")]
        [InlineData("Angular 2.0")]
        [InlineData("Mithril")]
        [InlineData("Kotlin + React")]
        [InlineData("Firebase + AngularJS")]
        [InlineData("Vanilla ES6")]
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
