using NPOI.HSSF.UserModel;
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
        public List<string> DanhSachLoai = new List<string>() { "Class T - Shirt", "Crewneck Sweatshirt", "Hoodie", "Ladies T - Shirt" };
        public List<Export> GetProductFromFile(string path)
        {
            try
            {
                int id = int.Parse(File.ReadAllText(Environment.CurrentDirectory + "\\id.txt"));
                var ds = new List<Export>();
                XSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new XSSFWorkbook(file);
                }
                ISheet sheet = hssfwb.GetSheetAt(0);
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    var iRow = sheet.GetRow(row);
                    if (iRow != null)
                    {
                        var parent = new Export()
                        {
                            ID = id+"",
                            Type = "variable", // variation
                            SKU = iRow.GetCell(2).StringCellValue,
                            Name = iRow.GetCell(3).StringCellValue,
                            Published = "1",
                            IsFeatured = "0",
                            VisibilityInCatalog = "visible",
                            ShortDescription = "",
                            Description = iRow.GetCell(8).StringCellValue,
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
                                            ID = id+"",// id.ToString(),
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
                                            Position = "", //id.ToString(),
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
                File.WriteAllText(Environment.CurrentDirectory + "\\id.txt", id.ToString());
                return ds;
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
    }
}
