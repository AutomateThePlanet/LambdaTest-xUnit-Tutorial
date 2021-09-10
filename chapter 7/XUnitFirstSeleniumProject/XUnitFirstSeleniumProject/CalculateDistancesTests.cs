using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;
using XUnitGeolocationTests;

namespace XUnitFirstSeleniumProject
{
    public class CalculateDistancesTests
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 90;
        private WebDriverWait _webDriverWait;
        private IWebDriver _driver;
        private bool _passed = true;

        [Theory]
        [InlineData("Germany", "DE", "Berlin", "1320.41 km / 820.47 mi")]
        [InlineData("Argentina", "AR", "Buenos Aires", "11951.47 km / 7426.3 mi")]
        [InlineData("Australia", "AU", "Canberra", "15355.98 km / 9541.76 mi")]
        [InlineData("Canada", "CA", "Ottawa", "7379.06 km / 4585.14 mi")]
        [InlineData("Japan", "JP", "Tokyo", "9188.49 km / 5709.46 mi")]
        [InlineData("Taiwan", "TW", "Taipei City", "8789.28 km / 5461.41 mi")]
        [InlineData("Norway", "NO", "Oslo", "2098.7 km / 1304.07 mi")]
        [InlineData("South Africa", "ZA", "Cape Town", "8544.51 km / 5309.32 mi")]
        public void VerifyDistance(string country, string countryCode, string capital, string expectedDistance)
        {
            try
            {
                string testName = $"lambda-name=Verify Distance Bulgaria - {country}";
                InitializeDriver(testName, countryCode);
                ((IJavaScriptExecutor)_driver).ExecuteScript(testName);

                _driver.Navigate().GoToUrl("https://www.gps-coordinates.net/");

                _driver.Manage().Cookies.AddCookie(new Cookie("cookieconsent_dismissed", "yes"));
                _driver.Navigate().Refresh();

                var map = WaitAndFindElement(By.XPath("//*[@id='map_canvas']/div/div"));
                Assert.True(map.Displayed);

                WaitUntilPageLoadsCompletely();
                var addressTitle = WaitAndFindElement(By.XPath("//*[@id='iwtitle']"));
                string address = addressTitle.Text;
                string currentAddress = $"{capital}, {address.Split(",").Last()}";

                _driver.Navigate().GoToUrl("https://www.gps-coordinates.net/distance");

                var firstDistanceAddressInput = WaitAndFindElement(By.Id("address1"));
                firstDistanceAddressInput.SendKeys(currentAddress);

                var secondDistanceAddressInput = WaitAndFindElement(By.Id("address2"));
                secondDistanceAddressInput.SendKeys("Sofia, Bulgaria");

                var calculateDistanceButton = WaitAndFindElement(By.XPath("//button[text()='Calculate the distance']"));
                calculateDistanceButton.Click();

                var distanceSpan = WaitAndFindElement(By.Id("distance"));

                ValidateInnerTextIs(distanceSpan, expectedDistance);
            }
            catch (Exception ex)
            {
                _passed = false;
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-exceptions", new List<string>() { ex.Message, ex.StackTrace });
                throw;
            }
            finally
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-status=" + (_passed ? "passed" : "failed"));
                _driver.Quit();
            }
        }

        [RepeatFact(TimesToRepeat = 4)]
        public void VerifyCalculatedDistance_When_CountryGermany()
        {
            try
            {
                string testName = $"lambda-name=Verify Distance Bulgaria - Germany";
                InitializeDriver(testName, "DE");

                _driver.Navigate().GoToUrl("https://www.gps-coordinates.net/");
                var map = WaitAndFindElement(By.Id("map_canvas"));
                Assert.True(map.Displayed);

                WaitUntilPageLoadsCompletely();
                var addressTitle = WaitAndFindElement(By.XPath("//*[@id='iwtitle']"));
                string address = addressTitle.Text;
                string currentAddress = $"Berlin, {address.Split(",").Last()}";

                _driver.Navigate().GoToUrl("https://www.gps-coordinates.net/distance");

                var firstDistanceAddressInput = WaitAndFindElement(By.Id("address1"));
                firstDistanceAddressInput.SendKeys(currentAddress);

                var secondDistanceAddressInput = WaitAndFindElement(By.Id("address2"));
                secondDistanceAddressInput.SendKeys("Sofia, Bulgaria");

                var calculateDistanceButton = WaitAndFindElement(By.XPath("//button[text()='Calculate the distance']"));
                calculateDistanceButton.Click();

                var distanceSpan = WaitAndFindElement(By.Id("distance"));

                ValidateInnerTextIs(distanceSpan, "1320.41 km / 820.47 mi");
            }
            catch (Exception ex)
            {
                _passed = false;
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-exceptions", new List<string>() { ex.Message, ex.StackTrace });
                throw;
            }
            finally
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-status=" + (_passed ? "passed" : "failed"));
                _driver.Quit();
            }
        }

        private void InitializeDriver(string testName, string countryCode)
        {
            string userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
            string accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
            var options = new ChromeOptions();
            options.BrowserVersion = "91.0";
            options.AddAdditionalCapability("user", userName, true);
            options.AddAdditionalCapability("accessKey", accessKey, true);
            options.AddAdditionalCapability("build", "GEOLOCATION TESTING", true);
            options.AddAdditionalCapability("name", testName, true);
            options.PlatformName = "Windows 10";

            options.AddAdditionalCapability("console", true, true);
            options.AddAdditionalCapability("network", true, true);

            options.AddAdditionalCapability("video", true, true);
            options.AddAdditionalCapability("visual", true, true);

            options.AddAdditionalCapability("javascriptEnabled", true, true);
            options.AddAdditionalCapability("acceptSslCerts", true, true);
            options.AddAdditionalCapability("unexpectedAlertBehaviour", "accept", true);

            options.AddAdditionalCapability("geoLocation", countryCode, true);

            options.AddAdditionalCapability("lambdaMaskCommands", new string[] { "setValues", "setCookies", "getCookies" }, true);

            _driver = new RemoteWebDriver(new Uri($"https://{userName}:{accessKey}@hub.lambdatest.com/wd/hub"), options);
            _driver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }
       

        private void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            try
            {
                _webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Expected distance was not {expectedText} but was {resultSpan.Text}");
            }
        }

        private IWebElement WaitAndFindElement(By locator)
        {
            var element = _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
            ScrollToVisible(element);
            _driver.Highlight(element);
            return element;
        }

        private void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)_driver;
            _webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
            _webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        private void ScrollToVisible(IWebElement element)
        {
            try
            {
                // Versions 1
                ////var actions = new Actions(_driver);
                ////actions.MoveToElement(element).Perform();

                // Versions 2
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (ElementNotInteractableException)
            {
                // ignore
            }
        }
    }
}
