using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PhantomJSChamps
{
    public class RunBot
    {
        public static RunBot Instance { get { return new RunBot(); } }
        public List<PhantomJSOptions> Profiles { get; set; }

        public RunBot()
        {

            CreateProfiles();

        }

        /// <summary>
        /// init profiles
        /// </summary>
        public void CreateProfiles()
        {
            Profiles = new List<PhantomJSOptions>();
            PhantomJSOptions options1 = new PhantomJSOptions();
            options1.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
            options1.AddAdditionalCapability("phantomjs.page.customHeaders.Accept-Language", "en-US");
            options1.AddAdditionalCapability("phantomjs.page.customHeaders.Accept", "text/html, application/xhtml+xml, application/xml;q=0.9, */*;q=0.8");

            PhantomJSOptions options2 = new PhantomJSOptions();
            options2.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 5.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586");
            options2.AddAdditionalCapability("phantomjs.page.customHeaders.Accept-Language", "en-US");
            options2.AddAdditionalCapability("phantomjs.page.customHeaders.Accept", "text/html,application/xhtml+xml");

            PhantomJSOptions options3 = new PhantomJSOptions();
            options3.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.85 Safari/537.36");
            options3.AddAdditionalCapability("phantomjs.page.customHeaders.Accept-Language", "en-US");
            options3.AddAdditionalCapability("phantomjs.page.customHeaders.Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            Profiles.Add(options1);
            Profiles.Add(options2);
            Profiles.Add(options3);
        }

        /// <summary>
        /// Grab A random Profile For Setup
        /// </summary>
        /// <returns></returns>
        public PhantomJSOptions GenerateRandomProfile()
        {
            Random rand = new Random();
            var index = rand.Next(0, 2);
            return Profiles[index];//return a random profile
        }

        /// <summary>
        /// Use to Check Browser footprint
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public void TestUnique(PhantomJSDriver driver, WebDriverWait wait)
        {
            driver.Navigate().GoToUrl("https://amiunique.org/");

            var button = driver.FindElementById("link");

            button.Click();

            wait.Until((d) =>
            {
                return ExpectedConditions.ElementExists(By.Id("content"));
            });

            Thread.Sleep(TimeSpan.FromSeconds(60));

            driver.FindElementById("detBut").Click();
        }


        public string SetRandomProxy()
        {
            throw new NotImplementedException();
        }

        public void OpenUrl(PhantomJSDriver driver, string Url)
        {

            driver.Navigate().GoToUrl(Url);

            try
            {
                var searchInputBar = driver.FindElementById("keyword");//grab the pages search input
                Console.WriteLine("Begining Search");

                searchInputBar.Click();

                while (true)
                {
                   
                    //grab item to search for from the command pompt
                    Console.WriteLine("What Shoes Are You Looking For:");

                    var SearchFor = Console.ReadLine();
                    if (SearchFor == null || SearchFor == string.Empty)
                    {
                        Console.WriteLine("Invalid Search Type");
                    }

                    else
                    {

                        Console.WriteLine("Begin Search For " + SearchFor);
                        searchInputBar.SendKeys(SearchFor);//input search values
                        searchInputBar.SendKeys(Keys.Enter);//begin search

                        break;
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("We had a problem searching! " + ex.Message);
            }



        }

        /// <summary>
        /// get the results from the search and display them in the console
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public void DisplayResults(PhantomJSDriver driver, WebDriverWait wait)
        {
            try
            {
                Console.WriteLine("Input a Number to select Product:");

                var ResultsLinks = new List<IWebElement>();
                ResultsLinks.AddRange(driver.FindElementsByClassName("product_title"));

                int ProductIndex = 0;
                foreach (var Product in ResultsLinks)
                {

                    Console.WriteLine("Product " + ProductIndex + Product.Text);
                    ProductIndex++;
                }

                var ProductIndexSelected = Console.ReadLine();


                if (ProductIndexSelected != null || ProductIndexSelected != string.Empty)
                {
                    if (Int32.Parse(ProductIndexSelected) > ProductIndex)
                    {
                        Console.WriteLine("Index Not Valid");

                    }
                    else
                    {

                        var href = ResultsLinks[Int32.Parse(ProductIndexSelected)].FindElement(By.TagName("a")).GetAttribute("href");
                        driver.Navigate().GoToUrl(href);
                    }
                }
                else
                {
                    Console.WriteLine("No Index Selected");
                }
            }
            catch (Exception Ex)
            {

                Console.WriteLine("Display Results Failed : "+Ex.Message);
            }
           
        }

        /// <summary>
        /// select shoe size and add to cart
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="wait"></param>
        public void SelectShoeSize(PhantomJSDriver driver, WebDriverWait wait)
        {
            var SizeDropDown = driver.FindElementById("pdp_size_select");
            SizeDropDown.Click();

            wait.Until((_driver) => { return ExpectedConditions.ElementIsVisible(By.ClassName("product_sizes_content")); });

            var ShoeSize = new List<IWebElement>(driver.FindElementsByTagName("a"));
            var SelectedShoeSize = ShoeSize.Find((ele) => { return ele.GetAttribute("data-modelsize") == "11_0"; });//TODO Select from File
            var AddToCart = new List<IWebElement>(driver.FindElementsByTagName("button")).Find((ele) => { return ele.GetAttribute("title") == "Add To Cart"; });

            if (SelectedShoeSize != null)
            {
                //Add To cart
                SelectedShoeSize.Click();

                Thread.Sleep(TimeSpan.FromSeconds(2));

                AddToCart.Click();

                Console.WriteLine("Added to Cart! ");

            }

        }
    }
}
