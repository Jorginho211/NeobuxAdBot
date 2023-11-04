using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NeobuxAdBot
{
    public class AdClicker
    {
        private const string MENU_XPATH = "//*[@id=\"navAds\"]/a";
        private readonly IWebDriver _driver;

        public AdClicker(IWebDriver driver)
        {
            _driver = driver;
        }

        public async Task Perform()
        {
            _driver.FindElement(By.XPath(MENU_XPATH)).Click();
            await Task.Delay(1000);

            var html = _driver.PageSource;
            //Regex.Matches(html, @"");
        }
    }
}
