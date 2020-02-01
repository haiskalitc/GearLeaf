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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrawDataLolonesies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetProduct get = new GetProduct();
            var ds = get.Get("https://www.lolonesies.com/sitemap_products_1.xml?from=1512336965&to=1975795908662");
            if (ds.Count > 0)
            {
                Crawler craw = new Crawler();
                craw.Init();
                Task.Run(() => 
                {
                    craw.Run(ds);
                }); 
            }
        }
    }
}
