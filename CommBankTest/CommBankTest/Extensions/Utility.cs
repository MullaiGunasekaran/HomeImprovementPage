using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using System.Xml.Linq;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace CommBankTest.Extensions
{
    static class Utility
    {
        public static void GetDataFromXML(string datasheet, string section)
        {
            // XDocument xDoc = XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Data", datasheet));
            XDocument xDoc = XDocument.Load(Path.Combine("C:\\Mullai\\C#\\My Projects\\CommBankTest\\CommBankTest\\Extensions", "Data", datasheet));
            var cols = xDoc.Descendants(section).First();
            ScenarioContext.Current.Add("Nodename", section);
            foreach (XAttribute attribute in cols.Attributes())
            {
                Console.WriteLine("{0}:{1}", attribute.Name, attribute.Value);
                string temp = attribute.Name.ToString();
                ScenarioContext.Current.Add(temp, attribute.Value);
            }
          //  ScenarioContext.Current.Add("Nodename", section);

        }
        public static void WaitandClick(IWebElement element)
        {
            System.Threading.Thread.Sleep(2000);
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(60);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            bool elemt = wait.Until<bool>((d) =>
            {
                try
                {
                    if (d.Displayed & d.Enabled)
                    {
                        d.Click();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    string a = e.Message;
                    return false;
                }
            });

        }
        public static string UniqueTimeStamp()
        {
            var dd = DateTime.Now;
            return dd.Year.ToString() + dd.Month.ToString() + dd.Day.ToString() + dd.Hour.ToString() + dd.Minute.ToString() + dd.Second.ToString();
        }

        public static void TakeScreenShot(IWebDriver driver)
        {
            string FolderName = "C:\\Mullai\\Screenshots";
            string FileName = UniqueTimeStamp();
            string PathName = FolderName +"\\"+ FileName + ".jpg";
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(PathName, ScreenshotImageFormat.Jpeg);
        }

        public static IWebDriver SelectBrowser(string browsername)
        {
            IWebDriver driver;
            switch (browsername)
            {
                case "Chrome":
                    driver = new ChromeDriver();// ("../../Driver");
                    //driver = new FirefoxDriver();
                    return driver;
                    
                case "FireFox":
                    driver = new FirefoxDriver();
                    return driver;

                case "IE":
                    driver = new InternetExplorerDriver();
                    return driver;

                   
                default:
                    Console.WriteLine("Invalid Browser");
                    return null;
                    
            }
        }
    }
}
