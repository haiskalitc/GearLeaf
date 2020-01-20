using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CrawDataShoes
{
    public class Crawler
    {
        IWebDriver driver;
        WebDriverWait waiter;
        private static Random random = new Random();

        public List<string> Init()
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
            chromeOption.AddArgument("--headless");
            chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOption.AddArgument("disable-infobars");
            driver = new ChromeDriver(chromeDriverService, chromeOption);
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            return reviews;
        }

        public void Run(List<string> dsLink)
        {
            try
            {
                if (dsLink.Count > 0)
                {
                    foreach (var link in dsLink)
                    {

                        var dsImage = new List<string>();
                        driver.Navigate().GoToUrl(link);
                        if (ElementsIsVisible(By.XPath("//div[@class='thumb-outter']//div[@class='thumb-box']//div//img")))
                        {
                            var dsLinkRate = driver.FindElements(By.XPath("//div[@class='thumb-outter']//div[@class='thumb-box']//div//img"));
                            var name = driver.FindElements(By.XPath("//div//h3"));
                            if (dsLinkRate.Count > 0 && name.Count > 0)
                            {
                                string fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Merchking\\" + ToTitleCase(name[1].Text.ToLower()) + " " + RandomString();
                                if (!Directory.Exists(fileSavePath))
                                {
                                    Directory.CreateDirectory(fileSavePath);
                                }

                                foreach (var item in dsLinkRate)
                                {
                                    string urlTem = item.GetAttribute("src");
                                    var urls = Regex.Split(urlTem, "&width=");
                                    if (!String.IsNullOrEmpty(urls[0]))
                                    {
                                        dsImage.Add(urls[0]);
                                    }
                                }
                                if (dsImage.Count > 0)
                                {
                                    foreach (var item in dsImage)
                                    {
                                        var img = DownloadImageFromUrl(item);
                                        if (img != null)
                                        {
                                        AnhBang:
                                            string filePath = fileSavePath + "\\" + ToTitleCase(name[1].Text.ToLower()) + " " + RandomString() + ".jpeg";
                                            if (File.Exists(filePath))
                                            {
                                                goto AnhBang;
                                            }
                                            img.Save(filePath);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string RandomString()
        {
     
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return "V"+ random.Next(9).ToString() + random.Next(9).ToString() + random.Next(9).ToString();

        }
        public System.Drawing.Image DownloadImageFromUrl(string item)
        {
            System.Drawing.Image image = null;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)
                    System.Net.HttpWebRequest.Create(item.Trim());
                webRequest.UseDefaultCredentials = true;
                webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();
                image = System.Drawing.Image.FromStream(stream);
                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return image;
        }

        public List<Links> Get(string url)
        {
            var dsRoot = new List<Links>();
            int countLink = 200;
            Looper:
            driver.Navigate().GoToUrl(url + "?page=" + countLink);
            var dsLink = GetListRate(driver);
            if (dsLink.Count > 0)
            {
                dsRoot.AddRange(dsLink);
                countLink++;
                goto Looper;
            }
            return dsRoot;
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
        public List<Links> GetListRate(IWebDriver driver)
        {
            List<Links> links = new List<Links>();
            var dsLinkRate = driver.FindElements(By.XPath("//div[@class='col-md-3 col-sm-6 col-xs-12']//a"));
            if (dsLinkRate.Count > 0)
            {
                foreach (var item in dsLinkRate)
                {
                    try
                    {
                        var url = item.FindElement(By.XPath("./img"));
                        var name = item.FindElement(By.XPath("./h4"));
                        Links link = new Links()
                        {
                            Name = name.Text,
                            URL = url.GetAttribute("src")
                        };
                        if (link != null)
                        {
                            if (!String.IsNullOrEmpty(link.URL) && !String.IsNullOrEmpty(link.Name))
                            {
                                links.Add(link);
                            }
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
                try { chromeDriverProcess.Kill(); } catch { }
            }
        }
        public void ClearGoogleChrome()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chrome");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try { chromeDriverProcess.Kill(); } catch { }
            }
        }
    }
}
