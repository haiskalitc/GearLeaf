﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
using System.Threading.Tasks;
using System.Windows;

namespace CrawDataGifNest
{
   public class Crawler
    {

        IWebDriver driver;
        WebDriverWait waiter;
        private static Random random = new Random();

        public Crawler()
        {
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
            //chromeOption.AddArgument("--headless");
            chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOption.AddArgument("disable-infobars");
            driver = new ChromeDriver(chromeDriverService, chromeOption);
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
        public string GetLink(string url)
        {
            string dsResult = "";
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
                            foreach (var item in dsLinkInPage)
                            {
                                if (!String.IsNullOrEmpty(item.GetAttribute("href")))
                                {
                                    dsResult += item.GetAttribute("href") + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return dsResult;
            }
            return dsResult;
        }
        public static string Remove_Special_Characters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9._]", " ");
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        public void Dowload(List<string> dsLink)
        {
            if(dsLink.Count > 0)
            {
                for (int j = 1522; j < dsLink.Count; j++)
                {
                    if (!String.IsNullOrEmpty(dsLink[j]))
                    {
                        driver.Navigate().GoToUrl(dsLink[j]);
                        if (ElementsIsVisible(By.XPath("//nav//a")) && ElementsIsVisible(By.XPath("//h1")))
                        {
                            var dsTitleNav = driver.FindElements(By.XPath("//nav//a"));
                            var titleH = driver.FindElement(By.XPath("//h1"));
                            string title = "";
                            foreach (var dsTitle in dsTitleNav)
                            {
                                if(!String.IsNullOrEmpty(dsTitle.Text))
                                {
                                    if (!String.IsNullOrEmpty(title))
                                    {
                                        title += " ";
                                    }
                                    title += dsTitle.Text;
                                }
                            }
                            bool FLAG = false;
                            if (!String.IsNullOrEmpty(titleH.Text))
                            {
                                title += " " + titleH.Text;
                                string fileSavePath = "";
                                if (titleH.Text.Contains("Hoodie"))
                                {
                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Hoodie\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\"+ title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " +  RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Sweatpant"))
                                {
                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Sweatpant\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Sneaker"))
                                {

                                    try
                                    {
                                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Sneaker\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                        if (!Directory.Exists(path))
                                        {
                                            Directory.CreateDirectory(path);
                                        }
                                        fileSavePath = path + "\\" + title;
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                        else
                                        {
                                            fileSavePath += " " + RandomString(5);
                                            if (!Directory.Exists(fileSavePath))
                                            {
                                                Directory.CreateDirectory(fileSavePath);
                                            }
                                        }
                                    }
                                    catch(Exception ex)
                                    {

                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Phone Case"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Phone Case\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("T-Shirt"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\T-Shirt\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Jacket"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Jacket\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }                                       
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Jersey"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Jersey\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Low-Top"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Low-Top\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Shade"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Shade\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Bedding Set"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Bedding Set\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Sweatshirt"))
                                {

                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Sweatshirt\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                    ///
                                }
                                else
                                {
                                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Other\\" + Remove_Special_Characters(dsTitleNav[dsTitleNav.Count - 1].Text);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fileSavePath = path + "\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        fileSavePath += " " + RandomString(5);
                                        if (!Directory.Exists(fileSavePath))
                                        {
                                            Directory.CreateDirectory(fileSavePath);
                                        }
                                    }
                                }
                                if (!FLAG)
                                {
                                    if (ElementsIsVisible(By.XPath("//div[@class='flickity-slider']//div")))
                                    {
                                        var dsAnh = driver.FindElements(By.XPath("//div[@class='flickity-slider']//div//a"));
                                        if (!titleH.Text.Contains("Shade"))
                                        {
                                            for (int i = 0; i < dsAnh.Count / 2; i++)
                                            {
                                                if (dsAnh[i] != null)
                                                {
                                                    var url = dsAnh[i].GetAttribute("href");
                                                    if (!String.IsNullOrEmpty(url))
                                                    {
                                                        try
                                                        {
                                                            var img = DownloadImageFromUrl(url);
                                                            if (img != null)
                                                            {
                                                                img.Save(fileSavePath + "\\" + title.ToLower().Replace(" ", "-") + " " + i + ".jpeg");
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < dsAnh.Count; i++)
                                            {
                                                if (dsAnh[i] != null)
                                                {
                                                    var url = dsAnh[i].GetAttribute("href");
                                                    if (!String.IsNullOrEmpty(url))
                                                    {
                                                        try
                                                        {
                                                            var img = DownloadImageFromUrl(url);
                                                            if (img != null)
                                                            {
                                                                img.Save(fileSavePath + "\\" + title.ToLower().Replace(" ", "-") + " " + i + ".jpeg");
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(dsLink[j]);
                                }
                            }
                        }
                    }
                    Console.WriteLine(j);
                }
                MessageBox.Show("Xong!!");
            }
            else
            {
                MessageBox.Show("Không có Link");
            }
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
