using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Configuration;
using OpenQA.Selenium;


namespace CommBankTest.Extensions
{
    [Binding]
    public sealed class Hooks1
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        IWebDriver driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            //TODO: implement logic that has to run before executing each scenario
            string URL = ConfigurationManager.AppSettings["URL"];
            ScenarioContext.Current["URL"] = URL;
            string COMM_BANK_URL = ConfigurationManager.AppSettings["URL1"];
            ScenarioContext.Current["COMMBANK_URL"] = COMM_BANK_URL;
            string browser = ConfigurationManager.AppSettings["ChromeBrowser"];          
            driver = Utility.SelectBrowser(browser);
            ScenarioContext.Current["DRIVER"] = driver;
            
        }

        [AfterStep]
        [Scope (Tag = "ScreenShot")]
        public void TakeScreenShot()
        {
            IWebDriver browser;
            browser = (IWebDriver)ScenarioContext.Current["DRIVER"];
            Utility.TakeScreenShot(browser);
        }
  

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
