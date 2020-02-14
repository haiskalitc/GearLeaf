using CrawTeeherivar;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Export> dsExport = null;
        public string pathID = Environment.CurrentDirectory + "\\id.txt";
        public string path = Environment.CurrentDirectory + "\\link.txt";

        public MainWindow()
        {
            InitializeComponent();

        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
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
                        dsExport = craw.GetProduct(dsLink);                       
                    }
                }
                else
                {
                    Console.WriteLine("Thất bại - KHÔNG TÌM THẤY LINK XML");
                }
            });
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (dsExport.Count > 0)
            {
                Window1 win = new Window1(dsExport);
                win.Show();
                this.Hide();
                win.Back += (sen, arg) =>
                {
                    (sen as Window1).Close();
                    File.WriteAllText(pathID, FilesHandle.ID_GLOBAL.ToString());
                };
            }
            else
            {//
                Console.WriteLine("Thất bại - KHÔNG TÌM THẤY LINK XML");
            }
        }
    }
}
