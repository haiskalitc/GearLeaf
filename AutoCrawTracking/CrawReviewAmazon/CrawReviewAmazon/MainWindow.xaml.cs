using LumenWorks.Framework.IO.Csv;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrawReviewAmazon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Crawler craw = new Crawler();
        public MainWindow()
        {
            InitializeComponent();
            var ds = craw.Init("https://seller.merchize.com/a/orders?page=1");
            Dispatcher.Invoke(() =>
            {
                rtbLog.AppendText("Nhớ đăng nhập tài khoản merchize trước nha." + Environment.NewLine);
                rtbLog.AppendText("Muốn mở nhạc hay làm gì đó thì sài trình duyệt khác không được sài Google Chrome," + Environment.NewLine);

            });
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // IWorkbook workBook;
                if (!String.IsNullOrEmpty(txtPath.Text.Trim()))
                {
                    List<Source> ds = new List<Source>();
                    using (CsvReader csv = new CsvReader(new StreamReader(txtPath.Text.Trim()), true))
                    {
                        int fieldCount = csv.FieldCount;

                        string[] headers = csv.GetFieldHeaders();
                        while (csv.ReadNextRecord())
                        {
                            var tran = csv[25].ToString();
                            var ode = csv[12].ToString();
                            if (!String.IsNullOrEmpty(tran) && !String.IsNullOrEmpty(ode))
                            {
                                Source source = new Source()
                                {
                                    OrderId = tran,
                                    TransactionId = ode
                                };
                                if (source != null)
                                {
                                    if (!String.IsNullOrEmpty(source.OrderId) && !String.IsNullOrEmpty(source.TransactionId))
                                    {
                                        ds.Add(source);
                                    }
                                }
                            }
                        }
                    }
                    if (craw != null)
                    {
                        if (ds.Count > 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                rtbLog.AppendText("Đang bắt đầu lấy dữ liệu, vui lòng ko tắt Google Chrome" + Environment.NewLine);
                                rtbLog.ScrollToEnd();
                                btnGet.IsEnabled = false;
                                btnPath.IsEnabled = false;
                            });

                            List<Review> dsRe = new List<Review>();
                            await Task.Run(() =>
                            {
                                dsRe.AddRange(craw.Get(ds));
                            });
                            if (dsRe.Count > 0)
                            {

                                XSSFWorkbook wb = new XSSFWorkbook();
                                ISheet sheet = wb.CreateSheet();

                                var row1 = sheet.CreateRow(0);
                                row1.CreateCell(0).SetCellValue("OrderID");
                                row1.CreateCell(1).SetCellValue("Customer");
                                row1.CreateCell(2).SetCellValue("TransactionID");
                                row1.CreateCell(3).SetCellValue("TrackingCode");
                                row1.CreateCell(4).SetCellValue("CarrierName");

                                int rowIndex = 1;
                                foreach (var item in dsRe)
                                {
                                    var newRow = sheet.CreateRow(rowIndex);
                                    newRow.CreateCell(0).SetCellValue(item.OrderID);
                                    newRow.CreateCell(1).SetCellValue(item.Customer);
                                    newRow.CreateCell(2).SetCellValue(item.TransactionID);
                                    newRow.CreateCell(3).SetCellValue("GEARLEAF" + item.TrackingCode);
                                    newRow.CreateCell(4).SetCellValue(item.CarrierName);
                                    rowIndex++;
                                }
                                string fileSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + RandomString(10) + ".xlsx";
                                FileStream fs = new FileStream(fileSavePath, FileMode.CreateNew);
                                wb.Write(fs);
                                MessageBox.Show("Xuất thành công file tại " + Environment.NewLine + fileSavePath, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                Dispatcher.Invoke(() =>
                                {
                                    rtbLog.AppendText("Xuất thành công file tại " + fileSavePath + Environment.NewLine);
                                    rtbLog.ScrollToEnd();
                                    btnGet.IsEnabled = true;
                                    btnPath.IsEnabled = true;
                                });

                            }
                            else
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    rtbLog.AppendText("Không lấy được order hoặc chưa đăng nhập" + Environment.NewLine);
                                    rtbLog.ScrollToEnd();
                                    btnGet.IsEnabled = true;
                                    btnPath.IsEnabled = true;
                                });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chưa đăng nhập tài khoản merchize", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            Dispatcher.Invoke(() =>
                            {
                                rtbLog.AppendText("Chưa đăng nhập tài khoản merchize" + Environment.NewLine);
                                rtbLog.ScrollToEnd();
                                btnGet.IsEnabled = true;
                                btnPath.IsEnabled = true;
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Khởi tạo driver thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        Dispatcher.Invoke(() =>
                        {
                            rtbLog.AppendText("Khởi tạo driver thất bại" + Environment.NewLine);
                            rtbLog.ScrollToEnd();
                            btnGet.IsEnabled = true;
                            btnPath.IsEnabled = true;
                        });
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu tracking", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    Dispatcher.Invoke(() =>
                    {
                        rtbLog.AppendText("Không có dữ liệu tracking" + Environment.NewLine);
                        rtbLog.ScrollToEnd();
                        btnGet.IsEnabled = true;
                        btnPath.IsEnabled = true;
                    });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Khởi tạo driver thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                Dispatcher.Invoke(() => 
                {
                    rtbLog.AppendText(ex.Message + Environment.NewLine);
                    rtbLog.ScrollToEnd();
                    btnGet.IsEnabled = true;
                    btnPath.IsEnabled = true;
                });
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel Files |*.xls;*.xlsx;*.csv";
            if (open.ShowDialog() == true)
            {
                txtPath.Text = open.FileName;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClearDriver();
            ClearGoogleChrome();
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
