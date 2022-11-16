using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Udemy
{
    /*
            Basic XPath syntax:
            //tag[@attribute='value']

            practice on https://practicetestautomation.com/practice-test-exceptions/

            edit button XPath:
            //button[@id="edit_btn"]

            second input row:
            //div[@id="row2"]/input

            */

    /*
    / vs // vs ./ vs .// section

    / is used at the beginning of absolute location path, short for child node and it selects a root element
    // is used at the beginning of relative location path, short for descendant or self node, selects element anywhere on a page

    example for relative XPath for row 1 text field:
    //div[@id='rows']/div/div/input == //div[@id='rows']//input - the second syntax mean search input tag in any of descendant of div with attribute = rows

    .//input or ./input - relative location path, starting at the context node; example:
    we are having list of IWebElements:
    IList<IWebElement> rows = driver.FindElement(By.XPath("//div[@class='row']"));

    and now we are iterating through this list
    foreach (IWebElement row in rows) {
    row.FindElement(By.XPath("//label")).Text;
    }
    having only // in XPath we're missing information that we are referring to row element and looking for label in every place of the html code;
    when used:
    row.FindElement(By.XPath(".//label")).Text;
    we are telling Selenium that we want to get label tag from row IWebElement only, not from the whole page

    excercise below
    */


    internal class XPath_training
    {
        IWebDriver driver;
        String url = "https://practicetestautomation.com/practice-test-exceptions/";

        [SetUp]
        public void OpeningBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig()); //WebDriverManager package that helps implementing proper chromedriver for currently installed browser version
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }

        [Test]
        public void XPathExcercise()
        {
            driver.FindElement(By.XPath("//button[@id='add_btn']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='row2']/input")));
            //driver.FindElement(By.XPath("//div[@id='row2']/input")).SendKeys("sushi");

            IList<IWebElement> rows = driver.FindElements(By.XPath("//div[@class='row']"));
            String? actualText = null;
            foreach (IWebElement row in rows)
            {
                String label = row.FindElement(By.XPath(".//label")).Text;

                if (label.Equals("Row 2"))
                {
                    row.FindElement(By.XPath(".//input")).SendKeys("sushi");
                    row.FindElement(By.XPath(".//button[@id='save_btn']")).Click();
                    actualText = row.FindElement(By.XPath(".//input")).GetAttribute("value");
                    break;
                }

            }
            Assert.That(actualText, Is.EqualTo("sushi"));
        }

            [TearDown]
        public void QuitBrowser()
        {
            //driver.Quit();
        }
    }
}
