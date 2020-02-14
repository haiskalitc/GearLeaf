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
            btnEditSize.IsEnabled = true;
            btnSaveSize.IsEnabled = false;
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
        //public void KhoiTaoDanhSachMau()
        //{
        //    var ds = Handler.ColorsHandle.getInstance.GetList();
        //    if (ds.Count() > 0)
        //    {
        //        try
        //        {
        //            var bc = new BrushConverter();
        //            foreach (var item in ds)
        //            {
        //                System.Windows.Controls.CheckBox check = new System.Windows.Controls.CheckBox()
        //                {
        //                    Margin = new Thickness(5),
        //                    Content = item.Name,
        //                    Foreground = System.Windows.Media.Brushes.Black,
        //                    Background = (System.Windows.Media.Brush)bc.ConvertFrom(item.Hexa),

        //                };
        //                if (check != null)
        //                {
        //                    dsMau.Children.Add(check);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        finally
        //        {
        //            GC.Collect();
        //        }
        //    }
        //}

        private void cbbLoaiSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                danhSachSize.Children.Clear();
                stackSizeChart.Children.Clear();
                var item = cbbLoaiSanPham.SelectedItem as Category;
                if (item != null)
                {
                    // SIze chart
                    var dsSizeChart = item.SizeChart.Split(',');
                    if (dsSizeChart.Count() > 0)
                    {
                        //
                        foreach (var itemSize in dsSizeChart)
                        {
                            var itemTextSize = new System.Windows.Controls.TextBox()
                            {
                                Width = 300,
                                Height = 30,
                                Text = itemSize
                            };
                            var stack = new StackPanel()
                            {
                                Orientation = System.Windows.Controls.Orientation.Horizontal,
                                Margin = new Thickness(0, 10, 0, 0)
                                
                            };
                            stack.Children.Add(itemTextSize);
                            if (stack != null)
                            {
                                stackSizeChart.Children.Add(stack);
                            }
                        }
                    }

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
                            IsEnabled = false,
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
                                                IsEnabled = false,
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
                                            IsEnabled = false,
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
                                            IsEnabled = false,
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
        public string pathID = Environment.CurrentDirectory + "\\id.txt";
        public string path = Environment.CurrentDirectory + "\\link.txt";
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\db.txt");
                foreach (string line in lines)
                    Console.WriteLine("\t\t\t" + line);
                string[] text = File.ReadAllLines(path);
                FilesHandle.ID_GLOBAL = int.Parse(File.ReadAllText(pathID));
                if (text.Count() > 0)
                {
                    Crawler craw = new Crawler();
                    foreach (var link in text)
                    {
                        var dsLink = craw.Get(link);
                        var ds = craw.GetProduct(dsLink);
                        if (ds.Count > 0)
                        {
                            dsListExport.Add(new RootExport() { DanhSach = ds});
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Thất bại - KHÔNG TÌM THẤY LINK XML");
                }
            });

            //System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            //folderDlg.ShowNewFolderButton = true;
            //// Show the FolderBrowserDialog.  
            //DialogResult result = folderDlg.ShowDialog();
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    txtPath.Text = folderDlg.SelectedPath;
            //    var str = txtPath.Text.Split('\\');
            //    trvItemFile.Header = String.IsNullOrEmpty(str[str.Length - 1]) ? str[0] : str[str.Length - 1];
            //    dsFolder = new Folder(folderDlg.SelectedPath);
            //    trvFiles.DataContext = dsFolder;
            //}
        }
        public RootExport GetList(Folder folder)
        {
            var ds = new List<Export>();
            var dsLink = new List<string>();
            try
            {
                foreach (Folder itemFolder in folder.SubFolders)
                {
                    if (itemFolder.Files.Count > 0)
                    {
                        foreach (FileInfo itemFile in itemFolder.Files)
                        {
                            // Task.Run(() => 
                            //{
                            //    UploadFileMPUHighLevelAPI.UploadFile(itemFile.FullName);
                            //});   
                            string URL = txtServer.Text.Trim() + folder.Name.Replace(" ", "%20") + "/"
                                                                                     + itemFolder.Name.Replace(" ", "%20") + "/"
                                                                                     + itemFile.Name.Replace(" ", "%20");
                            dsLink.Add(itemFile.FullName);
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
                                                if (folder.Name.Contains("Sneaker"))
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
                                                        Name = itemFile.Name.Replace(".jpeg", "").Replace(".png", "") + " - " + Gender + ", " + itemCheck.Content.ToString(),
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
                                                else if (!folder.Name.Contains("Tumbler") && !folder.Name.Contains("Music Box"))
                                                {
                                                    var exportChild = new Export()
                                                    {
                                                        ID = ID_.ToString(),
                                                        Type = "variation",
                                                        Name = itemFile.Name.Replace(".jpeg", "").Replace(".png", "") + " - " + itemCheck.Content.ToString(),
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
                                Name = itemFile.Name.Replace(".jpeg", "").Replace(".png", ""),
                                Published = "1",
                                IsFeatured = "0",
                                VisibilityInCatalog = "visible",
                                ShortDescription = "",
                                Description = itemFile.Name.Replace(".jpeg", "").Replace(".png", "")  + Environment.NewLine + txtDes.Text,
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
                                RegularPrice = (folder.Name.Contains("Tumbler") || folder.Name.Contains("Music Box")) ? Tumbler : "",
                                Categories = itemFolder.Name,
                                Tags = "",
                                ShippingClass = "",
                                Images = URL + ", " + URL + ", " + GetLinkSizeChart(),
                                Parent = "",
                                GroupedProducts = "",
                                Upsells = "",
                                CrossSells = "",
                                ExternalURL = "",
                                ButtonText = "",
                                Position = "0",
                                SwatchesAttributes = "",
                                Attribute1Global = "0",
                                Attribute1Name = folder.Name.Contains("Sneaker") ? "Style" : "Size",
                                Attribute1Value = folder.Name.Contains("Sneaker") ? "Men, Women" : size,
                                Attribute1Visible = "1",
                                Attribute2Value = size,
                                Attribute2Global = "0",
                                Attribute2Name = "Size",
                                Attribute2Visible = "1"
                            };
                            if (!folder.Name.Contains("Tumbler") && !folder.Name.Contains("Music Box"))
                            {
                                if (dsChildRen.Count > 0 && export != null)
                                {
                                    ds.Add(export);
                                    ds.AddRange(dsChildRen);
                                    if (folder.Name.Contains("Sneaker"))
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
                    Name = folder.Name,
                    DanhSach = ds,
                    Link = dsLink
                };
            }
            catch
            {
                return new RootExport()
                {
                    Name = folder.Name,
                    DanhSach = ds,
                    Link = dsLink
                };
            }
            finally
            {
                GC.Collect();
            }
        }
        private void trvFiles_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //var item = trvFiles.SelectedItem as Folder;
            //if (item != null)
            //{
            //    if (item.Files.Count > 0)
            //    {
            //        foreach (var size in item.Files)
            //        {
            //            try
            //            {
            //                var itemImage = size as FileInfo;
            //                if (itemImage.FullName.Contains(".jpeg") || itemImage.FullName.Contains(".png"))
            //                {
            //                    System.Windows.Controls.Image image = new System.Windows.Controls.Image()
            //                    {
            //                        Width = 80,
            //                        Height = 80,
            //                        Margin = new Thickness(5),
            //                        Source = new BitmapImage(new Uri(itemImage.FullName, UriKind.RelativeOrAbsolute))
            //                    };
            //                    dsAnh.Children.Add(image);
            //                }
            //            }
            //            catch { }
            //            finally
            //            {
            //                GC.Collect();
            //            }
            //        }
            //    }
            //}
        }
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
            catch(Exception ecx)
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
        private async void btnResize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var flder = new Folder(folderDlg.SelectedPath);
                    if (flder != null)
                    {
                        btnResize.IsEnabled = false;
                        string root = flder.FullPath + "_Resize";
                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        await Task.Run(() =>
                        {
                            foreach (Folder itemFolderProduct in flder.SubFolders)
                            {
                                string rootProduct = root + "\\" + itemFolderProduct.Name;
                                if (!Directory.Exists(rootProduct))
                                {
                                    Directory.CreateDirectory(rootProduct);
                                }
                                foreach (Folder itemFolderCollection in itemFolderProduct.SubFolders)
                                {
                                    string rootCollection = rootProduct + "\\" + itemFolderCollection.Name;
                                    if (!Directory.Exists(rootCollection))
                                    {
                                        Directory.CreateDirectory(rootCollection);
                                    }

                                    foreach (FileInfo file in itemFolderCollection.Files)
                                    {
                                        try
                                        {
                                            //ReduceImage(rootCollection + "\\" + file.Name, file.FullName);
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
                                }
                            }
                        });

                        System.Windows.MessageBox.Show("Resize thành công!!");
                        btnResize.IsEnabled = false;
                    }
                    else
                    {
                        btnResize.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Chưa chọn folder");
                btnResize.IsEnabled = true;
            }
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
                try
                {
                    var newRow = sheet.CreateRow(rowIndex);
                    FillToRow(item, newRow, name);
                    rowIndex++;
                }
                catch(Exception ex)
                {
                }
                finally
                {
                    GC.Collect();
                }
            }
            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + name + " " + RandomString(5) + " " + DateTime.Now.ToLongDateString() + ".xlsx", FileMode.CreateNew);
            wb.Write(fs);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void FillToRow(Export ex, IRow row, string name)
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
                //Weight (lbs)
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



                //Attribute 1 name
                if (name != "Tumbler" || name != "Music Box")
                {
                    row.CreateCell(40).SetCellValue(ex.Attribute1Name);
                }
                //Attribute 1 value(s)
                if (name != "Tumbler" || name != "Music Box")
                    row.CreateCell(41).SetCellValue(ex.Attribute1Value);
                //Attribute 1 visible
                if (name != "Tumbler" || name != "Music Box")
                    row.CreateCell(42).SetCellValue(ex.Attribute1Visible);
                //Attribute 1 global
                if (name != "Tumbler" || name != "Music Box")
                    row.CreateCell(43).SetCellValue(ex.Attribute1Global);
                if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                    row.CreateCell(44).SetCellValue(ex.Attribute2Name);
                if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                    row.CreateCell(45).SetCellValue(ex.Attribute2Value);
                if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                    row.CreateCell(46).SetCellValue(ex.Attribute2Visible);
                if (name != "Music Box" && name != "Tumbler" && name.Contains("Sneaker"))
                    row.CreateCell(47).SetCellValue(ex.Attribute2Global);
            }
            catch
            { }
            finally
            {
                GC.Collect();
            }
            }
        private void btnResetId_Click(object sender, RoutedEventArgs e)
        {
            if (CountHandle.getInstance.Update(1, 1) > 0)
            {
                System.Windows.Forms.MessageBox.Show("Reset Id thành công");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Reset Id thất bại");
            }
        }
        private void btnEditSize_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in danhSachSize.Children)
            {
                var stack = item as StackPanel;
                if (stack != null)
                {
                    var itemCheck = stack.Children[0] as System.Windows.Controls.CheckBox;
                    var itemText = stack.Children[1] as System.Windows.Controls.TextBox;
                    if (itemCheck != null && itemText != null)
                    {
                        itemText.IsEnabled = true;
                    }
                }
            }
            btnEditSize.IsEnabled = false;
            btnSaveSize.IsEnabled = true;
        }
        private void btnSaveSize_Click(object sender, RoutedEventArgs e)
        {
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
                        itemText.IsEnabled = false;
                    }
                }
            }
            if (!String.IsNullOrEmpty(size))
            {
                var item = cbbLoaiSanPham.SelectedItem as Category;
                if (item != null)
                {
                    if (SizeHandle.getInstance.Update(item.Id.ToString(), size) > 0)
                    {
                        System.Windows.MessageBox.Show("Cập nhật giá thành công");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Cập nhật giá thất bại");
                    }
                }
            }
            btnEditSize.IsEnabled = true;
            btnSaveSize.IsEnabled = false;
        }
        private void btnAddNewSize_Click(object sender, RoutedEventArgs e)
        {
            var itemTextSize = new System.Windows.Controls.TextBox()
            {
                Width = 200,
                Height = 30,
                Text = ""
            };
            var itemButtonDel = new System.Windows.Controls.Button()
            {
                Width = 40,
                Height = 30,
                Content = "Xóa",
                Margin = new Thickness(15, 0, 0, 0),
            };
            var stack = new StackPanel()
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0)

            };
            stack.Children.Add(itemTextSize);
            stack.Children.Add(itemButtonDel);
            if (stack != null)
            {
                stackSizeChart.Children.Add(stack);
                btnSaveSizeImg.IsEnabled = true;
            }
        }
        private void btnSaveSizeImg_Click(object sender, RoutedEventArgs e)
        {
            string size = "";
            foreach (var item in stackSizeChart.Children)
            {
                var stack = item as StackPanel;
                if (stack != null)
                {
                    var itemText = stack.Children[0] as System.Windows.Controls.TextBox;
                    if (itemText != null)
                    {
                        if (!String.IsNullOrEmpty(size))
                        {
                            size += ",";
                        }
                        size += itemText.Text;
                    }
                }
            }
            if (!String.IsNullOrEmpty(size))
            {
                var item = cbbLoaiSanPham.SelectedItem as Category;
                if (item != null)
                {
                    if (SizeHandle.getInstance.UpdateSize(item.Id, size) > 0)
                    {
                        System.Windows.MessageBox.Show("Cập size chart thành công");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Cập nhật size chart thất bại");
                    }
                }
            }

        }
        public string GetLinkSizeChart()
        {
            string size = "";
            foreach (var item in stackSizeChart.Children)
            {
                var stack = item as StackPanel;
                if (stack != null)
                {
                    var itemText = stack.Children[0] as System.Windows.Controls.TextBox;
                    if (itemText != null)
                    {
                        if (!String.IsNullOrEmpty(itemText.Text))
                        {
                            if (!String.IsNullOrEmpty(size))
                            {
                                size += ",";
                            }
                            size += itemText.Text;
                        }
                    }
                }
            }
            return size;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
