using OpenQA.Selenium;

namespace XUnitFirstSeleniumProject.third
{
    public class ToDoAppPage
    {
        private readonly DriverAdapter _driver;

        public ToDoAppPage(DriverAdapter driver) => _driver = driver;

        public void AssertLeftItems(int expectedCount)
        {
            var resultSpan = _driver.FindElement(By.XPath("//footer/*/span | //footer/span"));
            if (expectedCount <= 0)
            {
                _driver.ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                _driver.ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
        }

        public IWebElement GetItemCheckBox(string todoItem)
        {
            return _driver.FindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        public void AddNewToDoItem(string todoItem)
        {
            var todoInput = _driver.FindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            todoInput.SendKeys(Keys.Enter);
        }
    }
}
