using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace XUnitFirstSeleniumProject.Version3
{
    public partial class ToDoPage : WebPage
    {
        public ToDoPage(IWebDriver driver, WebDriverWait wait, Actions actions)
          : base(driver, wait, actions)
        {
        }

        protected override string Url => "https://todomvc.com/";

        public void AddNewToDoItem(string todoItem)
        {
            ToDoInput.SendKeys(todoItem);
            Actions.Click(ToDoInput).SendKeys(Keys.Enter).Perform();
        }

        public void CheckItem(string itemName)
        {
            GetItemCheckBox(itemName).Click();
        }   
    }
}
