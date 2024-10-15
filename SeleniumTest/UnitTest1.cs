using MMLib.RapidPrototyping.Generators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Script;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System;


namespace SeleniumUnitTest
{
    [TestFixture]
    public class BlogTest
    {
        private IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
            // Initialize the ChromeDriver
            driver = new ChromeDriver();
            // Maximize the browser window
            driver.Manage().Window.Maximize();
            // Navigate to homepage
            driver!.Navigate().GoToUrl("https://pialember23.thkit.ee");
            // Set an implicit wait time
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [Test]
        public void GoToBlogPageTest()
        {
            
            // find blog link and click on it
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"menu-item-1364\"]"));
            element.Click();

            // Wait for the results page to load and display the results
            // It's better to use explicit waits in real tests
            System.Threading.Thread.Sleep(2000);

            // Verify that the page title contains the search term
            Assert.IsTrue(driver.Title.Contains("Blog – WP Blog"), "The page title does not contain the search term.");
        }

        [Test]
        public void GoToAboutPageTest()
        {

            // find about link and click on it
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"menu-item-1337\"]/a"));
            element.Click();
            System.Threading.Thread.Sleep(2000);

            String ActualTitle = driver.Title;
            String ExpectedTitle = "About – WP Blog";
            // Verify that the page titles are equal
            Assert.AreEqual(ExpectedTitle, ActualTitle);
        }
        [Test]
        public void SearchTest()
        {
            
            //find search element & click
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"ast-desktop-header\"]/div/div/div/div/div[2]/div[2]/div/div/a"));
            element.Click();
            System.Threading.Thread.Sleep(2000);
            //find search textbox
            IWebElement searchBox = driver.FindElement(By.Id("search-field"));
            //write text in the textbox
            searchBox.SendKeys("best tips");
            System.Threading.Thread.Sleep(2000);
            //find search submit and click on it
            IWebElement searchSubmit = driver.FindElement(By.XPath("//*[@id=\"ast-desktop-header\"]/div/div/div/div/div[2]/div[2]/div/form/label/button"));
            element.Click();
            System.Threading.Thread.Sleep(2000);

            // Verify that the page title contains the search term
            Assert.IsTrue(driver.Title.Contains("Search Results for “best tips” – WP Blog"), "The page title does not contain the search term.");
        }
        [Test]
        public void SelectPostTest()
        {   //navigate to home page
            driver!.Navigate().GoToUrl("https://pialember23.thkit.ee");
            System.Threading.Thread.Sleep(2000);
            //find articlse block element
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"uc_post_blocks_elementor_7bcce25\"]/div[1]"));
            //find the second link element in the article block
            IWebElement article = element.FindElements(By.TagName("a"))[1];
            //save the link elements text and click on the element
            var elementText = article.Text;
            article.Click();

            // Verify that the page title contains the articles name
            Assert.IsTrue(driver.Title.Contains(elementText.ToString()), "The page title does not contain the search term, you are not on the right page..");
        }
        [Test]
        public void CommentTest()
        {
            
            //navigate to home page
            driver!.Navigate().GoToUrl("https://pialember23.thkit.ee");
            System.Threading.Thread.Sleep(2000);
            //find articlse block element
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"uc_post_blocks_elementor_7bcce25\"]/div[1]"));
            //find the second link element in the article block and click on it
            IWebElement article = element.FindElements(By.TagName("a"))[1];
            article.Click();
            System.Threading.Thread.Sleep(2000);
            //find page's title and save it as text
            IWebElement pageTitle = driver.FindElement(By.TagName("Title"));
            var pageTitleText = pageTitle.Text;
            Console.WriteLine(pageTitleText);
            
            System.Threading.Thread.Sleep(2000);

            //find commet section
            IWebElement commentBox = driver.FindElement(By.Id("comment"));

            //dummytext
            LoremIpsumGenerator loremIpsumGenerator = new LoremIpsumGenerator();
            var someComments = loremIpsumGenerator.Next(3, 3);
            
            //enter dummytext to textbox
            commentBox.SendKeys(someComments);
            System.Threading.Thread.Sleep(2000);

            //find submit button and click on it
            IWebElement submitButton = commentBox.FindElement(By.XPath("//*[@id=\"submit\"]"));
            submitButton.Click();
            System.Threading.Thread.Sleep(2000);


            //verify that the page's title contains the same text as previous page's title
            Assert.IsTrue(driver.Title.Contains("Comment Submission Failure"), "It is possible to submit a comment without name information.");
        }

        [Test]
        public void SendEmailWithEmptyContactInfoTest()
        {
            //navigate to home page
            driver!.Navigate().GoToUrl("https://pialember23.thkit.ee");
            System.Threading.Thread.Sleep(2000);
            //find contact element and click on it
            IWebElement element = driver.FindElement(By.XPath("//*[@id=\"menu-item-150\"]/a"));
            element.Click();
            System.Threading.Thread.Sleep(2000);

            //dummytext
            LoremIpsumGenerator loremIpsumGenerator = new LoremIpsumGenerator();
            var someText = loremIpsumGenerator.Next(3, 3);

            //find message textarea element and add some text
            IWebElement messageBox = driver.FindElement(By.XPath("//*[@id=\"wpforms-1328-field_2\"]"));
            messageBox.SendKeys(someText);
            System.Threading.Thread.Sleep(2000);

            //Find send message button and click on it
            IWebElement sendButton = driver.FindElement(By.XPath("//*[@id=\"wpforms-submit-1328\"]"));
            sendButton.Click();
            System.Threading.Thread.Sleep(2000);

            //Find text element(This field is required)
            IWebElement requiredText = driver.FindElement(By.XPath("//*[@id=\"wpforms-1328-field_0-error\"]"));

            // Verify that the page title contains the articles name
            Assert.NotNull(requiredText.Text);
        }


        [TearDown]
        public void Teardown()
        {
            // Close the browser and dispose of the driver
            driver?.Dispose();
        }
    }
}


