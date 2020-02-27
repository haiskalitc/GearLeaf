using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeherivar
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Xuất File data
            //string des = File.ReadAllText(Environment.CurrentDirectory + "\\des.txt");
            //string[] link = File.ReadAllLines(Environment.CurrentDirectory + "\\link.txt");
            //Crawler craw = new Crawler();
            //foreach (var item in link)
            //{
            //    Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
            //    Console.WriteLine("Waiting for dowload link " + item);
            //    var dsLink = craw.Get(item);
            //    if (dsLink.Count > 0)
            //    {
            //        var dsSanPham = craw.GetProduct(dsLink, item);
            //        if (dsSanPham.Count > 0)
            //        {
            //            var arr = item.Split('.');
            //            var str = arr[arr.Length - 2].Split('/')[1];
            //            Export(dsSanPham, str, 1, false);
            //        }Z
            //        else
            //        {
            //            Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
            //            Console.WriteLine("Link is not found!");
            //        }
            //    }
            //    else
            //    {
            //        Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
            //        Console.WriteLine("Link is not found!");
            //    }
            //}
            #endregion

            #region Xuất File import Teheerivar
            //FileHandle file = new FileHandle();
            //string[] paths = File.ReadAllLines(Environment.CurrentDirectory + "\\path.txt");
            //foreach (var path in paths)
            //{
            //    var arr = path.Split('\\');
            //    var name = arr[arr.Length - 1].Replace(".xlsx", "");
            //    var dsImport = file.GetProductFromFile(path);
            //    int row = 1;
            //    foreach (var item in dsImport)
            //    {
            //        Export(item, name, row, true);
            //        row++; D:\HaiNguyen\Code\GearLeaf\Teeherivar\Teeherivar\Program.cs
            //    }
            //}

            // Data T-Shirt
            //string path = @"D:\HaiNguyen\datashirt\datashirt";
            //FileHandle file = new FileHandle();
            //var dsImport = file.GetProductFromForlder(path);
            //Export(dsImport[8], "DataShirt", 1, true);
            //int row = 1;
            //foreach (var item in dsImport)
            //{
            //    row++;
            //}
            #endregion

            #region Xuất File GifNest
            GifNestHandle gifNest = new GifNestHandle();
            string name = "Sporty Sneaker";
            //123983
            //Round Carpet
            //Low Top
            //3D Hoodie
            var ds = gifNest.GetProductFromFileAquacozy(@"D:\HaiNguyen\Code\Aquacozy1_Fix\Aquacozys\" + name, name);
            if (ds.Count > 0)
            {
                //131956
                ExportGifNest(ds, name);
            }
            else
            {
                Console.WriteLine("Product is not found!");
            }
            #endregion
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
        //8 + 8 + 20 + 7

        public static void Export(List<Export> dsRe, string name, int index, bool isExport = false)
        {
            #region
            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet();
            var row1 = sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("ID");
            row1.CreateCell(1).SetCellValue("Type");
            row1.CreateCell(2).SetCellValue("SKU");
            row1.CreateCell(3).SetCellValue("Name");
            row1.CreateCell(4).SetCellValue("Published");
            row1.CreateCell(5).SetCellValue("Is featured?");
            row1.CreateCell(6).SetCellValue("Visibility in catalog");
            row1.CreateCell(7).SetCellValue("Short description");
            row1.CreateCell(8).SetCellValue("Description");
            row1.CreateCell(9).SetCellValue("Date sale price starts");
            row1.CreateCell(10).SetCellValue("Date sale price ends");
            row1.CreateCell(11).SetCellValue("Tax status");
            row1.CreateCell(12).SetCellValue("Tax class");
            row1.CreateCell(13).SetCellValue("In stock?");
            row1.CreateCell(14).SetCellValue("Stock");
            row1.CreateCell(15).SetCellValue("Low stock amount");
            row1.CreateCell(16).SetCellValue("Backorders allowed?");
            row1.CreateCell(17).SetCellValue("Sold individually?");
            row1.CreateCell(18).SetCellValue("Weight (lbs)");
            row1.CreateCell(19).SetCellValue("Length (cm)");
            row1.CreateCell(20).SetCellValue("Width (cm)");
            row1.CreateCell(21).SetCellValue("Height (cm)");
            row1.CreateCell(22).SetCellValue("Allow customer reviews?");
            row1.CreateCell(23).SetCellValue("Purchase note");
            row1.CreateCell(24).SetCellValue("Sale price");
            row1.CreateCell(25).SetCellValue("Regular price");
            row1.CreateCell(26).SetCellValue("Categories");
            row1.CreateCell(27).SetCellValue("Tags");
            row1.CreateCell(28).SetCellValue("Shipping class");
            row1.CreateCell(29).SetCellValue("Images");
            row1.CreateCell(30).SetCellValue("Download limit");
            row1.CreateCell(31).SetCellValue("Download expiry days");
            row1.CreateCell(32).SetCellValue("Parent");
            row1.CreateCell(33).SetCellValue("Grouped products");
            row1.CreateCell(34).SetCellValue("Upsells");
            row1.CreateCell(35).SetCellValue("Cross-sells");
            row1.CreateCell(36).SetCellValue("External URL");
            row1.CreateCell(37).SetCellValue("Button text");
            row1.CreateCell(38).SetCellValue("Position");
            row1.CreateCell(39).SetCellValue("Swatches Attributes");
            row1.CreateCell(40).SetCellValue("Attribute 1 name");
            row1.CreateCell(41).SetCellValue("Attribute 1 value(s)");
            row1.CreateCell(42).SetCellValue("Attribute 1 visible");
            row1.CreateCell(43).SetCellValue("Attribute 1 global");
            row1.CreateCell(44).SetCellValue("Attribute 2 name");
            row1.CreateCell(45).SetCellValue("Attribute 2 value(s)");
            row1.CreateCell(46).SetCellValue("Attribute 2 visible");
            row1.CreateCell(47).SetCellValue("Attribute 2 global");
            row1.CreateCell(48).SetCellValue("Attribute 3 name");
            row1.CreateCell(49).SetCellValue("Attribute 3 value(s)");
            row1.CreateCell(50).SetCellValue("Attribute 3 visible");
            row1.CreateCell(51).SetCellValue("Attribute 3 global");
            int rowIndex = 1;
            foreach (var item in dsRe)
            {
                var newRow = sheet.CreateRow(rowIndex);
                FillToRow(item, newRow);
                rowIndex++;
            }
            #endregion
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\DataShirt";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (isExport)
                {
                    string pathImport = path + "\\Import\\" + name;
                    if (!Directory.Exists(pathImport))
                    {
                        Directory.CreateDirectory(pathImport);
                    }
                    FileStream fs = new FileStream(pathImport + "\\" + index + ".xlsx", FileMode.CreateNew);
                    wb.Write(fs);
                    Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                    Console.WriteLine("Excel file was contained in your Desktop!");
                }
                else
                {
                    string pathProduct = path + "\\Product";
                    if (!Directory.Exists(pathProduct))
                    {
                        Directory.CreateDirectory(pathProduct);
                    }
                    FileStream fs = new FileStream(pathProduct + "\\" + name + ".xlsx", FileMode.CreateNew);
                    wb.Write(fs);
                    Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                    Console.WriteLine("Excel file was contained in your Desktop!");
                }

            }
            catch
            {
                Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                Console.WriteLine("Excel file was not exported!");
            }
            finally
            {
                GC.Collect();
            }
        }
        public static void ExportGifNest(List<Export> dsRe, string name)
        {
            #region
            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet();
            var row1 = sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("ID");
            row1.CreateCell(1).SetCellValue("Type");
            row1.CreateCell(2).SetCellValue("SKU");
            row1.CreateCell(3).SetCellValue("Name");
            row1.CreateCell(4).SetCellValue("Published");
            row1.CreateCell(5).SetCellValue("Is featured?");
            row1.CreateCell(6).SetCellValue("Visibility in catalog");
            row1.CreateCell(7).SetCellValue("Short description");
            row1.CreateCell(8).SetCellValue("Description");
            row1.CreateCell(9).SetCellValue("Date sale price starts");
            row1.CreateCell(10).SetCellValue("Date sale price ends");
            row1.CreateCell(11).SetCellValue("Tax status");
            row1.CreateCell(12).SetCellValue("Tax class");
            row1.CreateCell(13).SetCellValue("In stock?");
            row1.CreateCell(14).SetCellValue("Stock");
            row1.CreateCell(15).SetCellValue("Low stock amount");
            row1.CreateCell(16).SetCellValue("Backorders allowed?");
            row1.CreateCell(17).SetCellValue("Sold individually?");
            row1.CreateCell(18).SetCellValue("Weight (lbs)");
            row1.CreateCell(19).SetCellValue("Length (cm)");
            row1.CreateCell(20).SetCellValue("Width (cm)");
            row1.CreateCell(21).SetCellValue("Height (cm)");
            row1.CreateCell(22).SetCellValue("Allow customer reviews?");
            row1.CreateCell(23).SetCellValue("Purchase note");
            row1.CreateCell(24).SetCellValue("Sale price");
            row1.CreateCell(25).SetCellValue("Regular price");
            row1.CreateCell(26).SetCellValue("Categories");
            row1.CreateCell(27).SetCellValue("Tags");
            row1.CreateCell(28).SetCellValue("Shipping class");
            row1.CreateCell(29).SetCellValue("Images");
            row1.CreateCell(30).SetCellValue("Download limit");
            row1.CreateCell(31).SetCellValue("Download expiry days");
            row1.CreateCell(32).SetCellValue("Parent");
            row1.CreateCell(33).SetCellValue("Grouped products");
            row1.CreateCell(34).SetCellValue("Upsells");
            row1.CreateCell(35).SetCellValue("Cross-sells");
            row1.CreateCell(36).SetCellValue("External URL");
            row1.CreateCell(37).SetCellValue("Button text");
            row1.CreateCell(38).SetCellValue("Position");
            row1.CreateCell(39).SetCellValue("Swatches Attributes");
            if (name.Equals("Sneaker") || name.Equals("Boots") || name.Equals("Chunky Sneaker") || name.Equals("Leather Boots"))
            {
                row1.CreateCell(40).SetCellValue("Attribute 1 name");
                row1.CreateCell(41).SetCellValue("Attribute 1 value(s)");
                row1.CreateCell(42).SetCellValue("Attribute 1 visible");
                row1.CreateCell(43).SetCellValue("Attribute 1 global");
                row1.CreateCell(44).SetCellValue("Attribute 2 name");
                row1.CreateCell(45).SetCellValue("Attribute 2 value(s)");
                row1.CreateCell(46).SetCellValue("Attribute 2 visible");
                row1.CreateCell(47).SetCellValue("Attribute 2 global");
            }
            else if (!name.Equals("Shade") && !name.Equals("Car Seat") && !name.Equals("Shower Curtain") && !name.Equals("Umberllas"))
            {
                row1.CreateCell(40).SetCellValue("Attribute 1 name");
                row1.CreateCell(41).SetCellValue("Attribute 1 value(s)");
                row1.CreateCell(42).SetCellValue("Attribute 1 visible");
                row1.CreateCell(43).SetCellValue("Attribute 1 global");
            }
            int rowIndex = 1;
            foreach (var item in dsRe)
            {
                var newRow = sheet.CreateRow(rowIndex);
                FillToRowGifNest(item, newRow, name);
                rowIndex++;
            }
            #endregion
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Aquacozy Import";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FileStream fs = new FileStream(path + "\\" + name + ".xlsx", FileMode.CreateNew);
                wb.Write(fs);
                Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                Console.WriteLine("Excel file was contained in your Desktop!");
            }
            catch
            {
                Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                Console.WriteLine("Excel file was not exported!");
            }
            finally
            {
                GC.Collect();
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void FillToRowGifNest(Export ex, IRow row, string name)
        {
            int LENGHT = 0;
            if (name.Equals("Sneaker") || name.Equals("Boots") || name.Equals("Chunky Sneaker") || name.Equals("Leather Boots"))
            {
                LENGHT = 48;
            }
            else if (!name.Equals("Shade") && !name.Equals("Car Seat") && !name.Equals("Shower Curtain") && !name.Equals("Umberllas"))
            {
                LENGHT = 44;
            }
            else
            {
                LENGHT = 40;
            }
            for (int i = 0; i < LENGHT/*41*//*48*/; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            //Id
                            row.CreateCell(i).SetCellValue(ex.ID);
                            break;
                        }
                    case 1:
                        {
                            //Type
                            row.CreateCell(i).SetCellValue(ex.Type);
                            break;
                        }
                    case 2:
                        {
                            //SKU
                            row.CreateCell(i).SetCellValue(ex.SKU);
                            break;
                        }
                    case 3:
                        {
                            //Name
                            row.CreateCell(i).SetCellValue(ex.Name);
                            break;
                        }
                    case 4:
                        {
                            //Published
                            row.CreateCell(i).SetCellValue(ex.Published);
                            break;
                        }
                    case 5:
                        {
                            //Is featured?
                            row.CreateCell(i).SetCellValue(ex.IsFeatured);
                            break;
                        }
                    case 6:
                        {
                            //Visibility in catalog
                            row.CreateCell(i).SetCellValue(ex.VisibilityInCatalog);
                            break;
                        }
                    case 7:
                        {
                            //Short description
                            row.CreateCell(i).SetCellValue(ex.ShortDescription);
                            break;
                        }
                    case 8:
                        {
                            //Description
                            row.CreateCell(i).SetCellValue(ex.Description);
                            break;
                        }
                    case 9:
                        {
                            //Date sale price starts
                            row.CreateCell(i).SetCellValue(ex.DataSalePriceStarts);
                            break;
                        }
                    case 10:
                        {
                            //Date sale price ends
                            row.CreateCell(i).SetCellValue(ex.DataSalePriceEnds);
                            break;
                        }
                    case 11:
                        {
                            //Tax status
                            row.CreateCell(i).SetCellValue(ex.TaxStatus);
                            break;
                        }
                    case 12:
                        {
                            //Tax class
                            row.CreateCell(i).SetCellValue(ex.TaxClass);
                            break;
                        }
                    case 13:
                        {
                            //In stock?
                            row.CreateCell(i).SetCellValue(ex.InStock);
                            break;
                        }
                    case 14:
                        {
                            //Stock
                            row.CreateCell(i).SetCellValue(ex.Stock);
                            break;
                        }
                    case 15:
                        {
                            //Low stock amount
                            row.CreateCell(i).SetCellValue(ex.LowStockAmount);
                            break;
                        }
                    case 16:
                        {
                            //Backorders allowed?
                            row.CreateCell(i).SetCellValue(ex.BackordersAllowed);
                            break;
                        }
                    case 17:
                        {
                            //Sold individually?
                            row.CreateCell(i).SetCellValue(ex.SoldIndividually);
                            break;
                        }
                    case 18:
                        {
                            //Weight (lbs)
                            row.CreateCell(i).SetCellValue(ex.Weight);
                            break;
                        }
                    case 19:
                        {
                            //Length (cm)
                            row.CreateCell(i).SetCellValue(ex.Length);
                            break;
                        }
                    case 20:
                        {
                            //Width (cm)
                            row.CreateCell(i).SetCellValue(ex.Width);
                            break;
                        }
                    case 21:
                        {
                            //Height (cm)
                            row.CreateCell(i).SetCellValue(ex.Height);
                            break;
                        }
                    case 22:
                        {
                            //Allow customer reviews?
                            row.CreateCell(i).SetCellValue(ex.AllowCustomerReviews);
                            break;
                        }
                    case 23:
                        {
                            //Purchase note
                            row.CreateCell(i).SetCellValue(ex.PurchaseNote);
                            break;
                        }
                    case 24:
                        {
                            //Sale price
                            row.CreateCell(i).SetCellValue(ex.SalePrice);
                            break;
                        }
                    case 25:
                        {
                            //Regular price
                            row.CreateCell(i).SetCellValue(ex.RegularPrice);
                            break;
                        }
                    case 26:
                        {
                            //Categories
                            row.CreateCell(i).SetCellValue(ex.Categories);
                            break;
                        }
                    case 27:
                        {
                            //Tags
                            row.CreateCell(i).SetCellValue(ex.Tags);
                            break;
                        }
                    case 28:
                        {
                            //Shipping class
                            row.CreateCell(i).SetCellValue(ex.ShippingClass);
                            break;
                        }
                    case 29:
                        {
                            //Images
                            row.CreateCell(i).SetCellValue(ex.Images);
                            break;
                        }
                    case 30:
                        {
                            //Download limit
                            row.CreateCell(i).SetCellValue(ex.DownloadLimit);
                            break;
                        }
                    case 31:
                        {
                            //Download expiry days
                            row.CreateCell(i).SetCellValue(ex.DownloadExpiryDays);
                            break;
                        }
                    case 32:
                        {
                            //Parent
                            row.CreateCell(i).SetCellValue(ex.Parent);
                            break;
                        }
                    case 33:
                        {
                            //Grouped products
                            row.CreateCell(i).SetCellValue(ex.GroupedProducts);
                            break;
                        }
                    case 34:
                        {
                            //Upsells
                            row.CreateCell(i).SetCellValue(ex.Upsells);
                            break;
                        }
                    case 35:
                        {
                            //Cross-sells
                            row.CreateCell(i).SetCellValue(ex.CrossSells);
                            break;
                        }
                    case 36:
                        {
                            //External URL
                            row.CreateCell(i).SetCellValue(ex.ExternalURL);
                            break;
                        }
                    case 37:
                        {
                            //Button text
                            row.CreateCell(i).SetCellValue(ex.ButtonText);
                            break;
                        }
                    case 38:
                        {
                            //Position
                            row.CreateCell(i).SetCellValue(ex.Position);
                            break;
                        }
                    case 39:
                        {
                            //SwatchesAttributes
                            row.CreateCell(i).SetCellValue(ex.SwatchesAttributes);
                            break;
                        }

                    case 40:
                        {
                            //Attribute 1 name
                            //if (name != "Tumbler" || name != "Music Box")
                            {
                                row.CreateCell(i).SetCellValue(ex.Attribute1Name);
                            }
                            break;
                        }
                    case 41:
                        {
                            //Attribute 1 value(s)
                            //if (name != "Tumbler" || name != "Music Box")
                            row.CreateCell(i).SetCellValue(ex.Attribute1Value);
                            break;
                        }
                    case 42:
                        {
                            //Attribute 1 visible
                            //if (name != "Tumbler" || name != "Music Box")
                            row.CreateCell(i).SetCellValue(ex.Attribute1Visible);
                            break;
                        }
                    case 43:
                        {
                            ////Attribute 1 global
                            row.CreateCell(i).SetCellValue(ex.Attribute1Global);
                            break;
                        }

                    //    //Sneaker
                    case 44:
                        {
                            //Attribute 2 name
                            row.CreateCell(i).SetCellValue(ex.Attribute2Name);
                            break;
                        }
                    case 45:
                        {
                            //Attribute 2 value(s)
                            row.CreateCell(i).SetCellValue(ex.Attribute2Value);
                            break;
                        }
                    case 46:
                        {
                            //Attribute 2 visible
                            row.CreateCell(i).SetCellValue(ex.Attribute2Visible);
                            break;
                        }
                    case 47:
                        {
                            //Attribute 2 global
                            //if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                            row.CreateCell(i).SetCellValue(ex.Attribute2Global);
                            break;
                        }
                }
            }
        }
        public static void FillToRow(Export ex, IRow row)
        {
            for (int i = 0; i < 52; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            //Id
                            row.CreateCell(i).SetCellValue(ex.ID);
                            break;
                        }
                    case 1:
                        {
                            //Type
                            row.CreateCell(i).SetCellValue(ex.Type);
                            break;
                        }
                    case 2:
                        {
                            //SKU
                            row.CreateCell(i).SetCellValue(ex.SKU);
                            break;
                        }
                    case 3:
                        {
                            //Name
                            row.CreateCell(i).SetCellValue(ex.Name);
                            break;
                        }
                    case 4:
                        {
                            //Published
                            row.CreateCell(i).SetCellValue(ex.Published);
                            break;
                        }
                    case 5:
                        {
                            //Is featured?
                            row.CreateCell(i).SetCellValue(ex.IsFeatured);
                            break;
                        }
                    case 6:
                        {
                            //Visibility in catalog
                            row.CreateCell(i).SetCellValue(ex.VisibilityInCatalog);
                            break;
                        }
                    case 7:
                        {
                            //Short description
                            row.CreateCell(i).SetCellValue(ex.ShortDescription);
                            break;
                        }
                    case 8:
                        {
                            //Description
                            row.CreateCell(i).SetCellValue(ex.Description);
                            break;
                        }
                    case 9:
                        {
                            //Date sale price starts
                            row.CreateCell(i).SetCellValue(ex.DataSalePriceStarts);
                            break;
                        }
                    case 10:
                        {
                            //Date sale price ends
                            row.CreateCell(i).SetCellValue(ex.DataSalePriceEnds);
                            break;
                        }
                    case 11:
                        {
                            //Tax status
                            row.CreateCell(i).SetCellValue(ex.TaxStatus);
                            break;
                        }
                    case 12:
                        {
                            //Tax class
                            row.CreateCell(i).SetCellValue(ex.TaxClass);
                            break;
                        }
                    case 13:
                        {
                            //In stock?
                            row.CreateCell(i).SetCellValue(ex.InStock);
                            break;
                        }
                    case 14:
                        {
                            //Stock
                            row.CreateCell(i).SetCellValue(ex.Stock);
                            break;
                        }
                    case 15:
                        {
                            //Low stock amount
                            row.CreateCell(i).SetCellValue(ex.LowStockAmount);
                            break;
                        }
                    case 16:
                        {
                            //Backorders allowed?
                            row.CreateCell(i).SetCellValue(ex.BackordersAllowed);
                            break;
                        }
                    case 17:
                        {
                            //Sold individually?
                            row.CreateCell(i).SetCellValue(ex.SoldIndividually);
                            break;
                        }
                    case 18:
                        {
                            //Weight (lbs)
                            row.CreateCell(i).SetCellValue(ex.Weight);
                            break;
                        }
                    case 19:
                        {
                            //Length (cm)
                            row.CreateCell(i).SetCellValue(ex.Length);
                            break;
                        }
                    case 20:
                        {
                            //Width (cm)
                            row.CreateCell(i).SetCellValue(ex.Width);
                            break;
                        }
                    case 21:
                        {
                            //Height (cm)
                            row.CreateCell(i).SetCellValue(ex.Height);
                            break;
                        }
                    case 22:
                        {
                            //Allow customer reviews?
                            row.CreateCell(i).SetCellValue(ex.AllowCustomerReviews);
                            break;
                        }
                    case 23:
                        {
                            //Purchase note
                            row.CreateCell(i).SetCellValue(ex.PurchaseNote);
                            break;
                        }
                    case 24:
                        {
                            //Sale price
                            row.CreateCell(i).SetCellValue(ex.SalePrice);
                            break;
                        }
                    case 25:
                        {
                            //Regular price
                            row.CreateCell(i).SetCellValue(ex.RegularPrice);
                            break;
                        }
                    case 26:
                        {
                            //Categories
                            row.CreateCell(i).SetCellValue(ex.Categories);
                            break;
                        }
                    case 27:
                        {
                            //Tags
                            row.CreateCell(i).SetCellValue(ex.Tags);
                            break;
                        }
                    case 28:
                        {
                            //Shipping class
                            row.CreateCell(i).SetCellValue(ex.ShippingClass);
                            break;
                        }
                    case 29:
                        {
                            //Images
                            row.CreateCell(i).SetCellValue(ex.Images);
                            break;
                        }
                    case 30:
                        {
                            //Download limit
                            row.CreateCell(i).SetCellValue(ex.DownloadLimit);
                            break;
                        }
                    case 31:
                        {
                            //Download expiry days
                            row.CreateCell(i).SetCellValue(ex.DownloadExpiryDays);
                            break;
                        }
                    case 32:
                        {
                            //Parent
                            row.CreateCell(i).SetCellValue(ex.Parent);
                            break;
                        }
                    case 33:
                        {
                            //Grouped products
                            row.CreateCell(i).SetCellValue(ex.GroupedProducts);
                            break;
                        }
                    case 34:
                        {
                            //Upsells
                            row.CreateCell(i).SetCellValue(ex.Upsells);
                            break;
                        }
                    case 35:
                        {
                            //Cross-sells
                            row.CreateCell(i).SetCellValue(ex.CrossSells);
                            break;
                        }
                    case 36:
                        {
                            //External URL
                            row.CreateCell(i).SetCellValue(ex.ExternalURL);
                            break;
                        }
                    case 37:
                        {
                            //Button text
                            row.CreateCell(i).SetCellValue(ex.ButtonText);
                            break;
                        }
                    case 38:
                        {
                            //Position
                            row.CreateCell(i).SetCellValue(ex.Position);
                            break;
                        }
                    case 39:
                        {
                            //SwatchesAttributes
                            row.CreateCell(i).SetCellValue(ex.SwatchesAttributes);
                            break;
                        }

                    case 40:
                        {
                            //Attribute 1 name
                            //if (name != "Tumbler" || name != "Music Box")
                            {
                                row.CreateCell(i).SetCellValue(ex.Attribute1Name);
                            }
                            break;
                        }
                    case 41:
                        {
                            //Attribute 1 value(s)
                            //if (name != "Tumbler" || name != "Music Box")
                            row.CreateCell(i).SetCellValue(ex.Attribute1Value);
                            break;
                        }
                    case 42:
                        {
                            //Attribute 1 visible
                            //if (name != "Tumbler" || name != "Music Box")
                            row.CreateCell(i).SetCellValue(ex.Attribute1Visible);
                            break;
                        }
                    case 43:
                        {
                            ////Attribute 1 global
                            row.CreateCell(i).SetCellValue(ex.Attribute1Global);
                            break;
                        }
                    case 44:
                        {
                            //Attribute 2 name
                            row.CreateCell(i).SetCellValue(ex.Attribute2Name);
                            break;
                        }
                    case 45:
                        {
                            //Attribute 2 value(s)
                            row.CreateCell(i).SetCellValue(ex.Attribute2Value);
                            break;
                        }
                    case 46:
                        {
                            //Attribute 2 visible
                            row.CreateCell(i).SetCellValue(ex.Attribute2Visible);
                            break;
                        }
                    case 47:
                        {
                            //Attribute 2 global
                            //if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                            row.CreateCell(i).SetCellValue(ex.Attribute2Global);
                            break;
                        }
                    case 48:
                        {
                            //Attribute 3 name
                            row.CreateCell(i).SetCellValue(ex.Attribute3Name);
                            break;
                        }
                    case 49:
                        {
                            //Attribute 3 value(s)
                            row.CreateCell(i).SetCellValue(ex.Attribute3Value);
                            break;
                        }
                    case 50:
                        {
                            //Attribute 3 visible
                            row.CreateCell(i).SetCellValue(ex.Attribute3Visible);
                            break;
                        }
                    case 51:
                        {
                            //Attribute 3 global
                            row.CreateCell(i).SetCellValue(ex.Attribute3Global);
                            break;
                        }
                        }
                }
            }
    }
}