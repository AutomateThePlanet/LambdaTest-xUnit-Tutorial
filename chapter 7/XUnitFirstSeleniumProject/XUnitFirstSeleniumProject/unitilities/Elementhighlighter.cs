using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace XUnitGeolocationTests
{
    public static class Elementhighlighter
    {
        private static IJavaScriptExecutor _javaScriptExecutor;

        public static void Highlight(this IWebDriver driver, IWebElement nativeElement, int waitBeforeUnhighlightMiliSeconds = 100, string color = "yellow")
        {
            try
            {
                _javaScriptExecutor = (IJavaScriptExecutor)driver;
                var originalElementBorder = (string)_javaScriptExecutor.ExecuteScript("return arguments[0].style.background", nativeElement);
                
                _javaScriptExecutor.ExecuteScript($"arguments[0].style.background='{color}'; return;", nativeElement);
                if (waitBeforeUnhighlightMiliSeconds >= 0)
                {
                    if (waitBeforeUnhighlightMiliSeconds > 1000)
                    {
                        var backgroundWorker = new BackgroundWorker();
                        backgroundWorker.DoWork += (obj, e) => Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMiliSeconds);
                        backgroundWorker.RunWorkerAsync();
                    }
                    else
                    {
                        Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMiliSeconds);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
        private static void Unhighlight(IWebElement nativeElement, string border, int waitBeforeUnhighlightMiliSeconds)
        {
            try
            {
                Thread.Sleep(waitBeforeUnhighlightMiliSeconds);
                _javaScriptExecutor.ExecuteScript("arguments[0].style.background='" + border + "'; return;", nativeElement);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
