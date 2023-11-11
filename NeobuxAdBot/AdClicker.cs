using OpenQA.Selenium;

namespace NeobuxAdBot
{
    public class AdClicker
    {
        private const string MENU_XPATH = "//*[@id=\"navAds\"]/a";
        private const string ACTIVE_ADS_CLASSNAME = ".cell:not(.c_ad0)";
        private const string RED_CIRCLE_TAG = "img";

        private const int AD_WAIT_SECONDS = 30;

        private readonly IWebDriver _driver;

        public AdClicker(IWebDriver driver)
        {
            _driver = driver;
        }

        public async Task Perform()
        {
            var random = new Random();
            _driver.FindElement(By.XPath(MENU_XPATH)).Click();
            await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1, 3)));

            var activeAdsElems = _driver.FindElements(By.CssSelector(ACTIVE_ADS_CLASSNAME));
            Console.WriteLine($"Found {activeAdsElems.Count} ads to click");
            await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1, 3)));

            var mainWindowHandler = _driver.CurrentWindowHandle;

            foreach(var activeAd in activeAdsElems)
            {
                activeAd.Click();
                await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1, 3)));

                var redCircle = activeAd.FindElement(By.TagName(RED_CIRCLE_TAG));
                redCircle.Click();
                await Task.Delay(TimeSpan.FromSeconds(AD_WAIT_SECONDS));
                
                var lastWindowHandler = _driver.WindowHandles.Last();
                _driver.SwitchTo().Window(lastWindowHandler);
                _driver.Close();
                await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1, 3)));

                _driver.SwitchTo().Window(mainWindowHandler);
            }
        }
    }
}
