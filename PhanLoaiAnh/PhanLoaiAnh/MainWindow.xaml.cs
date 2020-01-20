using PhanLoaiAnh.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PhanLoaiAnh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get all collection from textbox multiline
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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

        /// <summary>
        /// btnRun
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var ds = GetListCollections(txtPath.Text);

            //Handle.getInstance.ThemSanPham(ds);
        }

        public void Run(List<MyFileInfo> dsFiles, string path)
        {
            List<PhanLoaiSanPham> dsRoot = new List<PhanLoaiSanPham>();
            var dsLoaiSanPham = Handle.getInstance.GetListLoaiSanPham();
            var dsSanPham = Handle.getInstance.GetListSanPham();
            #region Loop Danh sách Sản phẩm
            foreach (var itemLoaiSanPham in dsLoaiSanPham)
            {
                var dsFileFilterBySanPham = new List<MyFileInfo>();
                if (itemLoaiSanPham.Name.Equals("Blanket"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains("blanket") && !modelFile.File.Name.ToLower().Trim().Contains("quilt blanket"))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else if (itemLoaiSanPham.Name.Equals("Quilt Blanket"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if ((modelFile.File.Name.ToLower().Trim().Contains("quilt blanket") || modelFile.File.Name.ToLower().Trim().Contains("quilt")))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else if (itemLoaiSanPham.Name.Equals("Rug"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains("rug") || modelFile.File.Name.ToLower().Trim().Contains("overlay"))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else if (itemLoaiSanPham.Name.Equals("Sporty Sneaker"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains("sporty sneaker") || modelFile.File.Name.ToLower().Trim().Contains("sport sneaker"))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else if (itemLoaiSanPham.Name.Equals("Sneaker"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains("sneaker") && ( !modelFile.File.Name.ToLower().Trim().Contains("sport sneaker") && !modelFile.File.Name.ToLower().Trim().Contains("sporty sneaker") && !modelFile.File.Name.ToLower().Trim().Contains("chunky sneaker")))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else if (itemLoaiSanPham.Name.Equals("Bedding Set"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains("bedding set") || modelFile.File.Name.ToLower().Trim().Contains("cover set"))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else if (itemLoaiSanPham.Name.Equals("3D Hoodie"))
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains("3d hoodie") || modelFile.File.Name.ToLower().Trim().Contains("3d"))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                else
                {
                    dsFileFilterBySanPham = dsFiles.Where(modelFile =>
                    {
                        if (modelFile.File.Name.ToLower().Trim().Contains(itemLoaiSanPham.Name.ToLower().Trim()))
                        {
                            return true;
                        }
                        return false;
                    }).ToList();
                }
                if (dsFileFilterBySanPham.Count > 0)
                {
                    //// Xóa danh sách đã có
                    dsFiles.RemoveAll(model => dsFileFilterBySanPham.Any(mode => mode.Id.Equals(model.Id)));
                    var dsTemp = new List<Collection>();
                    #region Loop loại sản phẩm
                    foreach (var itemSanPham in dsSanPham)
                    {
                        var dsFileFilterByLoaiSanPham = new List<MyFileInfo>();
                        switch (itemSanPham.Name.ToLower().Trim())
                        {

                            case "zodiac":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("aries") || name.Equals("taurus") || name.Equals("gemini") ||
                                                name.Equals("cancer") || name.Equals("leo") ||
                                                name.Equals("virgo") || name.Equals("libra") ||
                                                name.Equals("scorpius") || name.Equals("sagittarius") || name.Equals("capricorn") ||
                                                name.Equals("aquarius") || name.Equals("pisces") || name.Contains("zodiac"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }

                            case "month":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("january") || name.Equals("february") || name.Equals("march") ||
                                                name.Equals("april") || name.Equals("may") ||
                                                name.Equals("june") || name.Equals("july") ||
                                                name.Equals("august") || name.Equals("september") || name.Equals("october") ||
                                                name.Equals("november") || name.Equals("december"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "slovakia":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("slovakia") || name.Equals("slovenia"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "baseball":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("baseball") || name.Equals("basebal"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "cheer":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("cheer") || name.Equals("cheerleading"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "dog":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("dog") || name.Equals("chow chow") || name.Equals("chihuahua"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "boat":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("boat") || name.Equals("boating"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "book":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("book") || name.Equals("bookcase") || name.Equals("bookshelf"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "fish":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("fish") || name.Equals("fishing"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "australia":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("australia") || name.Equals("australian"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "race":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("race") || name.Equals("racing"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "zambia":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("zambia") || name.Equals("zimbabwe"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }

                            case "wolf":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Contains("wolf") || name.Equals("wolves"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                                
                            case "alaska":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("alaska") || name.Equals("alaskan"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "amecian":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            
                                            if (name.Equals("amecian") || name.Equals("american") || name.Equals("america"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "yorkie":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("yorkie") || name.Equals("yorkshire"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "basebal":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("basebal") || name.Equals("baseball"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "elephant":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("elephant") || name.Equals("elephan"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "baker":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("baker") || name.Equals("baking"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "africa":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("africa") || name.Equals("african") || name.Equals("afican "))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "basketball":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("basketball") || name.Equals("basket"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "girls":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("girls") || name.Equals("girl"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "chowchow":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("chowchow") || name.Equals("chow"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "surfer":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("surfboards") || name.Equals("surfer") || name.Equals("surfing") || name.Equals("windsurfing"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();
                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "moto":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains(itemSanPham.Name.ToLower().Trim()))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "bike":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains("bik"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "bicycle":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains("bicycl")|| regexItem.ToLower().Trim().Contains("bicyle"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "dragonfly":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains("dragonfly")|| regexItem.ToLower().Trim().Contains("dragonflies"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "farm":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains("farm") || regexItem.ToLower().Trim().Contains("farmer"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "butterfly":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains("butterfly") || regexItem.ToLower().Trim().Contains("butterflies"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "electrician":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var regexItem in regex)
                                        {
                                            if (regexItem.ToLower().Trim().Contains("electrician") || regexItem.ToLower().Trim().Contains("electric"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();

                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }
                            case "family":
                                {
                                    dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                        var regex = model.File.Name.ToLower().Trim().Split(' ');
                                        foreach (var name in regex)
                                        {
                                            if (name.Equals("grandma") || name.Equals("mom") || name.Equals("dad") || name.Equals("baby") ||
                                                name.Equals("grandpa") || name.Equals("papa") ||
                                                name.Equals("mother") || name.Equals("sisters") ||
                                                name.Equals("daughter") || name.Equals("granddaughter") || name.Equals("son") ||
                                                name.Equals("husband") || name.Equals("boyfriend") || name.Equals("girlfriend") || name.Equals("wife") || name.Equals("family") || name.Equals("daddy"))
                                            {
                                                return true;
                                            }
                                        }
                                        return false;
                                    }).ToList();


                                    if (dsFileFilterByLoaiSanPham.Count > 0)
                                    {
                                        dsTemp.Add(new Collection()
                                        {
                                            SanPham = itemSanPham.Name,
                                            DanhSachAnh = dsFileFilterByLoaiSanPham
                                        });
                                        dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                    }
                                    break;
                                }

                            default:
                                {
                                    var regexItemSanPham = itemSanPham.Name.ToLower().Trim().Split(' ');
                                    if (regexItemSanPham.Length > 1)
                                    {
                                        dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => model.File.Name.ToLower().Trim().Contains(itemSanPham.Name.ToLower().Trim())).ToList();
                                        if (dsFileFilterByLoaiSanPham.Count > 0)
                                        {
                                            dsTemp.Add(new Collection()
                                            {
                                                SanPham = itemSanPham.Name,
                                                DanhSachAnh = dsFileFilterByLoaiSanPham
                                            });
                                            dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                        }
                                    }
                                    else if (regexItemSanPham.Length > 0)
                                    {

                                        dsFileFilterByLoaiSanPham = dsFileFilterBySanPham.Where(model => {
                                            var regex = model.File.Name.ToLower().Trim().Split(' ');
                                            foreach (var regexItem in regex)
                                            {
                                                if (regexItem.ToLower().Trim().Equals(itemSanPham.Name.ToLower().Trim()) || regexItem.ToLower().Trim().Equals(itemSanPham.Name.ToLower().Trim() + "s") || regexItem.ToLower().Trim().Equals(itemSanPham.Name.ToLower().Trim() + "es"))
                                                {
                                                    return true;
                                                }
                                            }
                                            return false;
                                        }).ToList();

                                        if (dsFileFilterByLoaiSanPham.Count > 0)
                                        {
                                            dsTemp.Add(new Collection()
                                            {
                                                SanPham = itemSanPham.Name,
                                                DanhSachAnh = dsFileFilterByLoaiSanPham
                                            });
                                            dsFileFilterBySanPham.RemoveAll(model => dsFileFilterByLoaiSanPham.Any(mode => mode.Id.Equals(model.Id)));
                                        }
                                        break;
                                    }

                                    break;
                                }
                        }
                    }
                    dsTemp.Add(new Collection()
                    {
                        SanPham = "Other",
                        DanhSachAnh = dsFileFilterBySanPham
                    });
                    #endregion
                    // Check ds San pham
                    if (dsTemp.Count > 0)
                    {
                        dsRoot.Add(new PhanLoaiSanPham()
                        {
                            ListSanPham = dsTemp,
                            LoaiSanPham = itemLoaiSanPham.Name
                        });
                    }
                }
            }
            #endregion
            // ROOT
            if (dsRoot.Count > 0)
            {
                // Move
                #region Loop tao folder
                if (dsRoot.Count > 0)
                {
                    foreach (var item in dsRoot)
                    {
                        var newFolder = path + "\\" + item.LoaiSanPham;
                        if (!Directory.Exists(newFolder))
                        {
                            Directory.CreateDirectory(newFolder);
                        }
                        foreach (var sanPham in item.ListSanPham)
                        {
                            var newFolderSanPham = newFolder + "\\" + sanPham.SanPham;
                            if (!Directory.Exists(newFolderSanPham))
                            {
                                Directory.CreateDirectory(newFolderSanPham);
                            }
                            foreach (var itemDetail in sanPham.DanhSachAnh)
                            {
                                try
                                {
                                    //if (chkResize.IsChecked == true)
                                    //{
                                    //    ImageReduce.SaveJpeg(itemDetail.FullName, newFolderSanPham + "\\" + itemDetail.Name, 50);
                                    //}
                                    //else
                                    //{
                                    //    System.IO.Directory.Move(itemDetail.FullName, newFolderSanPham + "\\" + itemDetail.Name);
                                    //}
                                    itemDetail.File.CopyTo(newFolderSanPham + "\\" + itemDetail.File.Name);
                                    itemDetail.File.Delete();
                                    //System.IO.Directory.Move(itemDetail.File.FullName, newFolderSanPham + "\\" + itemDetail.File.Name);
                                    //System.IO.Directory.Delete(itemDetail.File.FullName);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    System.Windows.MessageBox.Show("Xong", "Thông báo", MessageBoxButton.OK);
                }
                else
                {
                    System.Windows.MessageBox.Show("Không phân loại được sản phẩm nào", "Thông báo", MessageBoxButton.OK);
                }
                #endregion
            }
        }

        /// <summary>
        /// btnPath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = folderDlg.SelectedPath;
                var str = txtPath.Text.Split('\\');
                var folderList = new Folders(folderDlg.SelectedPath);
                if (folderList.Files.Count > 0)
                {
                    await Task.Run(() => 
                    {
                        Run(folderList.Files, folderDlg.SelectedPath);
                    });
                }
            }

        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
