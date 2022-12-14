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
            String parentWindowId = driver.CurrentWindowHandle;
            
            //click the link which open new tab in browser
            driver.FindElement(By.XPath("//a[contains(text(),'Free Access')]")).Click();
            
            //assert with property WindowHandles.Count that there are 2 pages opened;
            Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));

            //switch to second tab and then gather email address which is shown on red (part of the string) and try to log in with that email
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            string txt = driver.FindElement(By.XPath("//p[@class='im-para red']")).Text;
            String[] splittedText = txt.Split("at");
            String[] trimmedString = splittedText[1].Trim().Split(" ");
            driver.SwitchTo().Window(parentWindowId);
            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);

            //assert that data inserted into login is equal to expected email
            String email = driver.FindElement(By.Id("username")).GetAttribute("value");
            Assert.That(email, Is.EqualTo("mentor@rahulshettyacademy.com"));
            

        }
    }
}
