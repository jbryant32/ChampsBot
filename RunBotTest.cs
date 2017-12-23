using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace PhantomJSChamps
{
    [TestFixture]
    public class RunBotTest
    {
        PhantomJSDriver driver;
        WebDriverWait wait;
        int[] ScreenWidths = { 1024, 1920, 1366, 1280, 1600 };
        int[] ScreenHeights = { 768, 1080, 768, 1024, 900 };

        [SetUp]
        public void SetUp()
        {
            
            driver = new PhantomJSDriver(Directory.GetCurrentDirectory(),RunBot.Instance.GenerateRandomProfile());
            driver.Manage().Window.Size = new System.Drawing.Size(1024,768);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60.00));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        [Test]
        public void RunBotOnDOM()
        {

            RunBot.Instance.TestUnique(driver,wait);

            RunBot.Instance.DisplayResults(driver, wait);

            RunBot.Instance.SelectShoeSize(driver,wait);

            try
            {
                Screenshot sh = driver.GetScreenshot();
                sh.SaveAsFile(@"C:\screenShots\Temp.jpg", ScreenshotImageFormat.Png);
            }
            catch (Exception)
            {

                Console.WriteLine("Screen Shot Failed...");
            }
        
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
