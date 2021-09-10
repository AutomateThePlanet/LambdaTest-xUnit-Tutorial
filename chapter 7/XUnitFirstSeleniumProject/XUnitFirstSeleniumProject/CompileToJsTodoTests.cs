using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Xunit;
using XUnitFirstSeleniumProject.first;

namespace XUnitFirstSeleniumProject
{
    public class CompileToJsTodoTests : IClassFixture<DriverFixture>
    {
        private readonly DriverFixture _fixture;

        public CompileToJsTodoTests(DriverFixture fixture)
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
            _fixture.Driver.Navigate().GoToUrl("https://todomvc.com/");
            OpenTechnologyApp(technology);
            AddNewToDoItem("Clean the car");
            AddNewToDoItem("Clean the house");
            AddNewToDoItem("Buy Ketchup");
            GetItemCheckBox("Buy Ketchup").Click();
            AssertLeftItems(2);
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
            _fixture.WebDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
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
            return _fixture.WebDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}
