using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace NeobuxAdBot
{
    public class Login
    {
        private const string MENU_XPATH = "//*[@id=\"mMenuDv\"]/div[2]/a[2]";
        private const string USERNAME_XPATH = "//*[@id=\"Kf1\"]";
        private const string PASSWORD_XPATH = "//*[@id=\"Kf2\"]";
        private const string CAPTCHA_XPATH = "//*[@id=\"Kf3\"]";
        private const string LOGIN_BTN_XPATH = "//*[@id=\"botao_login\"]";

        private const int CAPTCHA_CHECK_SECONDS_WAIT = 1; 

        private readonly IWebDriver _driver;
        private readonly IConfiguration _configuration;

        public Login(IWebDriver driver, IConfiguration configuration)
        {
            _driver = driver;
            _configuration = configuration;
        }

        public async Task PerformAsync()
        {
            _driver.Navigate().GoToUrl("https://www.neobux.com");
            _driver.FindElement(By.XPath(MENU_XPATH)).Click();

            var usernameFieldElem = _driver.FindElement(By.XPath(USERNAME_XPATH));
            var passwordFieldElem = _driver.FindElement(By.XPath(PASSWORD_XPATH));
            usernameFieldElem.SendKeys(_configuration.GetValue<string>("NeobuxUsername"));
            passwordFieldElem.SendKeys(_configuration.GetValue<string>("NeobuxPassword"));

            var catpchaFieldElem = _driver.FindElement(By.XPath(CAPTCHA_XPATH));

            if (catpchaFieldElem != null)
            {
                Console.WriteLine("Captcha Required: Need to be inserted manually");
                while(_driver.Url.Contains("m/l/?vl"))
                {
                    await Task.Delay(TimeSpan.FromSeconds(CAPTCHA_CHECK_SECONDS_WAIT));
                }

                await Task.Delay(TimeSpan.FromSeconds(2));
                return;
            }

            var btnLoginElem = _driver.FindElement(By.XPath(LOGIN_BTN_XPATH));
            btnLoginElem.Click();
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}
