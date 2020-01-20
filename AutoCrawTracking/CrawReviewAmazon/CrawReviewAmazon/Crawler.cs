using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrawReviewAmazon
{
    public class Crawler
    {
        IWebDriver driver;
        WebDriverWait waiter;
        private static Random random = new Random();

        public List<string> Init(string url)
        {
            List<string> reviews = new List<string>();
            ClearDriver();
            ClearGoogleChrome();
            // service
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            chromeDriverService.HideCommandPromptWindow = true;
            // option
            var chromeOption = new ChromeOptions();
            chromeOption.AddArguments("chrome.switches", "--disable-extensions --disable-extensions-file-access-check " +
                "--disable-extensions-http-throttling --disable-infobars --enable-automation --start-maximized");
            chromeOption.AddUserProfilePreference("credentials_enable_service", false);
            // chromeOption.AddArgument("--headless");
            chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOption.AddArgument("disable-infobars");
            driver = new ChromeDriver(chromeDriverService, chromeOption);
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Navigate().GoToUrl(url);
            return reviews;
        }

        public List<Review> Get(List<Source> dsSource)
        {
           List<Review> ds = new List<Review>();
           var dsLink = GetListRate(driver);
            if (dsLink.Count > 0)
            {
                Actions actions = new Actions(driver);
                foreach (var item in dsLink)
                {
                    driver.Navigate().GoToUrl(item.Url);
                    if (ElementsIsVisible(By.XPath("//h4[@class='OrderNumber font-weight-bold d-flex']")))
                    {
                        var orderId = driver.FindElement(By.XPath("//h4[@class='OrderNumber font-weight-bold d-flex']")).Text.Replace("#", "");
                        var carrierName = "17track.net";
                        if (ElementsIsVisible(By.XPath("//div//div//div[@class='card-body text-dark']//div[@class='font-weight-bold mb-2']")))
                        {
                            var customer = driver.FindElement(By.XPath("//div//div//div[@class='card-body text-dark']//div[@class='font-weight-bold mb-2']")).Text;
                            if (ElementsIsVisible(By.XPath("//div[@class='FulfillmentTracking mb-3']//a")))
                            {
                                var transac = driver.FindElement(By.XPath("//div[@class='FulfillmentTracking mb-3']//a")).Text;
                                var trans = dsSource.FirstOrDefault(m => m.OrderId.Equals(orderId));
                                if (trans != null)
                                {
                                    Review re = new Review()
                                    {
                                        OrderID = orderId,
                                        CarrierName = carrierName,
                                        Customer = customer,
                                        TrackingCode = transac,
                                        TransactionID = trans.TransactionId
                                    };
                                    ds.Add(re);
                                }
                            }
                        }
                    }
                }
            }
            return ds;
        }
        public bool ElementsIsVisible(By xPath)
        {
            try
            {
                //innerexception
                var ignoredExceptions = new List<Type>() { typeof(StaleElementReferenceException) };
                waiter.IgnoreExceptionTypes(ignoredExceptions.ToArray());
                waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(xPath));
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                GC.Collect();
            }
        }
        public List<Rate> GetListRate(IWebDriver driver)
        {
            List<Rate> links = new List<Rate>();
            var dsLinkRate = driver.FindElements(By.XPath("//div[(@class='OrderTableInner')]//tr//td//a"));
            if (dsLinkRate.Count > 0)
            {
                foreach (var item in dsLinkRate)
                {
                    try
                    {
                        var rate = new Rate()
                        {
                            Url = item.GetAttribute("href"),
                        };
                        if (rate != null)
                        {
                            links.Add(rate);
                        }
                    }
                    catch { }
                }
            }
            return links;
        }
        public void ClearDriver()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try {chromeDriverProcess.Kill();} catch {}
            }
        }
        public void ClearGoogleChrome()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chrome");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try{ chromeDriverProcess.Kill(); }catch{}
            }
        }
    }
}
