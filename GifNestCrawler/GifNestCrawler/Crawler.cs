using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GifNestCrawler
{
    public class Crawler
    {

        IWebDriver driver;
        WebDriverWait waiter;
        private static Random random = new Random();

        public List<string> Init()
        {
            List<string> reviews = new List<string>();
            //ClearDriver();
            //ClearGoogleChrome();
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

        public List<string> GetLink(string url)
        {
            var dsResult = new List<string>();
            try
            {
                for (int i = 1; i < 183; i++)
                {
                    driver.Navigate().GoToUrl(url + "/shop/page/" + i + "/");
                    if (ElementsIsVisible(By.XPath("//div[@class='col-inner']//p//a")))
                    {
                        var dsLinkInPage = driver.FindElements(By.XPath("//div[@class='col-inner']//p//a"));
                        if (dsLinkInPage.Count > 0)
                        {
                            foreach(var item in dsLinkInPage)
                            {
                                if (!String.IsNullOrEmpty(item.GetAttribute("href")))
                                {
                                    dsResult.Add(item.GetAttribute("href"));
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return dsResult;
            }
            return dsResult;
        }

        /*
        public void Run(List<Product> dsLink)
        {
            try
            {
                if (dsLink.Count > 0)
                {
                    for (int i = 787; i < dsLink.Count; i++)
                    {
                        var link = dsLink[i];
                        var dsImage = new List<string>();
                        driver.Navigate().GoToUrl(link.URL);
                        if (ElementsIsVisible(By.XPath("//ul[@id='productThumbs']//li//a")))
                        {
                            var dsLinkRate = driver.FindElements(By.XPath("//ul[@id='productThumbs']//li//a"));
                            if (dsLinkRate.Count > 0)
                            {
                                string fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Lolonesies\\" + link.Title.Replace("?", "").Replace("\\", "").Replace("//", "");
                                if (!Directory.Exists(fileSavePath))
                                {
                                    Directory.CreateDirectory(fileSavePath);
                                }

                                foreach (var item in dsLinkRate)
                                {
                                    string urlTem = item.GetAttribute("href");
                                    if (!String.IsNullOrEmpty(urlTem))
                                    {
                                        dsImage.Add(urlTem);
                                    }
                                }
                                if (dsImage.Count > 0)
                                {
                                    foreach (var item in dsImage)
                                    {
                                        var img = DownloadImageFromUrl(item);
                                        if (img != null)
                                        {
                                            var arrTitle = item.Split('/');
                                            if (arrTitle.Count() > 0)
                                            {
                                                var arrTitles = arrTitle[arrTitle.Count() - 1].Split('?');
                                                if (arrTitles.Count() > 0)
                                                {
                                                    string filePath = fileSavePath + "\\" + arrTitles[0];
                                                    img.Save(filePath);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        */

        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string RandomString()
        {

            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return "V" + random.Next(9).ToString() + random.Next(9).ToString() + random.Next(9).ToString();

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
