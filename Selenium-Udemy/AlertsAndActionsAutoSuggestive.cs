using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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

        [Test]
        public void ActionsClassHover()
        {
            //hovering over button using Actions class, remember to add Perform for methods of that class!
            Actions a = new Actions(driver);
            IWebElement hover = driver.FindElement(By.XPath("//button[contains(text(),'Hover')]"));
            a.MoveToElement(hover).Perform();

            //select Top from the list
            IWebElement Top = driver.FindElement(By.XPath("//div/a[contains(text(),'Top')]"));
            a.MoveToElement(Top).Click().Perform();

            //assertion
            Assert.That(driver.Url, Is.EqualTo("https://rahulshettyacademy.com/AutomationPractice/#top"));

            //drag and drop, double click, right click (Context Click) are also available as methods from that class
        }

        [Test]
        public void Frames()
        {
            //scrolling down to iframe
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", frameScroll);
            
            //switch to iframe and then click all access plan link
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();

            //then switch back to original website
            driver.SwitchTo().DefaultContent();
        }




        }
}
