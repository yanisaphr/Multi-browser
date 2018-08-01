using System;
using System.Drawing;
using Applitools.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Demos
{

    public static class HelloWorld
    {

        static IWebDriver driver;
        static string CSS_OLSB_SEARCH_BAR_PANEL = "[data-selenium=\"searchBox\"]";
        static string JS_INJ_FREEZE_SEARCH_BAR_OLSB = "$('" + CSS_OLSB_SEARCH_BAR_PANEL + "').css({'position':'static'});";

        public static void Main(string[] args)
        {
            // Open a Chrome browser.
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            // Initialize the eyes SDK and set your private API key.
            var eyes = new Eyes();
            eyes.ApiKey = "TOLqDb0rgpHlNt111OXiJjHSDfZbM109ijaEQEtUWvmxgTY110";
            eyes.ForceFullPageScreenshot = true;
            //eyes.SaveNewTests = true; 
            eyes.BaselineEnvName = "No stiky";
            eyes.MatchLevel = Applitools.MatchLevel.Layout;

            try
            {


                // Start the test and set the browser's viewport size to 800x600.
                eyes.Open(driver, "Hello World!", "No stiky");

                // Navigate the browser to the "hello world!" web-site.
                driver.Url = "https://www.agoda.com?expuser=A";

                InjectJavascript(driver, JS_INJ_FREEZE_SEARCH_BAR_OLSB);

                // Visual checkpoint #1.
                eyes.CheckRegion(By.CssSelector("[data-selenium='tile-container-left']"));
                eyes.InRegion(By.CssSelector("[data-selenium='tile-container-right']"));



                eyes.CheckWindow("RESORT");
               // Click the "Click me!" button.
                driver.FindElement(By.XPath("//*[@data-selenium='homesTab']")).Click();

                //// Visual checkpoint #2.
                //eyes.CheckWindow("RESORT");

                // End the test.
                eyes.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // Close the browser.
                driver.Quit();

                // If the test was aborted before eyes.Close was called, ends the test as aborted.
                eyes.AbortIfNotClosed();
            }
        }


        private static void InjectJavascript(this IWebDriver driver, string script)
        {
            try
            {
                var javaScriptExecutor = driver as IJavaScriptExecutor;
                if (javaScriptExecutor == null)
                    return;
                javaScriptExecutor.ExecuteScript(script);
            }
            catch
            {
                return;
            }
        }
    }
}