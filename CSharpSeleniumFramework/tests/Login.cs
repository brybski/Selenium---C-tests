using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using CSharpSeleniumFramework.utilities;

namespace CSharpSeleniumFramework.tests
{
    public class Login : Base
    {

        [Test]
        public void FailedLoggingInWithIncorrectPassword()
        {
            //add wrong logging in data
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning2");
            driver.FindElement(By.Id("terms")).Click(); //bug found here - logging in is possible without accepting terms

            //click sign in and wait until error appears
            driver.FindElement(By.Id("signInBtn")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(By.Id("signInBtn"), "Sign In")); //wait instruction for response if login is done successfully or not

            //check if error is as expected
            string errorMessage = driver.FindElement(By.CssSelector(".alert.alert-danger.col-md-12")).Text;
            Assert.That(errorMessage, Is.EqualTo("Incorrect username/password."));
        }
    }
}