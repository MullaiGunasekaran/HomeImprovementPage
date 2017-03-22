using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using CommBankTest.Pages;
using System.Configuration;
using CommBankTest.Extensions;

namespace CommBankTest
{
    [Binding]
    public class HomeImprovementSteps
    {
        private HomeImprovementPage _objHomeImprove;
        private IWebDriver driver;

        [Given(@"I have launched the browser")]
        public void GivenIHaveLaunchedTheBrowser()
        {
            driver = (IWebDriver)ScenarioContext.Current["DRIVER"];
            driver.Manage().Window.Maximize();

        }

        [Given(@"I have entered the WebsiteURL")]
        public void GivenIHaveEnteredTheWebsiteURL()
        {
            driver = (IWebDriver)ScenarioContext.Current["DRIVER"];
            driver.Url = (string)ScenarioContext.Current["URL"];
        }

        [Given(@"I use datasheet ""(.*)"" and section ""(.*)""")]
        public void GivenIUseDatasheetAndSection(string p0, string p1)
        {
            Utility.GetDataFromXML(p0, p1);
        }

        [Given(@"I have entered the Search Text ""(.*)""")]
        public void GivenIHaveEnteredTheSearchText(string p0)
        {
            _objHomeImprove = new HomeImprovementPage(driver);
            _objHomeImprove.SelectSearchItem(p0);
        }

        [Given(@"I have entered the Postcode ""(.*)""")]
        public void GivenIHaveEnteredThePostcode(string p0)
        {
            driver = (IWebDriver)ScenarioContext.Current["DRIVER"];
            _objHomeImprove = new HomeImprovementPage(driver);
            _objHomeImprove.SelectSearchArea(p0);
        }
        
            
        [When(@"I press Lookup Button")]
        public void WhenIPressLookupButton()
        {
            driver = (IWebDriver)ScenarioContext.Current["DRIVER"];
            _objHomeImprove = new HomeImprovementPage(driver);
            _objHomeImprove.PressLookupButton();
        }
        
        [Then(@"the searchresults should be displayed")]
        public void ThenTheSearchresultsShouldBeDisplayed()
        {
            driver = (IWebDriver)ScenarioContext.Current["DRIVER"];
            _objHomeImprove = new HomeImprovementPage(driver);
            _objHomeImprove.DisplaytheResult();
        }

        [Then(@"I click the Next Button")]
        public void ThenIClickTheNextButton()
        {
            
        }

    }
}
