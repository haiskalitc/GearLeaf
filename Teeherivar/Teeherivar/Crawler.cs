using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OpenQA.Selenium;
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
using System.Threading.Tasks;
using System.Xml;

namespace Teeherivar
{
    public class Crawler
    {
        public List<string> DanhSachSize = new List<string>() { "2XL", "3XL", "4XL", "5XL", "L", "M", "S", "XL" };
        public List<string> DanhSachMau = new List<string>()
        {
            "Athletic Heather",
            "Black",
            "Blue",
            "Chocolate",
            "Forest Green", "Irish Green", "Light Blue", "Light Pink", "Navy",
            "Orange", "Pink", "Purple", "Red", "Sports Grey", "White"
        };
        public List<string> DanhSachLoai = new List<string>() { "Class T - Shirt", "Crewneck Sweatshirt", "Hoodie", "Ladies T - Shirt" };
        public static string PATH_ID = "";
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
            chromeOption.AddArgument("--headless");
            chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOption.AddArgument("disable-infobars");
            driver = new ChromeDriver(chromeDriverService, chromeOption);
            waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }
        public List<Product> Get(string path)
        {
            var dsProduct = new List<Product>();
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                string reply = wc.DownloadString(path);
                XmlDocument urldoc = new XmlDocument();
                urldoc.LoadXml(reply);
                XmlNodeList xnList = urldoc.GetElementsByTagName("url");
                foreach (XmlNode node in xnList)
                {
                    dsProduct.Add(new Product() { URL = node["loc"].InnerText });
                }
                dsProduct.RemoveAt(0);
                return dsProduct;
            }
            catch (Exception ex)
            {
                dsProduct.RemoveAt(0);
                return dsProduct;
            }
        }
        public void RunCrawl(string[] text)
        {
            foreach (var item in text)
            {
                Console.WriteLine("Đang tải link XML " + item);
                var ds = Get(item);
                Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                Console.WriteLine("Bắt đầu cờ rao data");
                Run(ds, item);
            }
        }

        public void Run(List<Product> dsLink, string PATH)
        {
            if (dsLink.Count > 0)
            {
                var dsResult = new List<Product>();

                for (int i = 0; i < dsLink.Count; i++)
                {
                    try
                    {
                        // Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                        Console.Write("Đang tải link thứ " + (i + 1) + " ... ");
                        if (dsLink[i] != null)
                        {
                            if (!String.IsNullOrEmpty(dsLink[i].URL))
                            {
                                driver.Navigate().GoToUrl(dsLink[i].URL);
                                var dsTitleNav = driver.FindElements(By.XPath("//nav//a"));
                                var titleH = driver.FindElement(By.XPath("//h1"));
                                string title = "";
                                string url = "";
                                foreach (var dsTitle in dsTitleNav)
                                {
                                    if (!String.IsNullOrEmpty(dsTitle.Text))
                                    {
                                        if (!String.IsNullOrEmpty(title))
                                        {
                                            title += " ";
                                        }
                                        title += dsTitle.Text;
                                    }
                                }
                                title += " " + titleH.Text;
                                if (ElementsIsVisible(By.XPath("//div[contains(@class,'product-thumbnails')]//div[@class='flickity-viewport']//div[@class='flickity-slider']//div[contains(@class,'col')]//a/img")))
                                {
                                    var dsAnh = driver.FindElements(By.XPath("//div[contains(@class,'product-thumbnails')]//div[@class='flickity-viewport']//div[@class='flickity-slider']//div[contains(@class,'col')]//a/img"));
                                    if (dsAnh.Count > 0)
                                    {
                                        foreach (var itemAnh in dsAnh)
                                        {
                                            if (!String.IsNullOrEmpty(url))
                                            {
                                                url += ", ";
                                            }
                                            url += itemAnh.GetAttribute("src");
                                        }
                                    }
                                }
                                if (!String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(url))
                                {
                                    dsResult.Add(new Product() { Title = ToTitleCase(title), URL = url });
                                    Console.WriteLine("Thành công");
                                }
                                else
                                {
                                    Console.WriteLine("Thất bại - KHÔNG TÌM THẤY LINK ẢNH");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Thất bại - KHÔNG TÌM THẤY LINK SẢN PHẨM");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Thất bại - KHÔNG TÌM THẤY LINK SẢN PHẨM");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Thất bại");
                        try
                        {
                            XSSFWorkbook wb = new XSSFWorkbook();
                            ISheet sheet = wb.CreateSheet();
                            var row1 = sheet.CreateRow(0);
                            row1.CreateCell(0).SetCellValue("Title");
                            row1.CreateCell(1).SetCellValue("Url");
                            int rowIndex = 1;
                            foreach (var item in dsResult)
                            {
                                var newRow = sheet.CreateRow(rowIndex);
                                newRow.CreateCell(0).SetCellValue(item.Title);
                                newRow.CreateCell(1).SetCellValue(item.URL);
                                rowIndex++;
                            }
                            var dsPath = PATH.Split('/');
                            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Teeherivar " + " " + dsPath[dsPath.Length - 1].Split('.')[0] + " " + DateTime.Now.ToLongDateString() + ".xlsx", FileMode.CreateNew);
                            wb.Write(fs);
                            Console.WriteLine("Xuất Excel thành công");
                        }
                        catch
                        {
                            Console.WriteLine("Xuất Excel thất bại");
                        }
                        finally
                        {
                            GC.Collect();
                        }
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                if (dsResult.Count > 0)
                {
                    try
                    {
                        XSSFWorkbook wb = new XSSFWorkbook();
                        ISheet sheet = wb.CreateSheet();
                        var row1 = sheet.CreateRow(0);
                        row1.CreateCell(0).SetCellValue("Title");
                        row1.CreateCell(1).SetCellValue("Url");
                        int rowIndex = 1;
                        foreach (var item in dsResult)
                        {
                            var newRow = sheet.CreateRow(rowIndex);
                            newRow.CreateCell(0).SetCellValue(item.Title);
                            newRow.CreateCell(1).SetCellValue(item.URL);
                            rowIndex++;
                        }
                        var dsPath = PATH.Split('/');
                        FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Teeherivar " + " " + dsPath[dsPath.Length - 1].Split('.')[0] + " " + DateTime.Now.ToLongDateString() + ".xlsx", FileMode.CreateNew);
                        wb.Write(fs);
                        Console.WriteLine("Xuất Excel thành công");
                    }
                    catch
                    {
                        Console.WriteLine("Xuất Excel thất bại");
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
            }
            else
            {
                Console.WriteLine("Không có link");
                ClearDriver();
                ClearGoogleChrome();
            }
        }
        public List<Export> GetProduct(List<Product> dsLink, string file)
        {
            if (dsLink.Count > 0)
            {
                var dsResult = new List<Export>();
                string des = File.ReadAllText(Environment.CurrentDirectory + "\\des.txt");

                for (int i = 0; i < /*1*/ dsLink.Count; i++)
                {
                    try
                    {
                        Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                        Console.Write(file + " link " + (i + 1) + " is in processing" + "... ");
                        if (dsLink[i] != null)
                        {
                            if (!String.IsNullOrEmpty(dsLink[i].URL))
                            {
                                driver.Navigate().GoToUrl(dsLink[i].URL);
                                //var dsTitleNav = driver.FindElements(By.XPath("//nav//a"));

                                string title = "";
                                string url = "";
                                string sku = "";
                                string categories = "";
                                string tags = "";

                                if (ElementsIsVisible(By.XPath("//h1")))
                                {
                                    var titleH = driver.FindElement(By.XPath("//h1"));
                                    if (titleH != null)
                                    {
                                        title = ToTitleCase(titleH.Text);
                                    }
                                }

                                if (ElementsIsVisible(By.XPath("//span[@class='posted_in']//a")))
                                {
                                    var titleH = driver.FindElements(By.XPath("//span[@class='posted_in']//a"));
                                    if (titleH != null)
                                    {
                                        foreach (var dsTitle in titleH)
                                        {
                                            if (!String.IsNullOrEmpty(dsTitle.Text))
                                            {
                                                if (!String.IsNullOrEmpty(categories))
                                                {
                                                    categories += ", ";
                                                }
                                                categories += ToTitleCase(dsTitle.Text);
                                            }
                                        }
                                    }
                                }

                                if (ElementsIsVisible(By.XPath("//span[@class='tagged_as']//a")))
                                {
                                    var titleH = driver.FindElements(By.XPath("//span[@class='tagged_as']//a"));
                                    if (titleH != null)
                                    {
                                        foreach (var dsTitle in titleH)
                                        {
                                            if (!String.IsNullOrEmpty(dsTitle.Text))
                                            {
                                                if (!String.IsNullOrEmpty(tags))
                                                {
                                                    tags += ", ";
                                                }
                                                tags += ToTitleCase(dsTitle.Text);
                                            }
                                        }
                                    }
                                }

                                if (ElementsIsVisible(By.XPath("//div[contains(@class,'product-thumbnails')]//div[@class='flickity-viewport']//div[@class='flickity-slider']//div[contains(@class,'col')]//a/img")))
                                {
                                    var dsAnh = driver.FindElements(By.XPath("//div[contains(@class,'product-thumbnails')]//div[@class='flickity-viewport']//div[@class='flickity-slider']//div[contains(@class,'col')]//a/img"));
                                    if (dsAnh.Count > 0)
                                    {
                                        foreach (var itemAnh in dsAnh)
                                        {
                                            if (!String.IsNullOrEmpty(url))
                                            {
                                                url += ", ";
                                            }
                                            url += itemAnh.GetAttribute("src");
                                        }
                                    }
                                }

                                if (ElementsIsVisible(By.XPath("//span[@class='sku']")))
                                {
                                    var skuElement = driver.FindElement(By.XPath("//span[@class='sku']"));
                                    if (skuElement != null)
                                    {
                                        sku = skuElement.Text;
                                    }
                                }

                                if (!String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(url))
                                {
                                    var parent = new Export()
                                    {
                                        ID = "", 
                                        Type = "variable",
                                        SKU = sku,
                                        Name = ToTitleCase(title),
                                        Published = "1",
                                        IsFeatured = "0",
                                        VisibilityInCatalog = "visible",
                                        ShortDescription = "",
                                        Description = "<p>" + ToTitleCase(title) + "</p>" + des,
                                        DataSalePriceStarts = "",
                                        DataSalePriceEnds = "",
                                        TaxStatus = "taxable",
                                        TaxClass = "",
                                        InStock = "1",
                                        Stock = "",
                                        LowStockAmount = "",
                                        BackordersAllowed = "0",
                                        SoldIndividually = "0",
                                        Width = "",
                                        Length = "",
                                        Height = "",
                                        AllowCustomerReviews = "1",
                                        PurchaseNote = "",
                                        SalePrice = "",
                                        RegularPrice = "",
                                        Categories = categories,
                                        Tags = tags,
                                        ShippingClass = "",
                                        Images = url,
                                        Parent = "", //id parent
                                        GroupedProducts = "",
                                        Upsells = "",
                                        CrossSells = "",
                                        ExternalURL = "",
                                        ButtonText = "",
                                        Position = "",// idParent,
                                        SwatchesAttributes = "",
                                        Attribute1Global = "1",
                                        Attribute1Name = "Style",
                                        Attribute1Value = "Class T-Shirt, Crewneck Sweatshirt, Hoodie, Ladies T-Shirt",
                                        Attribute1Visible = "1",
                                        Attribute2Global = "1",
                                        Attribute2Name = "Size",
                                        Attribute2Value = "2XL, 3XL, 4XL, 5XL, L, M, S, XL",
                                        Attribute2Visible = "1",
                                        Attribute3Global = "1",
                                        Attribute3Name = "Color",
                                        Attribute3Value = "Athletic Heather, Black, Blue, Chocolate, Forest Green, Irish Green, Light Blue, Light Pink, Navy, Orange, Pink, Purple, Red, Sports Grey, White",
                                        Attribute3Visible = "1"
                                    };
                                    if (parent != null)
                                    {
                                        dsResult.Add(parent);
                                        #region Child
                                        //foreach (var itemLoai in DanhSachLoai)
                                        //{
                                        //    double price = itemLoai.Equals("Class T-Shirt") ? 19.95 : itemLoai.Equals("Crewneck Sweatshirt") ? 31.95 : itemLoai.Equals("Hoodie") ? 34.95 : 22.95;
                                        //    foreach (var itemMau in DanhSachMau)
                                        //    {
                                        //        foreach (var itemSize in DanhSachSize)
                                        //        {
                                        //            //Id
                                        //            double priceUp = price;
                                        //            if (itemSize.Equals("2XL") || itemSize.Equals("3XL") || itemSize.Equals("4XL") || itemSize.Equals("5XL"))
                                        //            {
                                        //                priceUp += 2;
                                        //            }
                                        //            var child = new Export()
                                        //            {
                                        //                ID = "",// id.ToString(),
                                        //                Type = "variation",
                                        //                SKU = "",
                                        //                Name = ToTitleCase(title),
                                        //                Published = "1",
                                        //                IsFeatured = "0",
                                        //                VisibilityInCatalog = "visible",
                                        //                ShortDescription = "",
                                        //                Description = "",
                                        //                DataSalePriceStarts = "",
                                        //                DataSalePriceEnds = "",
                                        //                TaxStatus = "taxable",
                                        //                TaxClass = "parent",
                                        //                InStock = "1",
                                        //                Stock = "",
                                        //                LowStockAmount = "",
                                        //                BackordersAllowed = "0",
                                        //                SoldIndividually = "0",
                                        //                Width = "",
                                        //                Length = "",
                                        //                Height = "",
                                        //                AllowCustomerReviews = "1",
                                        //                PurchaseNote = "",
                                        //                SalePrice = "",
                                        //                RegularPrice = priceUp.ToString(),
                                        //                Categories = categories,
                                        //                Tags = tags,
                                        //                ShippingClass = "",
                                        //                Images = url,
                                        //                Parent = parent.ID,
                                        //                GroupedProducts = "",
                                        //                Upsells = "",
                                        //                CrossSells = "",
                                        //                ExternalURL = "",
                                        //                ButtonText = "",
                                        //                Position = "", //id.ToString(),
                                        //                SwatchesAttributes = "",
                                        //                Attribute1Global = "1",
                                        //                Attribute1Name = "Style",
                                        //                Attribute1Value = itemLoai,
                                        //                Attribute1Visible = "",
                                        //                Attribute2Global = "1",
                                        //                Attribute2Name = "Size",
                                        //                Attribute2Value = itemSize,
                                        //                Attribute2Visible = "",
                                        //                Attribute3Global = "1",
                                        //                Attribute3Name = "Color",
                                        //                Attribute3Value = itemMau,
                                        //                Attribute3Visible = ""
                                        //            };
                                        //            if (child != null)
                                        //            {
                                        //                // FilesHandle.ID_GLOBAL++;
                                        //                dsResult.Add(child);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                        #endregion
                                        Console.WriteLine("Succeed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed - CODE-467");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Failed - CODE-472");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed - CODE-477");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed - CODE-482");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return dsResult;
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                if (dsResult.Count > 0)
                {
                    return dsResult;
                }
                return null;
            }
            else
            {
                Console.WriteLine("Failed - Link is not defined");
                ClearDriver();
                ClearGoogleChrome();
                return null;
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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
        public void Dowload(List<string> dsLink)
        {
            if (dsLink.Count > 0)
            {
                for (int j = 0; j < dsLink.Count; j++)
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
                                if (!String.IsNullOrEmpty(dsTitle.Text))
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

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Hoodie\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Sweatpants"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Sweatpants\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Sneaker"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Sneaker\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Phone Case"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Phone Case\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("T-Shirt"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\T-Shirt\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Jacket"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Jacket\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Jersey"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Jersey\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Low-Top"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Low Top\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Shade"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Shade\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Bedding Set"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Bedding Set\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else if (titleH.Text.Contains("Sweatshirt"))
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Sweatshirt\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
                                    }
                                    ///
                                }
                                else
                                {

                                    fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GifNest\\Other\\" + title;
                                    if (!Directory.Exists(fileSavePath))
                                    {
                                        Directory.CreateDirectory(fileSavePath);
                                    }
                                    else
                                    {
                                        FLAG = true;
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
                            }
                        }
                    }
                    Console.WriteLine(j);
                }
            }
            else
            {
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
