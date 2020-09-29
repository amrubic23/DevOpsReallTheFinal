using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace NUnitTestProject1
{
    public class UnitTest2
    {
        string test_url = "http://192.168.0.26:8080/login";

        IWebDriver driver;
        [SetUp]
        public void start_browser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void test_admin()
        {
            driver.Url = test_url;

            //System.Threading.Thread.Sleep(2000);

            IWebElement loginName = driver.FindElement(By.Name("User"));
            loginName.SendKeys("admin");

            IWebElement passName = driver.FindElement(By.Name("Pass"));
            passName.SendKeys("admin");

            IWebElement loginButton = driver.FindElement(By.Id("btn2"));
            loginButton.Click();

            //System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Test Passed");
        }

        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}