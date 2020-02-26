using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeherivar
{
   public class FileHandle
    {
        public static int _ID = 0;
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
        public List<string> DanhSachLoai = new List<string>() { "Classic T - Shirt", "Crewneck Sweatshirt", "Hoodie", "Ladies T - Shirt" };
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "AqBwCeDrEtFyGuHiIoJpKLaMsNdOfPgQhRjSkTlUzVxWcXvYbZn0m123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public List<List<Export>> GetProductFromForlder(string path)
        {
            var result = new List<List<Export>>();
            try
            {
                int id = int.Parse(File.ReadAllText(Environment.CurrentDirectory + "\\id.txt"));
                string des = File.ReadAllText(Environment.CurrentDirectory + "\\Description\\des.txt");
                Folder folder = new Folder(path);
                if (folder.SubFolders.Count > 0)
                {
                    foreach (Folder itemFolder in folder.SubFolders)
                    {
                        var ds = new List<Export>();
                        if (itemFolder.SubFolders.Count > 0)
                        {
                            //
                            foreach (Folder itemProduct in itemFolder.SubFolders)
                            {
                                //
                                if (itemProduct.Files.Count > 0)
                                {
                                    string image = "";
                                    foreach (FileInfo itemFile in itemProduct.Files)
                                    {
                                        if (!String.IsNullOrEmpty(image))
                                        {
                                            image += ", ";
                                        }
                                        image += "http://35.197.63.152/uploadcdn/datashirt/" +
                                            itemFolder.Name + "/" + itemProduct.Name + "/" + itemFile.Name;
                                    }

                                    image += ", " + "https://sizeallproduct.s3.amazonaws.com/Size+Chart/ladiestshirt.jpg" + ", " + "https://sizeallproduct.s3.amazonaws.com/Size+Chart/unisextshirt.jpg"
                                        +", " + "https://sizeallproduct.s3.amazonaws.com/Size+Chart/sweater.jpg" + ", " + "https://sizeallproduct.s3.amazonaws.com/Size+Chart/hoodies.jpg";
                                    var SKU = RandomString(20);
                                    var parent = new Export()
                                    {
                                        ID = id + "",
                                        Type = "variable",
                                        SKU = SKU,
                                        Name = ToTitleCase(itemProduct.Name),
                                        Published = "1",
                                        IsFeatured = "0",
                                        VisibilityInCatalog = "visible",
                                        ShortDescription = "",
                                        Description = "<p>" + ToTitleCase(itemProduct.Name) + "</p>" + des,
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
                                        Categories = ToTitleCase(itemFolder.Name),
                                        Tags = ToTitleCase(itemFolder.Name),
                                        ShippingClass = "",
                                        Images = image,
                                        Parent = "",
                                        GroupedProducts = "",
                                        Upsells = "",
                                        CrossSells = "",
                                        ExternalURL = "",
                                        ButtonText = "",
                                        Position = id + "",
                                        SwatchesAttributes = "",
                                        Attribute1Global = "1",
                                        Attribute1Name = "Style",
                                        Attribute1Value = "Classic T-Shirt, Crewneck Sweatshirt, Hoodie, Ladies T-Shirt",
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
                                        ds.Add(parent);
                                        id++;
                                        #region Child
                                        foreach (var itemLoai in DanhSachLoai)
                                        {
                                            double price = itemLoai.Equals("Class T-Shirt") ? 19.95 : itemLoai.Equals("Crewneck Sweatshirt") ? 31.95 : itemLoai.Equals("Hoodie") ? 34.95 : 22.95;
                                            foreach (var itemMau in DanhSachMau)
                                            {
                                                foreach (var itemSize in DanhSachSize)
                                                {
                                                    //Id
                                                    double priceUp = price;
                                                    if (itemSize.Equals("2XL") || itemSize.Equals("3XL") || itemSize.Equals("4XL") || itemSize.Equals("5XL"))
                                                    {
                                                        priceUp += 2;
                                                    }
                                                    var child = new Export()
                                                    {
                                                        ID = id + "",
                                                        Type = "variation",
                                                        SKU = "",
                                                        Name = parent.Name,
                                                        Published = "1",
                                                        IsFeatured = "0",
                                                        VisibilityInCatalog = "visible",
                                                        ShortDescription = "",
                                                        Description = "",
                                                        DataSalePriceStarts = "",
                                                        DataSalePriceEnds = "",
                                                        TaxStatus = "taxable",
                                                        TaxClass = "parent",
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
                                                        RegularPrice = priceUp.ToString(),
                                                        Categories = parent.Categories,
                                                        Tags = parent.Tags,
                                                        ShippingClass = "",
                                                        Images = parent.Images,
                                                        Parent = parent.SKU,
                                                        GroupedProducts = "",
                                                        Upsells = "",
                                                        CrossSells = "",
                                                        ExternalURL = "",
                                                        ButtonText = "",
                                                        Position = id + "",
                                                        SwatchesAttributes = "",
                                                        Attribute1Global = "1",
                                                        Attribute1Name = "Style",
                                                        Attribute1Value = itemLoai,
                                                        Attribute1Visible = "",
                                                        Attribute2Global = "1",
                                                        Attribute2Name = "Size",
                                                        Attribute2Value = itemSize,
                                                        Attribute2Visible = "",
                                                        Attribute3Global = "1",
                                                        Attribute3Name = "Color",
                                                        Attribute3Value = itemMau,
                                                        Attribute3Visible = ""
                                                    };
                                                    if (child != null)
                                                    {
                                                        id++;
                                                        ds.Add(child);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(itemProduct.FullPath + " has not files");
                                }
                            }
                            if (ds.Count > 0)
                            {
                                result.Add(ds);
                               // File.WriteAllText(Environment.CurrentDirectory + "\\id.txt", id.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Product is not exist");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Category is not exist");
                }
                return result;
            }
            catch
            {
                return result;
            }
            finally
            {
                GC.Collect();
            }
        }

        public List<List<Export>> GetProductFromFile(string path)
        {
            try
            {
                int id = int.Parse(File.ReadAllText(Environment.CurrentDirectory + "\\id.txt"));
                string des = File.ReadAllText(Environment.CurrentDirectory + "\\Description\\des.txt");
                var result = new List<List<Export>>();
                XSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new XSSFWorkbook(file);
                }
                ISheet sheet = hssfwb.GetSheetAt(0);
                for (int i = 0; i < 10; i++)
                {
                    var ds = new List<Export>();
                    for (int row = (100 * i) + 1; i != 9 ? row < 100 * (i + 1): row <= sheet.LastRowNum; row++)
                    {
                        var iRow = sheet.GetRow(row);
                        if (iRow != null)
                        {
                            var parent = new Export()
                            {
                                ID = id + "",
                                Type = "variable",
                                SKU = iRow.GetCell(2).StringCellValue,
                                Name = iRow.GetCell(3).StringCellValue,
                                Published = "1",
                                IsFeatured = "0",
                                VisibilityInCatalog = "visible",
                                ShortDescription = "",
                                Description = "<p>" + ToTitleCase(iRow.GetCell(3).StringCellValue) + "</p>" + des,
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
                                Categories = iRow.GetCell(26).StringCellValue,
                                Tags = iRow.GetCell(27).StringCellValue,
                                ShippingClass = "",
                                Images = iRow.GetCell(29).StringCellValue,
                                Parent = "",
                                GroupedProducts = "",
                                Upsells = "",
                                CrossSells = "",
                                ExternalURL = "",
                                ButtonText = "",
                                Position = id + "",
                                SwatchesAttributes = "",
                                Attribute1Global = "1",
                                Attribute1Name = "Style",
                                Attribute1Value = "Classic T-Shirt, Crewneck Sweatshirt, Hoodie, Ladies T-Shirt",
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
                                ds.Add(parent);
                                id++;
                                #region Child
                                foreach (var itemLoai in DanhSachLoai)
                                {
                                    double price = itemLoai.Equals("Class T-Shirt") ? 19.95 : itemLoai.Equals("Crewneck Sweatshirt") ? 31.95 : itemLoai.Equals("Hoodie") ? 34.95 : 22.95;
                                    foreach (var itemMau in DanhSachMau)
                                    {
                                        foreach (var itemSize in DanhSachSize)
                                        {
                                            //Id
                                            double priceUp = price;
                                            if (itemSize.Equals("2XL") || itemSize.Equals("3XL") || itemSize.Equals("4XL") || itemSize.Equals("5XL"))
                                            {
                                                priceUp += 2;
                                            }
                                            var child = new Export()
                                            {
                                                ID = id + "",
                                                Type = "variation",
                                                SKU = "",
                                                Name = parent.Name,
                                                Published = "1",
                                                IsFeatured = "0",
                                                VisibilityInCatalog = "visible",
                                                ShortDescription = "",
                                                Description = "",
                                                DataSalePriceStarts = "",
                                                DataSalePriceEnds = "",
                                                TaxStatus = "taxable",
                                                TaxClass = "parent",
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
                                                RegularPrice = priceUp.ToString(),
                                                Categories = parent.Categories,
                                                Tags = parent.Tags,
                                                ShippingClass = "",
                                                Images = parent.Images,
                                                Parent = parent.SKU,
                                                GroupedProducts = "",
                                                Upsells = "",
                                                CrossSells = "",
                                                ExternalURL = "",
                                                ButtonText = "",
                                                Position = id + "",
                                                SwatchesAttributes = "",
                                                Attribute1Global = "1",
                                                Attribute1Name = "Style",
                                                Attribute1Value = itemLoai,
                                                Attribute1Visible = "",
                                                Attribute2Global = "1",
                                                Attribute2Name = "Size",
                                                Attribute2Value = itemSize,
                                                Attribute2Visible = "",
                                                Attribute3Global = "1",
                                                Attribute3Name = "Color",
                                                Attribute3Value = itemMau,
                                                Attribute3Visible = ""
                                            };
                                            if (child != null)
                                            {
                                                id++;
                                                ds.Add(child);
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    if (ds.Count > 0)
                    {
                        result.Add(ds);
                        File.WriteAllText(Environment.CurrentDirectory + "\\id.txt", id.ToString());
                    }
                }
                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }
        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
