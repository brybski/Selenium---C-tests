using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
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
            //driver.Quit();
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

        [Test]
        public void AutoSuggestiveDropdowns()
        {
            //insert Po into input area, then wait until auto suggestive dropdown appears and select Poland
            driver.FindElement(By.XPath("//input[@id='autocomplete']")).SendKeys("Po");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//ul[@id='ui-id-1']/li/div[text()='Poland']")));
            driver.FindElement(By.XPath("//ul[@id='ui-id-1']/li/div[text()='Poland']")).Click();


            //check if dynamic input (.text not work here!) is equal to Poland
            string autoSuggestiveResult = driver.FindElement(By.XPath("//input[@id='autocomplete']")).GetAttribute("value");
            Assert.That(autoSuggestiveResult, Is.EqualTo("Poland"));
        }


        }
}
