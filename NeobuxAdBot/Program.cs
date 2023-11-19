// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using NeobuxAdBot;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Polly;


IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"/app/appsettings.json").Build();

// Se añade politica de reintentos para esperar por selenium
IAsyncPolicy<RemoteWebDriver> retryPolicy = Policy<RemoteWebDriver>
    .Handle<WebDriverException>()
    .WaitAndRetryAsync(3, retryAttemp => TimeSpan.FromSeconds(Math.Pow(2, retryAttemp)));

var seleniumUri = new Uri(configuration.GetValue<string>("SeleniumUri")!);
IWebDriver seleniumDriver = await retryPolicy.ExecuteAsync(() => Task.FromResult(new RemoteWebDriver(seleniumUri, new FirefoxOptions())));

var login = new Login(seleniumDriver, configuration);
await login.PerformAsync();

var adClicker = new AdClicker(seleniumDriver);
await adClicker.Perform();

seleniumDriver.Quit();
