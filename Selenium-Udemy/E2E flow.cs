using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Udemy
{
    public class Tests
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

        [Test]
        public void LoggingInWithIncorrectPassword()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning2");
            driver.FindElement(By.Id("terms")).Click(); //bug found here - logging in is possible without accepting terms
            driver.FindElement(By.Id("signInBtn")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id("signInBtn"), "Sign In")); //wait instruction for response if login is done successfully or not
            String errorMessage = driver.FindElement(By.CssSelector(".alert.alert-danger.col-md-12")).Text;
            Assert.That(errorMessage, Is.EqualTo("Incorrect username/password."));
        }

        [Test]
        public void LoggingInWithProperCredentialsAsUser()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning");
            driver.FindElement(By.CssSelector("input[value = 'user']")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));
            driver.FindElement(By.Id("okayBtn")).Click();
            driver.FindElement(By.Id("terms")).Click();
            driver.FindElement(By.Id("signInBtn")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".nav-item.active")));
            Assert.That(driver.Url, Is.EqualTo("https://rahulshettyacademy.com/angularpractice/shop"));
        }

        [TearDown]
        public void QuitBrowser()
        {
            driver.Quit();
        }
    }
}