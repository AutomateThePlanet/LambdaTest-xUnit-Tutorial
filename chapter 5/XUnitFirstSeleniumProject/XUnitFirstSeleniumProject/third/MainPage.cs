using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace XUnitFirstSeleniumProject.third
{
    public class MainPage
    {
        private readonly DriverAdapter _driver;

        public MainPage(DriverAdapter driver) => _driver = driver;

        public void Open() => _driver.GoToUrl("https://todomvc.com/");

        public void OpenTechnologyApp(string name)
        {
            var technologyLink = _driver.FindElement(By.LinkText(name));
            technologyLink.Click();
        }
    }
}
