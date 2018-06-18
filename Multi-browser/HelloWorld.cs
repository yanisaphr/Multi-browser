using System;
using System.Drawing;
using Applitools.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Demos
{

    public class HelloWorld
    {
        public static void Main(string[] args)
        {
            // Open a Chrome browser.
            var driver = new ChromeDriver();

            // Initialize the eyes SDK and set your private API key.
            var eyes = new Eyes();
            eyes.ApiKey = "hTP9KIw4l9LKUGxlQdod46b4vRRvOaF8UfGOs1RmYyo110";
            eyes.ForceFullPageScreenshot = true;
            eyes.SaveNewTests = true; 
 
            try
            {
                // Start the test and set the browser's viewport size to 800x600.
                eyes.Open(driver, "Hello World!", "My first Selenium C# test!", new Size(800, 600));

                // Navigate the browser to the "hello world!" web-site.
                driver.Url = "https://www.agoda.com?expuser=B";

                // Visual checkpoint #1.
                eyes.CheckWindow("RESORT");

                // Click the "Click me!" button.
                driver.FindElement(By.XPath("//*[@data-selenium='homesTab']")).Click();

                // Visual checkpoint #2.
                eyes.CheckWindow("RESORT");

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
    }
}