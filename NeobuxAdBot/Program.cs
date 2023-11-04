// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using NeobuxAdBot;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;


IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("/app/appsettings.json").Build();

var seleniumUri = new Uri(configuration.GetValue<string>("SeleniumUri")!);
IWebDriver seleniumDriver = new RemoteWebDriver(seleniumUri, new FirefoxOptions());

var login = new Login(seleniumDriver, configuration);
await login.PerformAsync();

seleniumDriver.Close();
