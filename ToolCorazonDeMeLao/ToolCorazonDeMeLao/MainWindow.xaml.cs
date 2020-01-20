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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToolCorazonDeMeLao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Folder dsFolder;
        public MainWindow()
        {
            InitializeComponent();
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
                dsFolder = new Folder(folderDlg.SelectedPath);
                if (dsFolder != null)
                {
                    foreach (Folder itemFodlerRoot in dsFolder.SubFolders)
                    {
                        foreach (Folder itemFodler in itemFodlerRoot.SubFolders)
                        {
                            foreach (FileInfo file in itemFodler.Files)
                            {
                                var newNames = file.Name.Split(' ');
                                string temp = "";
                                bool isLoop = Check(newNames[newNames.Length - 1].ToLower().Trim().ToString());
                                int loop = isLoop ? newNames.Length - 1 : newNames.Length;
                                for (int i = 0; i < loop; i++)
                                {
                                    if (!String.IsNullOrEmpty(temp))
                                    {
                                        temp += " ";
                                    }
                                    temp += newNames[i];
                                }

                                try
                                {
                                    temp = temp.Replace("- Novelty Graphic", "").Trim();
                                    temp = temp.Replace("- Xmas Gift", "").Trim();
                                    temp = temp.Replace("Graphic Tee", "").Trim();
                                    if (isLoop)
                                    {
                                        temp += ".png";
                                        file.CopyTo(file.DirectoryName + "\\" + temp);
                                        file.Delete();
                                    }
                                }
                                catch
                                { }
                            }
                        }
                    }
                }
            }
        }

        public bool Check(string source)
        {
            if (source.Contains("shirt") || source.Contains("shirts") || 
                source.Contains("tshirt") || source.Contains("t-shirt") || 
                source.Contains("t shirt"))
            {
                return true;
            }
            return false;
        }
    }
}
