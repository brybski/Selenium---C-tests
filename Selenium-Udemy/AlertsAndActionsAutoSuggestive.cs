using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Udemy
{
    internal class AlertsAndActionsAutoSuggestive
    {
        IWebDriver driver;
        String url = "https://rahulshettyacademy.com/AutomationPractice";

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
            driver.Quit();
        }

        [Test]
        public void TestAlert()
        {
            //type name into name area, then click on confirm
            driver.FindElement(By.XPath("//input[@id='name']")).SendKeys("Bartek");
            driver.FindElement(By.XPath("//input[@id='confirmbtn']")).Click();

            //get whole text from alert and accept it
            String alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            //assert that alert contains name provided before
            Assert.That(alertText, Contains.Substring("Bartek"));
        }


    }
}
