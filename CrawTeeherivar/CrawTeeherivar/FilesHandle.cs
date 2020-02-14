using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawTeeherivar
{
    public class FilesHandle
    {
        public static int ID_GLOBAL = 0;
        public static void Export(List<Export> dsRe, string name)
        {
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
            row1.CreateCell(18).SetCellValue("Weight (kg)");
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
            try
            {
                FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + name + " " + RandomString(5) + " " + DateTime.Now.ToLongDateString() + ".xlsx", FileMode.CreateNew);
                wb.Write(fs);
                Console.WriteLine("Excel file was contained in your Desktop!");
            }
            catch
            {
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
        public static void FillToRow(Export ex, IRow row)
        {
            try
            {
                //Id
                row.CreateCell(0).SetCellValue(ex.ID);
                //Type
                row.CreateCell(1).SetCellValue(ex.Type);
                //SKU
                row.CreateCell(2).SetCellValue(ex.SKU);
                //Name
                row.CreateCell(3).SetCellValue(ex.Name);
                //Published
                row.CreateCell(4).SetCellValue(ex.Published);
                //Is featured?
                row.CreateCell(5).SetCellValue(ex.IsFeatured);
                //Visibility in catalog
                row.CreateCell(6).SetCellValue(ex.VisibilityInCatalog);
                //Short description
                row.CreateCell(7).SetCellValue(ex.ShortDescription);
                //Description
                row.CreateCell(8).SetCellValue(ex.Description);
                //Date sale price starts
                row.CreateCell(9).SetCellValue(ex.DataSalePriceStarts);
                //Date sale price ends
                row.CreateCell(10).SetCellValue(ex.DataSalePriceEnds);
                //Tax status
                row.CreateCell(11).SetCellValue(ex.TaxStatus);
                //Tax class
                row.CreateCell(12).SetCellValue(ex.TaxClass);
                //In stock?
                row.CreateCell(13).SetCellValue(ex.InStock);
                //Stock
                row.CreateCell(14).SetCellValue(ex.Stock);
                //Low stock amount
                row.CreateCell(15).SetCellValue(ex.LowStockAmount);
                //Backorders allowed?
                row.CreateCell(16).SetCellValue(ex.BackordersAllowed);
                //Sold individually?
                row.CreateCell(17).SetCellValue(ex.SoldIndividually);
                //Weight (kg)
                row.CreateCell(18).SetCellValue(ex.Weight);
                //Length (cm)
                row.CreateCell(19).SetCellValue(ex.Length);
                //Width (cm)
                row.CreateCell(20).SetCellValue(ex.Width);
                //Height (cm)
                row.CreateCell(21).SetCellValue(ex.Height);
                //Allow customer reviews?
                row.CreateCell(22).SetCellValue(ex.AllowCustomerReviews);
                //Purchase note
                row.CreateCell(23).SetCellValue(ex.PurchaseNote);
                row.CreateCell(24).SetCellValue(ex.SalePrice);
                //Regular price
                row.CreateCell(25).SetCellValue(ex.RegularPrice);
                //Categories
                row.CreateCell(26).SetCellValue(ex.Categories);
                //Tags
                row.CreateCell(27).SetCellValue(ex.Tags);
                //Shipping class
                row.CreateCell(28).SetCellValue(ex.ShippingClass);
                //Images
                row.CreateCell(29).SetCellValue(ex.Images);
                //Download limit
                row.CreateCell(30).SetCellValue(ex.DownloadLimit);
                //Download expiry days
                row.CreateCell(31).SetCellValue(ex.DownloadExpiryDays);
                //Parent
                row.CreateCell(32).SetCellValue(ex.Parent);
                //Grouped products
                row.CreateCell(33).SetCellValue(ex.GroupedProducts);
                //Upsells
                row.CreateCell(34).SetCellValue(ex.Upsells);
                //Cross-sells
                row.CreateCell(35).SetCellValue(ex.CrossSells);
                //External URL
                row.CreateCell(36).SetCellValue(ex.ExternalURL);
                //Button text
                row.CreateCell(37).SetCellValue(ex.ButtonText);
                //Position
                row.CreateCell(38).SetCellValue(ex.Position);
                //SwatchesAttributes
                row.CreateCell(39).SetCellValue(ex.SwatchesAttributes);

                row.CreateCell(40).SetCellValue(ex.Attribute1Name);
                row.CreateCell(41).SetCellValue(ex.Attribute1Value);
                row.CreateCell(42).SetCellValue(ex.Attribute1Visible);
                row.CreateCell(43).SetCellValue(ex.Attribute1Global);

                row.CreateCell(44).SetCellValue(ex.Attribute2Name);
                row.CreateCell(45).SetCellValue(ex.Attribute2Value);
                row.CreateCell(46).SetCellValue(ex.Attribute2Visible);
                row.CreateCell(47).SetCellValue(ex.Attribute2Global);

                row.CreateCell(48).SetCellValue(ex.Attribute3Name);
                row.CreateCell(49).SetCellValue(ex.Attribute3Value);
                row.CreateCell(50).SetCellValue(ex.Attribute3Visible);
                row.CreateCell(51).SetCellValue(ex.Attribute3Global);
            }
            catch
            { }
            finally
            {
                GC.Collect();
            }
        }
    }
}
