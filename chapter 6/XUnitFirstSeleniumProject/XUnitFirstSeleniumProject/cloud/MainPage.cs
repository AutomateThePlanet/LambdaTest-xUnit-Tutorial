using System.Threading;
using OpenQA.Selenium;

namespace XUnitFirstSeleniumProject.cloud
{
    public static class MainPage
    {
        public static ThreadLocal<IDriverAdapter> Driver { get; set; }

        public static void Open() => Driver.Value.GoToUrl("https://todomvc.com/");

        public static void OpenTechnologyApp(string name)
        {
            var technologyLink = Driver.Value.FindElement(By.LinkText(name));
            technologyLink.Click();
        }
    }
}
