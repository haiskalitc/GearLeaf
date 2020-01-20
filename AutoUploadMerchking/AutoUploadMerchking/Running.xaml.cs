using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoUploadAmazonS3.Model;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace AutoUploadAmazonS3
{
    /// <summary>
    /// Interaction logic for Running.xaml
    /// </summary>
    /// 
    public partial class Running : Window
    {
        private List<RootExport> dsListExport;

        public event EventHandler Callback;
        public Running(List<RootExport> dsListExport)
        {
            InitializeComponent();
            this.dsListExport = dsListExport;
            Run();
        }
        public Running()
        {
            InitializeComponent();

        }

        public async void Run()
        {
            await Task.Run(() =>
            {

                foreach (var item in dsListExport)
                {
                    //bool isRun = true;
                    //int i = 0;
                    //while (isRun)
                    //{
                    //    await Task.Run(() =>
                    //    {
                    //        if (i < item.Link.Count)
                    //        {
                    //            UploadFileMPUHighLevelAPI.UploadFile(item.Link[i]);
                    //            this.Dispatcher.Invoke(() =>
                    //            {
                    //                rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //                rtbContent.ScrollToEnd();
                    //            });
                    //            i++;
                    //        }
                    //        else
                    //        {
                    //            isRun = false;
                    //        }
                    //        if (i + 1 < item.Link.Count)
                    //        {
                    //            UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 1]);
                    //            this.Dispatcher.Invoke(() =>
                    //            {
                    //                rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //                rtbContent.ScrollToEnd();
                    //            });
                    //            i++;
                    //        }
                    //        else
                    //        {
                    //            isRun = false;
                    //        }
                    //        if (i + 2 < item.Link.Count)
                    //        {
                    //            UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 2]);
                    //            this.Dispatcher.Invoke(() =>
                    //            {
                    //                rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //                rtbContent.ScrollToEnd();
                    //            });
                    //            i++;
                    //        }
                    //        else
                    //        {
                    //            isRun = false;
                    //        }
                    //        if (i + 3 < item.Link.Count)
                    //        {
                    //            UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 3]);
                    //            this.Dispatcher.Invoke(() =>
                    //            {
                    //                rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //                rtbContent.ScrollToEnd();
                    //            });
                    //            i++;
                    //        }
                    //        else
                    //        {
                    //            isRun = false;
                    //        }
                    //if (i + 4 < item.Link.Count)
                    //{
                    //    UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 4]);
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //    });
                    //    i++;
                    //}
                    //else
                    //{
                    //    isRun = false;
                    //}
                    //if (i + 5 < item.Link.Count)
                    //{
                    //    UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 5]);
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //    });
                    //    i++;
                    //}
                    //else
                    //{
                    //    isRun = false;
                    //}
                    //if (i + 6 < item.Link.Count)
                    //{
                    //    UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 6]);
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //    });
                    //    i++;
                    //}
                    //else
                    //{
                    //    isRun = false;
                    //}
                    //if (i + 7 < item.Link.Count)
                    //{
                    //    UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 7]);
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //    });
                    //    i++;
                    //}
                    //else
                    //{
                    //    isRun = false;
                    //}
                    //if (i + 8 < item.Link.Count)
                    //{
                    //    UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 8]);
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //    });
                    //    i++;
                    //}
                    //else
                    //{
                    //    isRun = false;
                    //}
                    //if (i + 9 < item.Link.Count)
                    //{
                    //    UploadFileMPUHighLevelAPI.UploadFile(item.Link[i + 9]);
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        rtbContent.AppendText("Up thành công " + item.Link[i] + Environment.NewLine);
                    //    });
                    //    i++;
                    //}
                    //else
                    //{
                    //    isRun = false;
                    //}
                    //    });
                    //}
                    Export(item.DanhSach, item.Name);
                }
            });
            MessageBox.Show("Thành công!!");
            Callback(this, new EventArgs());
        }

        public void Export(List<Export> dsRe, string name)
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
            if (name != "Tumbler" && name != "Music Box")
            {

                row1.CreateCell(40).SetCellValue("Attribute 1 name");
                row1.CreateCell(41).SetCellValue("Attribute 1 value(s)");
                row1.CreateCell(42).SetCellValue("Attribute 1 visible");
                row1.CreateCell(43).SetCellValue("Attribute 1 global");

                if (name.Contains("Sneaker"))
                {
                    row1.CreateCell(44).SetCellValue("Attribute 2 name");
                    row1.CreateCell(45).SetCellValue("Attribute 2 value(s)");
                    row1.CreateCell(46).SetCellValue("Attribute 2 visible");
                    row1.CreateCell(47).SetCellValue("Attribute 2 global");
                }
            }

            int rowIndex = 1;
            foreach (var item in dsRe)
            {
                var newRow = sheet.CreateRow(rowIndex);
                FillToRow(item, newRow, name);
                rowIndex++;
            }
            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + name +" "+ RandomString(5) + " " + DateTime.Now.ToLongDateString() + ".xlsx", FileMode.CreateNew);
            wb.Write(fs);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #region Fill data to Row
        public void FillToRow(Export ex, IRow row, string name)
        {
            for (int i = 0; i < 48; i++)
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
                            if (name != "Tumbler" || name != "Music Box")
                            {
                                row.CreateCell(i).SetCellValue(ex.Attribute1Name);
                            }
                            break;
                        }
                    case 41:
                        {
                            //Attribute 1 value(s)
                            if (name != "Tumbler" || name != "Music Box")
                                row.CreateCell(i).SetCellValue(ex.Attribute1Value);
                            break;
                        }
                    case 42:
                        {
                            //Attribute 1 visible
                            if (name != "Tumbler" || name != "Music Box")
                                row.CreateCell(i).SetCellValue(ex.Attribute1Visible);
                            break;
                        }
                    case 43:
                        {
                            //Attribute 1 global
                            if (name != "Tumbler" || name != "Music Box")
                                row.CreateCell(i).SetCellValue(ex.Attribute1Global);
                            break;
                        }
                    case 44:
                        {
                            //Attribute 2 name
                            if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                                row.CreateCell(i).SetCellValue(ex.Attribute2Name);
                            break;
                        }
                    case 45:
                        {
                            //Attribute 2 value(s)
                            if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                                row.CreateCell(i).SetCellValue(ex.Attribute2Value);
                            break;
                        }
                    case 46:
                        {
                            //Attribute 2 visible
                            if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                                row.CreateCell(i).SetCellValue(ex.Attribute2Visible);
                            break;
                        }
                    case 47:
                        {
                            //Attribute 2 global
                            if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                                row.CreateCell(i).SetCellValue(ex.Attribute2Global);
                            break;
                        }
                }
            }
        }
        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            Callback(this, new EventArgs());
        }
    }
}
