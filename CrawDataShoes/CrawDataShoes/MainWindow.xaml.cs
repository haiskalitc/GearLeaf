using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

namespace CrawDataShoes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Crawler cr = new Crawler();
        public MainWindow()
        {
            InitializeComponent();
            cr.Init();
        }

        public List<string> GetListCollections(string source)
        {
            try
            {
                return new List<string>(source.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch
            {
                return null;
            }
        }

        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLink.Text.Trim()))
                {
                    var ds = GetListCollections(txtLink.Text.Trim());
                    if (ds.Count > 0)
                    {
                        try
                        {
                            btnRun.IsEnabled = false;
                            await Task.Run(() =>
                            {
                                cr.Run(ds);
                            });
                            this.Dispatcher.Invoke(() =>
                            {
                                btnRun.IsEnabled = true;
                                MessageBox.Show("Xong!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Dispatcher.Invoke(() =>
                            {
                                btnRun.IsEnabled = true;
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thí chủ chưa nhập Link", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Thí chủ chưa nhập Link", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.Invoke(() =>
                {
                    btnRun.IsEnabled = true;
                });
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

        private void Window_Closed(object sender, EventArgs e)
        {
            ClearDriver();
            ClearGoogleChrome();
        }
    }
}
