using AngleSharp.Dom;
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
    internal class XPath_training
    {
        IWebDriver driver;
        String url = "add proper link here";

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
            //div[@id='rows']/div/div/input

            */
        }


        [TearDown]
        public void QuitBrowser()
        {
            driver.Quit();
        }
    }
}
