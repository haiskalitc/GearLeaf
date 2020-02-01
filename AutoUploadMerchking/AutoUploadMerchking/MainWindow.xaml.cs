using AutoUploadAmazonS3.Model;
using Handler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AutoUploadAmazonS3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Folder dsFolder = null;
        public List<RootExport> dsListExport = new List<RootExport>();
        public long ID_ = 0;
        const int dotsPerInch = 600;
        const double widthInInch = 10;
        const double heightInInch = 8;
        public MainWindow()
        {
            InitializeComponent();
            cbbLoaiSanPham.ItemsSource = Handler.CategoryHandle.getInstance.GetList();
            // KhoiTaoDes();
            var id = CountHandle.getInstance.FindElementsById(1);
            if (id != null)
            {
                ID_ = id.Count1;
            }
        }
        public void ReduceImage(string path, string source)
        {
            try
            {
                using (Bitmap bmp1 = new Bitmap(System.Drawing.Image.FromFile(source), (int)(2400), (int)(3200)))
                {
                    bmp1.SetResolution(dotsPerInch, dotsPerInch);
                    using (Graphics graphics = Graphics.FromImage(bmp1))
                    {
                        var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        var myEncoderParameters = new EncoderParameters(1);
                        myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, 100L);
                        bmp1.Save(path, jgpEncoder, myEncoderParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        #region HolifuckingShit
        //public void KhoiTaoDes()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    //stringBuilder.Append(" < html lang=\"en\">");
        //    stringBuilder.Append("<head>");
        //    stringBuilder.Append("<meta charset=\"utf - 8\">");
        //    stringBuilder.Append("<script src=\"https://cdn.ckeditor.com/4.13.1/standard/ckeditor.js\"></script></head>");
        //    stringBuilder.Append("<body>");
        //    stringBuilder.Append("<textarea name=\"editor1\" id=\"editor1\" rows=\"10\" cols=\"80\"></textarea>");
        //    stringBuilder.Append("<script>CKEDITOR.replace( 'editor1' );</script>");
        //    stringBuilder.Append("</body>");
        //    //stringBuilder.Append("</html>");

        //    WindowsFormsHost host = new WindowsFormsHost();
        //    GeckoWebBrowser browser = new GeckoWebBrowser { Dock = DockStyle.Fill };
        //    host.Child = browser;
        //    description.Children.Add(host);
        //    browser.LoadHtml(stringBuilder.ToString());
        //}
        #endregion
        private void cbbLoaiSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                danhSachSize.Children.Clear();
                var item = cbbLoaiSanPham.SelectedItem as Category;
                if (item != null)
                {
                    txtDes.Text = item.Description;
                    if (item.Name.Contains("Tumbler") && item.Name.Contains("Music Box"))
                    {
                        StackPanel stack = new StackPanel()
                        {
                            Height = 50,
                            Margin = new Thickness(5),
                            Orientation = System.Windows.Controls.Orientation.Horizontal,
                        };
                        System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox()
                        {
                            Content = "Rengular Price",
                            Margin = new Thickness(5),
                            IsChecked = true,
                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                        };
                        System.Windows.Controls.TextBox text = new System.Windows.Controls.TextBox()
                        {
                            Text = "0",
                            Width = 80,
                            Height = 25,
                            VerticalAlignment = System.Windows.VerticalAlignment.Center
                        };
                        System.Windows.Controls.TextBlock textBlock = new System.Windows.Controls.TextBlock()
                        {
                            Margin = new Thickness(5, 0, 0, 0),
                            Width = 100,
                            Height = 25,
                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                            Text = "$"
                        };
                        stack.Children.Add(checkBox);
                        stack.Children.Add(text);
                        stack.Children.Add(textBlock);
                        danhSachSize.Children.Add(stack);
                    }
                    else
                    {
                        var sizeItem = Handler.SizeHandle.getInstance.FindElementsById(item.Id.ToString());
                        if (sizeItem != null)
                        {
                            var dsSizeTemp = sizeItem.Name.Split('|');
                            if (dsSizeTemp.Count() > 1)
                            {
                                foreach (var item2 in dsSizeTemp)
                                {
                                    var dsSize = item2.Split(',');
                                    foreach (var size in dsSize)
                                    {

                                        StackPanel stack = new StackPanel()
                                        {
                                            Height = 20,
                                            Margin = new Thickness(5),
                                            Orientation = System.Windows.Controls.Orientation.Horizontal,
                                        };
                                        var contentLabel = size.Trim().Split(' ');
                                        if (contentLabel.Count() > 2)
                                        {
                                            System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox()
                                            {
                                                Content = contentLabel[0] + " " + contentLabel[1],
                                                IsChecked = true,
                                                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                            };
                                            System.Windows.Controls.TextBox text = new System.Windows.Controls.TextBox()
                                            {
                                                Text = contentLabel[2],
                                                Width = 80,
                                                Height = 20,
                                                VerticalAlignment = System.Windows.VerticalAlignment.Center
                                            };
                                            System.Windows.Controls.TextBlock textBlock = new System.Windows.Controls.TextBlock()
                                            {
                                                Width = 100,
                                                Height = 20,
                                                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                                Text = "$"
                                            };
                                            stack.Children.Add(checkBox);
                                            stack.Children.Add(text);
                                            stack.Children.Add(textBlock);
                                            danhSachSize.Children.Add(stack);
                                        }
                                    }
                                    danhSachSize.Children.Add(new TextBlock() { Text = "-----------------------------------------" });
                                }
                            }
                            else
                            {
                                var dsSize = sizeItem.Name.Split(',');
                                foreach (var size in dsSize)
                                {

                                    StackPanel stack = new StackPanel()
                                    {
                                        Orientation = System.Windows.Controls.Orientation.Horizontal,
                                        Margin = new Thickness(5),
                                    };
                                    var contentLabel = size.Trim().Split(' ');
                                    if (contentLabel.Count() > 2)
                                    {
                                        System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox()
                                        {
                                            Content = contentLabel[0] + " " + contentLabel[1],
                                            Margin = new Thickness(5),
                                            IsChecked = true,
                                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                        };
                                        System.Windows.Controls.TextBox text = new System.Windows.Controls.TextBox()
                                        {
                                            Text = contentLabel[2],
                                            Width = 80,
                                            Height = 25,
                                            VerticalAlignment = System.Windows.VerticalAlignment.Center
                                        };
                                        System.Windows.Controls.TextBlock textBlock = new System.Windows.Controls.TextBlock()
                                        {
                                            Margin = new Thickness(5, 0, 0, 0),
                                            Width = 100,
                                            Height = 25,
                                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                            Text = "$"
                                        };
                                        stack.Children.Add(checkBox);
                                        stack.Children.Add(text);
                                        stack.Children.Add(textBlock);
                                        danhSachSize.Children.Add(stack);


                                    }
                                    else if (contentLabel.Count() > 1)
                                    {

                                        System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox()
                                        {
                                            Content = contentLabel[0],
                                            Margin = new Thickness(5),
                                            IsChecked = true,
                                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                        };
                                        System.Windows.Controls.TextBox text = new System.Windows.Controls.TextBox()
                                        {
                                            Text = contentLabel[1],
                                            Width = 80,
                                            Height = 25,
                                            VerticalAlignment = System.Windows.VerticalAlignment.Center
                                        };
                                        System.Windows.Controls.TextBlock textBlock = new System.Windows.Controls.TextBlock()
                                        {
                                            Margin = new Thickness(5, 0, 0, 0),
                                            Width = 100,
                                            Height = 25,
                                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                                            Text = "$"
                                        };
                                        stack.Children.Add(checkBox);
                                        stack.Children.Add(text);
                                        stack.Children.Add(textBlock);
                                        danhSachSize.Children.Add(stack);

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
            finally
            {
                GC.Collect();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = folderDlg.SelectedPath;
                var str = txtPath.Text.Split('\\');
                trvItemFile.Header = String.IsNullOrEmpty(str[str.Length - 1]) ? str[0] : str[str.Length - 1];
                dsFolder = new Folder(folderDlg.SelectedPath);
                trvFiles.DataContext = dsFolder;
            }
        }
        public RootExport GetList(Folder folder)
        {
            var ds = new List<Export>();
            var dsLink = new List<string>();
            try
            {
                foreach (Folder itemFolder in folder.SubFolders)
                {
                    string _folderName = GetName(folder.Name);
                    if (itemFolder.Files.Count > 0)
                    {
                        {
                            // Task.Run(() => 
                            //{
                            //    UploadFileMPUHighLevelAPI.UploadFile(itemFile.FullName);
                            //});   
                            string URL = "";
                            foreach (FileInfo itemFileUrl in itemFolder.Files)
                            {
                                if (!String.IsNullOrEmpty(URL))
                                {
                                    URL += ", ";
                                }
                               URL += txtServer.Text.Trim() + _folderName.Replace(" ", "%20") + "/"
                                                                                     + GetName(itemFolder.Name).Replace(" ", "%20") + "/"
                                                                                     + (itemFileUrl.Name).Replace(" ", "%20");//+ 3
                            }
                            //dsLink.Add(itemFile.FullName);
                            string IdParent = ID_.ToString();
                            string size = "";
                            var Gender = "Men";
                            var Tumbler = "";
                            var dsChildRen = new List<Export>();
                            var dsChildRenWomen = new List<Export>();
                            foreach (var item in danhSachSize.Children)
                            {
                                var stack = item as StackPanel;
                                if (stack != null)
                                {
                                    var itemCheck = stack.Children[0] as System.Windows.Controls.CheckBox;
                                    if (itemCheck != null)
                                    {
                                        if (itemCheck.IsChecked == true)
                                        {
                                            var itemText = stack.Children[1] as System.Windows.Controls.TextBox;
                                            if (itemText != null)
                                            {
                                                ID_++;
                                                if (_folderName.Contains("Sneaker"))
                                                {
                                                    //US5 (EU37.5)
                                                    if (itemCheck.Content.ToString().Trim().Equals("US12 (EU45)") || itemCheck.Content.ToString().Trim().Equals("US5 (EU37.5)")
                                                        || itemCheck.Content.ToString().Trim().Equals("US6 (EU39)"))
                                                    {
                                                        Gender = "Women";
                                                    }

                                                    var exportChild = new Export()
                                                    {
                                                        ID = ID_.ToString(),
                                                        Type = "variation",
                                                        Name = GetName(itemFolder.Name).Replace(".jpeg", "").Replace(".png", "") + " - " + Gender + ", " + itemCheck.Content.ToString(),
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
                                                        RegularPrice = String.IsNullOrEmpty(itemText.Text) ? "" : itemText.Text,
                                                        Categories = "",
                                                        Tags = "",
                                                        ShippingClass = "",
                                                        Images = URL,
                                                        Parent = "id:" + IdParent,
                                                        GroupedProducts = "",
                                                        Upsells = "",
                                                        CrossSells = "",
                                                        ExternalURL = "",
                                                        ButtonText = "",
                                                        Position = "0",
                                                        SwatchesAttributes = "",
                                                        Attribute1Global = "0",
                                                        Attribute1Name = "Style",
                                                        Attribute1Value = Gender,
                                                        Attribute1Visible = "",
                                                        Attribute2Global = "0",
                                                        Attribute2Name = "Size",
                                                        Attribute2Value = itemCheck.Content.ToString(),
                                                        Attribute2Visible = ""
                                                    };


                                                    if (exportChild != null)
                                                    {
                                                        if (!String.IsNullOrEmpty(exportChild.RegularPrice))
                                                        {
                                                            dsChildRen.Add(exportChild);
                                                            size += itemCheck.Content.ToString() + ", ";
                                                        }
                                                    }
                                                }
                                                else if (!_folderName.Contains("Tumbler") && !_folderName.Contains("Music Box"))
                                                {
                                                    var exportChild = new Export()
                                                    {
                                                        ID = ID_.ToString(),
                                                        Type = "variation",
                                                        Name = GetName(itemFolder.Name).Replace(".jpeg", "").Replace(".png", "") + " - " + itemCheck.Content.ToString(),
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
                                                        AllowCustomerReviews = "0",
                                                        PurchaseNote = "",
                                                        SalePrice = "",
                                                        RegularPrice = String.IsNullOrEmpty(itemText.Text) ? "" : itemText.Text,
                                                        Categories = "",
                                                        Tags = "",
                                                        ShippingClass = "",
                                                        Images = URL,
                                                        Parent = "id:" + IdParent,
                                                        GroupedProducts = "",
                                                        Upsells = "",
                                                        CrossSells = "",
                                                        ExternalURL = "",
                                                        ButtonText = "",
                                                        Position = "0",
                                                        SwatchesAttributes = "",
                                                        Attribute1Global = "0",
                                                        Attribute1Name = "Size",
                                                        Attribute1Value = itemCheck.Content.ToString(),
                                                        Attribute1Visible = "",
                                                        Attribute2Global = "0",
                                                        Attribute2Name = "Size",
                                                        Attribute2Value = itemCheck.Content.ToString(),
                                                        Attribute2Visible = ""
                                                    };


                                                    if (exportChild != null)
                                                    {
                                                        if (!String.IsNullOrEmpty(exportChild.RegularPrice))
                                                        {
                                                            dsChildRen.Add(exportChild);
                                                            size += itemCheck.Content.ToString() + ", ";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Tumbler = itemText.Text;
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                            var export = new Export()
                            {
                                ID = IdParent,
                                Type = "variable",
                                Name = GetName(itemFolder.Name).Replace(".jpeg", "").Replace(".png", ""),
                                Published = "1",
                                IsFeatured = "0",
                                VisibilityInCatalog = "visible",
                                ShortDescription = "",
                                Description = txtDes.Text,
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
                                RegularPrice = (_folderName.Contains("Tumbler") || _folderName.Contains("Music Box")) ? Tumbler : "",
                                Categories = GetName(itemFolder.Name),
                                Tags = "",
                                ShippingClass = "",
                                Images = URL,
                                Parent = "",
                                GroupedProducts = "",
                                Upsells = "",
                                CrossSells = "",
                                ExternalURL = "",
                                ButtonText = "",
                                Position = "0",
                                SwatchesAttributes = "",
                                Attribute1Global = "0",
                                Attribute1Name = _folderName.Contains("Sneaker") ? "Style" : "Size",
                                Attribute1Value = _folderName.Contains("Sneaker") ? "Men, Women" : size,
                                Attribute1Visible = "1",
                                Attribute2Value = size,
                                Attribute2Global = "0",
                                Attribute2Name = "Size",
                                Attribute2Visible = "1"
                            };
                            if (!_folderName.Contains("Tumbler") && !_folderName.Contains("Music Box"))
                            {
                                if (dsChildRen.Count > 0 && export != null)
                                {
                                    ds.Add(export);
                                    ds.AddRange(dsChildRen);
                                    if (_folderName.Contains("Sneaker"))
                                    {
                                        if (dsChildRenWomen.Count > 0)
                                        {
                                            ds.AddRange(dsChildRenWomen);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ds.Add(export);
                            }

                            ID_++;
                        }
                    }
                }


                CountHandle.getInstance.Update(ID_, 1);
                return new RootExport()
                {
                    Name = (folder.Name),
                    DanhSach = ds,
                    Link = dsLink
                };
            }
            catch
            {
                return new RootExport()
                {
                    Name = (folder.Name),
                    DanhSach = ds,
                    Link = dsLink
                };
            }
            finally
            {
                GC.Collect();
            }
        }

        #region My Chi
        //public void LamBieng()
        //{
        //    using (CsvReader csv = new CsvReader(new StreamReader(@"C:\Users\Administrator\Downloads\wc-product-export-27-12-2019-1577456873566.csv"), true))
        //    {
        //        int fieldCount = csv.FieldCount;

        //        string[] headers = csv.GetFieldHeaders();
        //        string s = "";

        //        for (int i = 0; i < headers.Length - 1; i++)
        //        {
        //            s += "row1.CreateCell(" + i + ").SetCellValue(\"" + headers[i] + "\");" + Environment.NewLine;
        //        }
        //        while (csv.ReadNextRecord())
        //        {
        //        }
        //    }
        //}
        #endregion

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dsListExport != null)
                {
                    if (dsListExport.Count > 0)
                    {
                        //foreach (var item in dsListExport)
                        //{
                        //    await Task.Run(() => 
                        //    {
                        //        Export(item.DanhSach, item.Name);
                        //    }); 
                        //}
                        //System.Windows.MessageBox.Show("Xong ne");
                        Running run = new Running(dsListExport);
                        run.Show();
                        this.Hide();
                        run.Callback += (ee, args) =>
                        {
                            (ee as Running).Close();
                            dsListExport.Clear();
                            txtDes.Text = "";
                            trvFiles.DataContext = null;
                            this.Show();
                        };

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Chưa có dữ liệu");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Chưa có dữ liệu");
                }
            }
            catch (Exception ecx)
            {
            }
            finally
            {
                GC.Collect();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dsFolder != null)
                {
                    var ds = GetList(dsFolder);
                    if (ds.DanhSach.Count > 0)
                    {
                        dsListExport.Add(ds);
                        dsFolder = null;
                        txtPath.Text = "";
                        txtDes.Text = "";
                        trvFiles.DataContext = dsFolder;
                        System.Windows.MessageBox.Show("Thêm thành công " + ds.Name);
                        string size = "";
                        foreach (var item in danhSachSize.Children)
                        {
                            var stack = item as StackPanel;
                            if (stack != null)
                            {
                                var itemCheck = stack.Children[0] as System.Windows.Controls.CheckBox;
                                var itemText = stack.Children[1] as System.Windows.Controls.TextBox;
                                if (itemCheck != null && itemText != null)
                                {
                                    if (itemCheck.Content.ToString().Trim().Equals("US12 (EU45)") || itemCheck.Content.ToString().Trim().Equals("US5 (EU37.5)")
                                        || itemCheck.Content.ToString().Trim().Equals("US6 (EU39)"))
                                    {
                                        size += itemCheck.Content + " " + itemText.Text + "|";

                                    }
                                    else
                                    {
                                        size += itemCheck.Content + " " + itemText.Text + ", ";
                                    }
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(size))
                        {
                            var item = cbbLoaiSanPham.SelectedItem as Category;
                            if (item != null)
                            {
                                SizeHandle.getInstance.Update(item.Id.ToString(), size);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Không có sản phẩm nào, có thể do chưa nhập giá");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Chưa chọn folder");
                }
            }
            catch { }
            finally
            {
                GC.Collect();
            }
        }

        public string GetName(string name)
        {
            string re = "";
            var ar = name.Split(' ');
            for (int i = 0; i < ar.Length - 1; i++)
            {
                if (!String.IsNullOrEmpty(re))
                {
                    re += " ";
                }
                re += ar[i];
            }
            return re;
        } 

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            CountHandle.getInstance.Update(1, 1);
        }
    }
}
