using System.Threading;
using OpenQA.Selenium;

namespace XUnitFirstSeleniumProject.cloud
{
    public static class ToDoAppPage
    {
        public static ThreadLocal<IDriverAdapter> Driver { get; set; }

        public static void AssertLeftItems(int expectedCount)
        {
            var resultSpan = Driver.Value.FindElement(By.XPath("//footer/*/span | //footer/span"));
            if (expectedCount <= 0)
            {
                Driver.Value.ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                Driver.Value.ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
        }

        public static IWebElement GetItemCheckBox(string todoItem)
        {
            return Driver.Value.FindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        public static void AddNewToDoItem(string todoItem)
        {
            var todoInput = Driver.Value.FindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            todoInput.SendKeys(Keys.Enter);
        }
    }
}
