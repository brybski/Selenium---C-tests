using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Udemy
{
    internal class WindowHandling
    {
        IWebDriver driver;
        String url = "https://rahulshettyacademy.com/loginpagePractise";

        [SetUp]
        public void OpeningBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig()); //WebDriverManager package that helps implementing proper chromedriver for currently installed browser version
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }

        [TearDown]
        public void QuitBrowser()
        {
            //driver.Quit();
        }

        [Test]
        public void WindowHandle()
        {
            //click the link which open new tab in browser
            driver.FindElement(By.XPath("//a[contains(text(),'Free Access')]")).Click();
            
            //assert with property WindowHandles.Count that there are 2 pages opened;
            //Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));

        }
    }
}
