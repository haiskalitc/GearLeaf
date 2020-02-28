using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZetaLongPaths;

namespace LocThuMuc
{
    public class Handle
    {
        public void PhanLoai()
        {
            string[] keywords = File.ReadAllLines(Environment.CurrentDirectory + "\\keywords.txt");
            string input = File.ReadAllText(Environment.CurrentDirectory + "\\input.txt");
            string output = File.ReadAllText(Environment.CurrentDirectory + "\\output.txt");
            Folder folder = new Folder(input);

            foreach (var item in keywords)
            {
                foreach (Folder itemFolder in folder.SubFolders)
                {
                    if (itemFolder.Name.Contains(item.Trim()))
                    {
                        string pathFolder = output + "\\" + item.Trim();
                        if (!Directory.Exists(pathFolder))
                        {
                            Directory.CreateDirectory(pathFolder);
                        }
                        try
                        {
                            string pathFile = pathFolder + "\\" + itemFolder.Name;
                            if (!Directory.Exists(pathFile))
                            {
                                Directory.CreateDirectory(pathFile);
                            }
                            foreach (FileInfo file in itemFolder.Files)
                            {
                                file.CopyTo(pathFile + "\\" + file.Name);
                                file.Delete();
                            }
                            System.IO.Directory.Delete(itemFolder.FullPath);
                            Console.WriteLine("Create new " + itemFolder.Name + " folder in " + itemFolder.FullPath);
                        }
                        catch
                        { }
                    }
                }
            }
        }

        public string StringToString(string str)
        {
            string res = "";
            var arr = str.Split(' ');
            foreach (var item in arr)
            {
                if (!String.IsNullOrEmpty(res))
                {
                    res += " ";
                }

                if (!String.IsNullOrEmpty(item.Trim()))
                {
                    if (item.Trim().ToLower().Contains("Tshirt"))
                    {
                        if (!res.ToLower().Trim().Contains("hoodie"))
                        {
                            res += "Hoodie";
                        }
                    }
                    else
                    {
                        res += item.Trim();
                    }
                }
            }
            if (!res.ToLower().Trim().Contains("hoodie"))
            {
                res += " Hoodie";
            }
            return ToTitleCase(res);
        }

        public void PhanLoaiBonHuHuVipProDepTraiKuTePhoMaiQue()
        {
            string input = File.ReadAllText(Environment.CurrentDirectory + "\\input.txt");
            ZlpDirectoryInfo folder = new ZlpDirectoryInfo(input);
            string s = "";
            foreach (ZlpDirectoryInfo itemFolder in folder.GetDirectories()) 
            {
                foreach (ZlpDirectoryInfo item in itemFolder.GetDirectories())
                {
                    if (item.Name.ToLower().Contains("shir"))
                    {
                        s += (item.Name) + Environment.NewLine;
                    }
                    // string name = item.Name.Replace("T Shirt", "");
                    //try
                    //{
                    //    if (item.Name.ToLower().Contains("shirt"))
                    //    {
                    //        item.Delete(true);
                    //    }
                    //    item.MoveTo(itemFolder.GetFullPath() + "/" + StringToString(name));
                    //}
                    //catch
                    //{
                    //    item.MoveTo(itemFolder.GetFullPath() + "/" + StringToString(name) + " " + new Random().Next(0, 100));

                    //}
                }
            }
        }

        public void PhanLoaiBa()
        {
            string input = File.ReadAllText(Environment.CurrentDirectory + "\\input.txt");
            ZlpDirectoryInfo folder = new ZlpDirectoryInfo(input);
            foreach (ZlpDirectoryInfo itemFolder in folder.GetDirectories())
            {
                foreach (ZlpDirectoryInfo item in itemFolder.GetDirectories())
                {
                    foreach (ZlpDirectoryInfo itemProduct in itemFolder.GetDirectories())
                    {
                        foreach (ZlpDirectoryInfo file in itemProduct.GetDirectories())
                        {
                            foreach (ZlpFileInfo itemFile in file.GetFiles())
                            {
                                try
                                {
                                    string name = itemFile
                                        .Name.Replace(" ", "-").Replace(" ", "-")
                                        .Replace(" ", "-").Replace(" ", "-")
                                        .Replace("--", "-").Replace("--", "-")
                                        .Replace("--", "-").Replace("--", "-").Replace("--", "-");
                                    itemFile.CopyTo(file.GetFullPath() + "\\" + Regex.Replace(name, "[^a-zA-Z0-9._-]", string.Empty), true);
                                    itemFile.Delete();
                                    Console.ForegroundColor = (System.ConsoleColor)new Random().Next(0, 14);
                                    Console.WriteLine(file.FullName);
                                }
                                catch
                                {

                                }
                                finally
                                {
                                    GC.Collect();
                                }
                            }
                        }
                    }
                }
            }
        }

        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        public void PhanLoaiHai()
        {
            //string[] keywords = File.ReadAllLines(Environment.CurrentDirectory + "\\keywords.txt");
            string input = File.ReadAllText(Environment.CurrentDirectory + "\\input.txt");
            // string output = File.ReadAllText(Environment.CurrentDirectory + "\\output.txt");
            var folderPath = new ZlpDirectoryInfo(input);

            if (folderPath.GetFiles().Length > 0)
            {
                foreach (ZlpFileInfo itemFile in folderPath.GetFiles())
                {
                    var dsArr = itemFile.Name.Split('_');
                    if (dsArr.Count() > 1)
                    {
                        var path = itemFile.DirectoryName + "\\" + ToTitleCase(dsArr[0]).Trim();
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        try
                        {
                            itemFile.MoveTo(path + "\\" + itemFile.Name, true);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Không có file ảnh");
            }


            //foreach (var item in keywords)
            //{
            //    foreach (Folder itemFolder in folder.SubFolders)
            //    {
            //        if (itemFolder.Name.Contains(item.Trim()))
            //        {
            //            string pathFolder = output + "\\" + item.Trim();
            //            if (!Directory.Exists(pathFolder))
            //            {
            //                Directory.CreateDirectory(pathFolder);
            //            }
            //            try
            //            {
            //                string pathFile = pathFolder + "\\" + itemFolder.Name;
            //                if (!Directory.Exists(pathFile))
            //                {
            //                    Directory.CreateDirectory(pathFile);
            //                }
            //                foreach (FileInfo file in itemFolder.Files)
            //                {
            //                    file.CopyTo(pathFile + "\\" + file.Name);
            //                    file.Delete();
            //                }
            //                System.IO.Directory.Delete(itemFolder.FullPath);
            //                Console.WriteLine("Create new " + itemFolder.Name + " folder in " + itemFolder.FullPath);
            //            }
            //            catch
            //            { }
            //        }
            //    }
            //}
        }
    }
}
