using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using CommBankTest.Extensions;

namespace CommBankTest.Pages 
{
    class HomeImprovementPage
    {
        private IWebDriver driver;
        public HomeImprovementPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "search_str")]
        private IWebElement Search_Box;

        [FindsBy(How = How.Name, Using = "search_zip")]
        private IWebElement Search_ZipCode;

        [FindsBy(How = How.Name, Using = "w")]
        private IWebElement Search_Final;

        

        public void SelectSearchItem(string searchtext)
        {
            
            Search_Box.Clear();
            string[] str = searchtext.Split(':');
            Search_Box.SendKeys((string)ScenarioContext.Current[str[1].ToString()]);
            //Search_Box.SendKeys("Arborists");
        }

        public void SelectSearchArea(string area)
        {
            Search_ZipCode.Clear();
            string[] str = area.Split(':');
            Search_ZipCode.SendKeys((string)ScenarioContext.Current[str[1].ToString()]);
         //   Search_ZipCode.SendKeys("Wollongong");
        }

        public void PressLookupButton()
        {
            Search_Box.Submit();
        }

        public void DisplaytheResult()
        {
            Search_Final.SendKeys(Keys.Escape);
            // IWebElement Result =  driver.FindElement(By.XPath("//div[contains(@class, 'basic-listing__contact-info') and normalize-space(.//text()='United Tree Services Pty Ltd'])"));
            //IList<IWebElement> Company_Name = driver.FindElements(By.ClassName("basic-listing__contact-info"));
            IList<IWebElement> Company_Name = driver.FindElements(By.XPath("//div[@class='basic-listing__contact-info']"));
            var Company_array = new string[Company_Name.Count];

            for (int i = 0; i < Company_Name.Count; i++)
            {
                IWebElement texts = Company_Name[i].FindElement(By.ClassName("basic-listing__title"));
                Company_array[i] = texts.Text;
            }

            IList<IWebElement> Recommendations_Num = driver.FindElements(By.ClassName("basic-listing__recommendations"));

            var str_array = new string[Recommendations_Num.Count];

            if (Recommendations_Num.Count == Company_Name.Count)
            {
                for (int j = 0; j < Recommendations_Num.Count; j++)
                {
                    IWebElement texts = Recommendations_Num[j].FindElement(By.ClassName("basic-listing__recommendation-count"));
                    str_array[j] = texts.Text;
                    Console.WriteLine("{0}, {1}", Company_array[j], str_array[j]);
                }
            }

            string OnlyNumber;
            var str_to_int_array = new UInt16[Recommendations_Num.Count];
            int max = str_to_int_array[0];
            for (int j = 0; j < Recommendations_Num.Count; j++)
            {
                OnlyNumber = Regex.Replace(str_array[j], "[^0-9]+", string.Empty);
                UInt16.TryParse(OnlyNumber, out str_to_int_array[j]);            
            }

            for (int i = 1; i < Recommendations_Num.Count; ++i)
            {                
                if (str_to_int_array[0] < str_to_int_array[i])
                    str_to_int_array[0] = str_to_int_array[i];
            }
            //     Console.WriteLine("Largest element = " + str_to_int_array[0]);

            string mystr = str_to_int_array[0].ToString();
            IWebElement final_Result = driver.FindElement(By.PartialLinkText(mystr));
            final_Result.Click();
            IWebElement Comp = driver.FindElement(By.TagName("h2"));
            Console.WriteLine("**************************************************");
            Console.WriteLine("Company with More Recommendations : " + Comp.Text + " with " +mystr + " Recommendations");
            Console.WriteLine("**************************************************");
            driver.Navigate().Back();
            Search_Final.SendKeys(Keys.Escape);
            //   Console.WriteLine(Comp.Text);


            //string filePath = @"C:Mullai\test.csv";
            //string delimiter = ",";
            //string[][] output = new string[][]{
            //       new string[]{"Col 1 Row 1", "Col 2 Row 1", "Col 3 Row 1"},
            //       new string[]{"Col1 Row 2", "Col2 Row 2", "Col3 Row 2"}
            //   };
            //int length = output.GetLength(0);
            //StringBuilder sb = new StringBuilder();
            //for (int index = 0; index < length; index++)
            //    sb.AppendLine(string.Join(delimiter, output[index]));
            //File.WriteAllText(filePath, sb.ToString());


            //string filePath1 = @"C:Mullai\test1.csv";
            //string delimiter1 = ",";
            ////string[][] output = new string[][]{
            ////       new string[]{"Col 1 Row 1", "Col 2 Row 1", "Col 3 Row 1"},
            ////       new string[]{"Col1 Row 2", "Col2 Row 2", "Col3 Row 2"}

            //int length1 = Company_array.GetLength(0);
            //StringBuilder sb1 = new StringBuilder();
            //for (int index = 0; index < length1; index++)
            //{

            //    sb1.Append(string.Join(delimiter1, Company_array[index]));
            //    //  sb.Append(string.Join(delimiter1));
            //    //.AppendLine(string.Join(delimiter1, ","));
            //    sb1.AppendLine(string.Join(delimiter1, str_array[index]));
            //    //sb1.Insert(index, ",");

            //}
            //  sb1.AppendLine()
            //    File.WriteAllText(filePath1, sb1.ToString());





        }
    }
}
