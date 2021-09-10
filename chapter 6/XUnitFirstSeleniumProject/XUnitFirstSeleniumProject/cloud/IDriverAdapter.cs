using System;
using OpenQA.Selenium;

namespace XUnitFirstSeleniumProject.cloud
{
    public interface IDriverAdapter : IDisposable
    {
        IWebElement FindElement(By locator);
        void GoToUrl(string url);
        void Start(BrowserType browserType, string testName = "");
        void ValidateInnerTextIs(IWebElement resultSpan, string expectedText);
        void ExecuteScript(string script, params object[] args);
    }
}