using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeherivar
{
   public class GifNestHandle
    {
        public List<string> DanhSachSize = null;
        public List<string> DanhSachGia = null;
        public string GetValue(List<string> ds)
        {
            string res = "";
            foreach (var item in ds)
            {
                if (!String.IsNullOrEmpty(res))
                {
                    res += ", ";
                }
                res += item;
            }
            return res;
        }
        #region

        public List<Export> GetProductFromFile(string path, string name)
        {
            try
            {
                int id = int.Parse(File.ReadAllText(Environment.CurrentDirectory + "\\ID\\id.txt"));
                string des = File.ReadAllText(Environment.CurrentDirectory + "\\DescriptionMerking\\" + name + ".txt");
                var dsResult = new List<Export>();
                string sizeChart = "";
                switch (name)
                {
                    #region size, gia, size chart
                    case "Hoodie":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "49.99", "49.99", "49.99", "49.99", "51.99", "51.99", "51.99", "51.99" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Hoodie+3D+size+chart.jpg";
                            break;
                        }
                    case "T-Shirt":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "29.95", "29.95", "29.95", "29.95", "31.95", "31.95", "31.95", "31.95" };
                            sizeChart = "https://i.imgur.com/kz2mItx.jpg";
                            break;
                        }
                    case "Sweatshirt":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "45.95", "45.95", "45.95", "45.95", "47.95", "47.95", "47.95", "47.95" };
                            sizeChart = "https://i.imgur.com/rBCxney.jpg";
                            break;
                        }
                    case "Sweatpant":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL", "6XL" };
                            DanhSachGia = new List<string>() { "53.95", "53.95", "53.95", "53.95", "55.95", "55.95", "55.95", "55.95", "55.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/sweatpant-size-chart.PNG";
                            break;
                        }
                    //case "Jacket":
                    //    {
                    //        DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL" };
                    //        DanhSachGia = new List<string>() { "59.95", "59.95", "59.95", "59.95", "61.95", "61.95" };
                    //        sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Varsity-Jacket-Size-1-600x355.jpg";

                    //        break;
                    //    }
                    case "Jacket":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "52.95", "52.95", "52.95", "52.95", "54.95", "54.95", "54.95", "54.95" };
                            sizeChart = "https://i.imgur.com/ajIfUuy.jpg%22%20alt=%22%22%20width=%22906%22%20height";
                            break;
                        }
                    case "Jersey":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL" };
                            DanhSachGia = new List<string>() { "49.99", "49.99", "49.99", "49.99", "51.99" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Baseball-Jersey-Size-600x355.jpg";
                            break;
                        }
                    case "Bedding Set":
                        {
                            DanhSachSize = new List<string>() { "Twin", "Full", "Queen", "King" };
                            DanhSachGia = new List<string>() { "65.95", "69.95", "69.95", "79.95" };
                            sizeChart = "https://i.imgur.com/csDx4yd.jpg";
                            break;
                        }
                    case "Phone Case":
                        {
                            DanhSachSize = new List<string>() { "Iphone 7/8", "Iphone 7/8 Plus", "Iphone X", "Iphone XS", "Iphone XS MAX", "Iphone XR", "Samsung Galaxy S9", "Samsung Galaxy S9 Plus", "Samsung Galaxy Note 9", "Samsung Galaxy S10", "Samsung Galaxy S10 Plus", "Iphone 6/ 6s", "Samsung Galaxy S10E", "SamSung Galaxy S8", "SamSung Galaxy S8 Plus", "SamSung Galaxy Note 8", "Huawei P20 Pro", "Huawei P30", "Huawei P30 Pro", "Huawei Mate 20 Pro", "OnePlus 6", "Iphone 6/6 Plus" };
                            sizeChart = "";
                            break;
                        }
                    //case "Sneaker":
                    //    {
                    //        DanhSachSize = new List<string>() { "US5(EU37.5)", "US5.5(EU38)", "US6(EU39)", "US7(EU40)", "US8(EU41)", "US8.5(EU42)", "US9(EU43)", "US10(EU44)", "US11(EU45)", "US12(EU46)", "US13(EU47)", "0", "US5(EU35)", "US6(EU36)", "US7(EU37)", "US7.5(EU38)", "US8(EU39)", "US9(EU40)", "US10(EU41)", "US11(EU42)", "US12(EU43)" };
                    //        sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/men+size+chart+(1).jpg" + ", " + "https://sizeallproduct.s3.amazonaws.com/Size+Chart/women+size+chart+(1).jpg"; ;
                    //        break;
                    //    }
                    case "Sneaker":
                        {
                            DanhSachSize = new List<string>() { "US5(EU37)", "US6(EU38)", "US6.5(EU39)", "US7(EU40)", "US8(EU41)","US9(EU42)", "US10(EU43)", "US11(EU44)", "US12(EU45)",
                                "0", "US5 (EU35)", "US6 (EU36)", "US7 (EU37)", "US7.5 (EU38)", "US8 (EU39)", "US8.5 (EU40)", "US9 (EU41)", "US10 (EU42)", "US10.5 (EU43)", "US11 (EU44)", "US12 (EU45)"};
                            sizeChart = "https://i.imgur.com/b74xM69.jpg" + ", " + "http://i.imgur.com/6pDFRiv.jpg"; ;
                            //59.95
                            break;
                        }
                    case "Blanket":
                        {
                            DanhSachSize = new List<string>() { "YOUTH", "LARGE", "XLARGE"};
                            DanhSachGia = new List<string>() { "45.95", "49.95", "59.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Blanket+size+chart.jpg";
                            break;
                        }
                    case "Boots":
                        {
                            DanhSachSize = new List<string>() { "US3(EU35)", "US3.5(EU35.5)", "US4(EU36)", "US4.5(EU36.5)", "US5(EU37)", "US5.5(EU38)", "US6(EU39)"
                                , "US6.5(EU39.5)", "US7(EU40)", "US7.5(EU40.5)",
                                "US8(EU41)", "US8.5(EU42)", "US9(EU42.5)", "US9.5(EU43)",
                                "US10(EU44)", "US10.5(EU44)", "US11(EU45)", "US11.5(EU45.5)", "US12(EU46)", "US13(EU47)", "0",
                                "US4.5 (EU35)", "US5 (EU35.5)", "US5.5 (EU36)",
                                "US6 (EU36.5)", "US6.5 (EU37)", "US7 (EU38)", "US7.5 (EU39)", "US8 (EU39.5)",
                                "US8.5 (EU40)", "US9 (EU40.5)", "US9.5 (EU41)", "US10 (EU42)",
                                "US10.5 (EU42.5)", "US11 (EU43)", "US11.5 (EU44)", "US12 (EU44.5)"
                            };
                            //Boots 74.95
                            sizeChart = "https://i.imgur.com/0jA9OzF.jpg";
                            break;
                        }
                    case "Car Seat":
                        {
                            sizeChart = "https://i.postimg.cc/Znbf4bf1/car-seat-covers.jpg";
                            break;
                        }
                    case "Umbrella":
                        {
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2019/03/Size-chart-for-UMBRELLAS.jpg";
                            break;
                        }
                    case "Shower Curtain":
                        {
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2018/06/sizing-chart-1.png";
                            break;
                        }
                    case "Chunky Sneaker":
                        {
                            DanhSachSize = new List<string>() { "US6(EU39)", "US7(EU40)", "US7.5(EU41)", "US8(EU41.5)", "US8.5(EU42)", "US9(EU42.5)", "US9.5(EU43)", "US10(EU44)", "0",
                            "US5.5 (EU36)", "US6 (EU36.5)", "US6.5 (EU37)", "US7 (EU38)", "US8 (EU39)",
                                "US8.5 (EU40)", "US9 (EU41)", "US9.5 (EU41.5)", "US10 (EU42)", "US10.5 (EU42.5)", "US11 (EU43)", "US11.5 (EU44)"};
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2019/07/Sneakers-Chunky-2019-July-Sizing-Chart.jpg";
                            break;
                        }
                    case "High Top":
                        {
                            DanhSachSize = new List<string>() { "US5(EU35)", "US6(EU36)", "US7(EU37)", "US7.5(EU38)", "US8(EU39)", "US8.5(EU40)",
                                "US9(EU41)", "US10(EU42)", "US10.5(EU43)", "US11(EU44)", "US12(EU45)" };
                            DanhSachGia = new List<string>() { "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Hightop+canvas+size+chart.jpg";
                            break;
                        }
                    case "Leather Boots":
                        {
                            DanhSachSize = new List<string>() { "US5 (EU38)", "US6 (EU39)", "US7 (EU40)", "US7.5 (EU41)", "US8.5 (EU42)", "US9.5 (EU43)", "US10 (EU44)", "US11 (EU45)", "US12 (EU46)", "0",
                           "US5 (EU35)", "US5.5 (EU36)", "US6 (EU37)", "US7 (EU38)", "US8 (EU39)", "US9 (EU40)", "US10 (EU41)", "US11 (EU42)", "US11.5 (EU43)", "US12 (EU44)"};
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2017/08/updated-boots-sizing-chart.jpg";
                            break;
                        }
                    case "Low top":
                        {
                            DanhSachSize = new List<string>() { "US5(EU35)", "US6(EU36)", "US7(EU37)", "US7.5(EU38)", "US8(EU39)", "US8.5(EU40)",
                                "US9(EU41)", "US10(EU42)", "US10.5(EU43)", "US11(EU44)", "US12(EU45)" };
                            DanhSachGia = new List<string>() { "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Lowtop+canvas+size+chart.jpg";
                            break;
                        }
                    case "Quilt":
                        {
                            DanhSachSize = new List<string>() { "SINGLE", "TWIN", "QUEEN", "KING", "SUPPER KING" };
                            DanhSachGia = new List<string>() { "56.95", "59.95", "76.95", "82.95", "88.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Quilts+size+chart.jpg";
                            break;
                        }
                    case "Rug":
                        {
                            DanhSachSize = new List<string>() { "SMALL", "MEDIUM", "LARGE"};
                            DanhSachGia = new List<string>() { "54.95", "69.95", "89.95"};
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Size+Chart.jpg";
                            break;
                        }
                        #endregion
                }
                Folder folder = new Folder(path);
                if (folder.SubFolders.Count > 0)
                {
                    for (int j = 0; j < folder.SubFolders.Count ; j ++)
                    {
                        Folder itemFolder = folder.SubFolders[j] as Folder;
                        if (itemFolder.SubFolders.Count > 0)
                        {
                            foreach (Folder itemProductFolder in itemFolder.SubFolders)
                            {
                                if (itemProductFolder.Files.Count > 0)
                                {
                                    string pathImage = "";
                                    foreach (FileInfo file in itemProductFolder.Files)
                                    {
                                        if (!String.IsNullOrEmpty(pathImage))
                                        {
                                            pathImage += ", ";
                                        }
                                        pathImage += "http://35.197.63.152/uploadcdn/datamerkingnon/" +  folder.Name + "/" +
                                            itemFolder.Name + "/" + itemProductFolder.Name + "/" + file.Name;
                                    }
                                    if (!String.IsNullOrEmpty(pathImage))
                                    {
                                        if (!String.IsNullOrEmpty(sizeChart))
                                        {
                                            pathImage += ", " + sizeChart;
                                        }
                                        string title = ToTitleCase(itemProductFolder.Name.Remove(itemProductFolder.Name.Length - 4).Trim());
                                        if (!title.ToLower().Replace(" ","").Contains(name.ToLower().Replace(" ", "")))
                                        {
                                            title += " " + ToTitleCase(name);
                                        }
                                        //88487
                                        var parent = new Export()
                                        {
                                            ID = id + "",
                                            Type = (name.Equals("Shade") || name.Equals("Auto Sun") || name.Equals("Car Seat") || name.Equals("Shower Curtain") || name.Equals("Umbrella")) ? "simple" : "variable",
                                            SKU = "",
                                            Name = title,
                                            Published = "1",
                                            IsFeatured = "0",
                                            VisibilityInCatalog = "visible",
                                            ShortDescription = "",
                                            Description = "<p>" + title + "</p>" + des,
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
                                            RegularPrice = name.Equals("Auto Sun") ? "39.95" : name.Equals("Car Seat") ? "59.95" : name.Equals("Shower Curtain") ? "49.95" : name.Equals("Umbrella") ? "38.95" : "",
                                            Categories = ToTitleCase(itemFolder.Name),
                                            Tags = "",
                                            ShippingClass = "",
                                            Images = pathImage,
                                            Parent = "",
                                            GroupedProducts = "",
                                            Upsells = "",
                                            CrossSells = "",
                                            ExternalURL = "",
                                            ButtonText = "",
                                            Position = "",
                                            SwatchesAttributes = "",
                                        };
                                        if (name.Equals("Sneaker") || name.Equals("Boots") || name.Equals("Chunky Sneaker") || name.Equals("Leather Boots"))
                                        {
                                            parent.Attribute1Global = "1";
                                            parent.Attribute1Name = "Style";
                                            parent.Attribute1Value = "Men, Women";
                                            parent.Attribute1Visible = "1";

                                            parent.Attribute2Global = "1";
                                            parent.Attribute2Name = "Size";
                                            parent.Attribute2Value = GetValue(DanhSachSize);
                                            parent.Attribute2Visible = "1";
                                        }
                                        else if (!name.Equals("Auto Sun") && !name.Equals("Car Seat") && !name.Equals("Shower Curtain") && !name.Equals("Umbrella") && !name.Equals("Shade"))
                                        {
                                            parent.Attribute1Global = "1";
                                            parent.Attribute1Name = "Size";
                                            parent.Attribute1Value = GetValue(DanhSachSize);
                                            parent.Attribute1Visible = "1";
                                        }
                                        if (parent != null)
                                        {
                                            dsResult.Add(parent);
                                            id++;
                                            if (!name.Equals("Auto Sun") && !name.Equals("Car Seat") && !name.Equals("Shower Curtain") && !name.Equals("Umbrella") && !name.Equals("Shade"))
                                            {
                                                string Gender = "Men";
                                                for (int i = 0; i < DanhSachSize.Count; i++)
                                                {
                                                    if (DanhSachSize[i].Equals("0"))
                                                    {
                                                        Gender = "Women";
                                                    }
                                                    else
                                                    {
                                                        var child = new Export()
                                                        {
                                                            ID = id + "",
                                                            Type = "variation",
                                                            SKU = "",
                                                            Name = title,
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
                                                            RegularPrice = name.Equals("Phone Case") ? "29.95" : 
                                                            name.Equals("Sneaker") ? "59.95" : 
                                                            name.Equals("Phone Case") ? "29.95"  :
                                                            name.Equals("Leather Boots") ? "65.95" :
                                                            name.Equals("Chunky Sneaker") ? "74.95" :
                                                            name.Equals("Boots") ? "74.95"
                                                            : DanhSachGia[i],
                                                            Categories = "",
                                                            Tags = parent.Tags,
                                                            ShippingClass = "",
                                                            Images = parent.Images,
                                                            Parent = "id:" + parent.ID,
                                                            GroupedProducts = "",
                                                            Upsells = "",
                                                            CrossSells = "",
                                                            ExternalURL = "",
                                                            ButtonText = "",
                                                            Position = "",
                                                            SwatchesAttributes = "",
                                                        };
                                                        if (name.Equals("Sneaker") || name.Equals("Boots") || name.Equals("Chunky Sneaker") || name.Equals("Leather Boots"))
                                                        {
                                                            child.Attribute1Global = "1";
                                                            child.Attribute1Name = "Style";
                                                            child.Attribute1Value = Gender;
                                                            child.Attribute1Visible = "1";

                                                            child.Attribute2Global = "1";
                                                            child.Attribute2Name = "Size";
                                                            child.Attribute2Value = DanhSachSize[i];
                                                            child.Attribute2Visible = "1";
                                                        }
                                                        else
                                                        {
                                                            child.Attribute1Global = "1";
                                                            child.Attribute1Name = "Size";
                                                            child.Attribute1Value = DanhSachSize[i];
                                                            child.Attribute1Visible = "";
                                                        }
                                                        if (child != null)
                                                        {
                                                            id++;
                                                            dsResult.Add(child);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Product is not found!");
                        }
                    }
                }
                File.WriteAllText(Environment.CurrentDirectory + "\\ID\\id.txt", id.ToString());
                return dsResult;
            }
            catch
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }
#endregion
        public List<Export> GetProductFromFileAquacozy(string path, string name)
        {
            try
            {
                int id = int.Parse(File.ReadAllText(Environment.CurrentDirectory + "\\ID\\id.txt"));
                string des = File.ReadAllText(Environment.CurrentDirectory + "\\Description\\" + name + ".txt");
                var dsResult = new List<Export>();
                string sizeChart = "";
                switch (name)
                {
                    #region size, gia, size chart
                    case "3D Hoodie":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "49.99", "49.99", "49.99", "49.99", "51.99", "51.99", "51.99", "51.99" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Hoodie+3D+size+chart.jpg";
                            break;
                        }
                    case "Hoodie":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "49.99", "49.99", "49.99", "49.99", "51.99", "51.99", "51.99", "51.99" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Hoodie+3D+size+chart.jpg";
                            break;
                        }
                    case "T-Shirt":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "29.95", "29.95", "29.95", "29.95", "31.95", "31.95", "31.95", "31.95" };
                            sizeChart = "https://i.imgur.com/kz2mItx.jpg";
                            break;
                        }
                    case "Sweatshirt":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "45.95", "45.95", "45.95", "45.95", "47.95", "47.95", "47.95", "47.95" };
                            sizeChart = "https://i.imgur.com/rBCxney.jpg";
                            break;
                        }
                    case "Sweatpant":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL", "6XL" };
                            DanhSachGia = new List<string>() { "53.95", "53.95", "53.95", "53.95", "55.95", "55.95", "55.95", "55.95", "55.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/sweatpant-size-chart.PNG";
                            break;
                        }
                    case "Jacket":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL", "3XL", "4XL", "5XL" };
                            DanhSachGia = new List<string>() { "52.95", "52.95", "52.95", "52.95", "54.95", "54.95", "54.95", "54.95" };
                            sizeChart = "https://i.imgur.com/ajIfUuy.jpg%22%20alt=%22%22%20width=%22906%22%20height";
                            break;
                        }
                    case "Jersey":
                        {
                            DanhSachSize = new List<string>() { "S", "M", "L", "XL", "2XL" };
                            DanhSachGia = new List<string>() { "49.99", "49.99", "49.99", "49.99", "51.99" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Baseball-Jersey-Size-600x355.jpg";
                            break;
                        }
                    case "Bedding Set":
                        {
                            DanhSachSize = new List<string>() { "Twin", "Full", "Queen", "King" };
                            DanhSachGia = new List<string>() { "65.95", "69.95", "69.95", "79.95" };
                            sizeChart = "https://i.imgur.com/csDx4yd.jpg";
                            break;
                        }
                    case "Phone Case":
                        {
                            DanhSachSize = new List<string>() { "Iphone 7/8", "Iphone 7/8 Plus", "Iphone X", "Iphone XS", "Iphone XS MAX", "Iphone XR", "Samsung Galaxy S9", "Samsung Galaxy S9 Plus", "Samsung Galaxy Note 9", "Samsung Galaxy S10", "Samsung Galaxy S10 Plus", "Iphone 6/ 6s", "Samsung Galaxy S10E", "SamSung Galaxy S8", "SamSung Galaxy S8 Plus", "SamSung Galaxy Note 8", "Huawei P20 Pro", "Huawei P30", "Huawei P30 Pro", "Huawei Mate 20 Pro", "OnePlus 6", "Iphone 6/6 Plus" };
                            sizeChart = "";
                            break;
                        }

                    case "Sneaker":
                        {
                            DanhSachSize = new List<string>() { "US5(EU37)", "US6(EU38)", "US6.5(EU39)", "US7(EU40)", "US8(EU41)","US9(EU42)", "US10(EU43)", "US11(EU44)", "US12(EU45)",
                                "0", "US5 (EU35)", "US6 (EU36)", "US7 (EU37)", "US7.5 (EU38)", "US8 (EU39)", "US8.5 (EU40)", "US9 (EU41)", "US10 (EU42)", "US10.5 (EU43)", "US11 (EU44)", "US12 (EU45)"};
                            sizeChart = "https://i.imgur.com/b74xM69.jpg" + ", " + "http://i.imgur.com/6pDFRiv.jpg"; ;
                            break;
                        }
                    case "Blanket":
                        {
                            DanhSachSize = new List<string>() { "YOUTH", "LARGE", "XLARGE" };
                            DanhSachGia = new List<string>() { "45.95", "49.95", "59.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Blanket+size+chart.jpg";
                            break;
                        }
                    case "Boots":
                        {
                            DanhSachSize = new List<string>() { "US3(EU35)", "US3.5(EU35.5)", "US4(EU36)", "US4.5(EU36.5)", "US5(EU37)", "US5.5(EU38)", "US6(EU39)"
                                , "US6.5(EU39.5)", "US7(EU40)", "US7.5(EU40.5)",
                                "US8(EU41)", "US8.5(EU42)", "US9(EU42.5)", "US9.5(EU43)",
                                "US10(EU44)", "US10.5(EU44)", "US11(EU45)", "US11.5(EU45.5)", "US12(EU46)", "US13(EU47)", "0",
                                "US4.5 (EU35)", "US5 (EU35.5)", "US5.5 (EU36)",
                                "US6 (EU36.5)", "US6.5 (EU37)", "US7 (EU38)", "US7.5 (EU39)", "US8 (EU39.5)",
                                "US8.5 (EU40)", "US9 (EU40.5)", "US9.5 (EU41)", "US10 (EU42)",
                                "US10.5 (EU42.5)", "US11 (EU43)", "US11.5 (EU44)", "US12 (EU44.5)"
                            };
                            //Boots 74.95
                            sizeChart = "https://i.imgur.com/0jA9OzF.jpg";
                            break;
                        }
                    case "Car Seat":
                        {
                            sizeChart = "https://i.postimg.cc/Znbf4bf1/car-seat-covers.jpg";
                            break;
                        }
                    case "Umbrella":
                        {
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2019/03/Size-chart-for-UMBRELLAS.jpg";
                            break;
                        }
                    case "Shower Curtain":
                        {
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2018/06/sizing-chart-1.png";
                            break;
                        }
                    case "Sneaker Shoes":
                    case "Sporty Sneaker":
                    case "Chunky Sneakers":
                        {
                            DanhSachSize = new List<string>() { "US6(EU39)", "US7(EU40)", "US7.5(EU41)", "US8(EU41.5)", "US8.5(EU42)", "US9(EU42.5)", "US9.5(EU43)", "US10(EU44)", "0",
                            "US5.5 (EU36)", "US6 (EU36.5)", "US6.5 (EU37)", "US7 (EU38)", "US8 (EU39)",
                                "US8.5 (EU40)", "US9 (EU41)", "US9.5 (EU41.5)", "US10 (EU42)", "US10.5 (EU42.5)", "US11 (EU43)", "US11.5 (EU44)"};
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2019/07/Sneakers-Chunky-2019-July-Sizing-Chart.jpg";
                            break;
                        }
                    case "High Top":
                        {
                            DanhSachSize = new List<string>() { "US5(EU35)", "US6(EU36)", "US7(EU37)", "US7.5(EU38)", "US8(EU39)", "US8.5(EU40)",
                                "US9(EU41)", "US10(EU42)", "US10.5(EU43)", "US11(EU44)", "US12(EU45)" };
                            DanhSachGia = new List<string>() { "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95", "59.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Hightop+canvas+size+chart.jpg";
                            break;
                        }
                    case "Leather Boots":
                        {
                            DanhSachSize = new List<string>() { "US5 (EU38)", "US6 (EU39)", "US7 (EU40)", "US7.5 (EU41)", "US8.5 (EU42)", "US9.5 (EU43)", "US10 (EU44)", "US11 (EU45)", "US12 (EU46)", "0",
                           "US5 (EU35)", "US5.5 (EU36)", "US6 (EU37)", "US7 (EU38)", "US8 (EU39)", "US9 (EU40)", "US10 (EU41)", "US11 (EU42)", "US11.5 (EU43)", "US12 (EU44)"};
                            sizeChart = "https://pillowprofits.com/wp-content/uploads/2017/08/updated-boots-sizing-chart.jpg";
                            break;
                        }
                    case "Low top":
                        {
                            DanhSachSize = new List<string>() { "US5(EU35)", "US6(EU36)", "US7(EU37)", "US7.5(EU38)", "US8(EU39)", "US8.5(EU40)",
                                "US9(EU41)", "US10(EU42)", "US10.5(EU43)", "US11(EU44)", "US12(EU45)" };
                            DanhSachGia = new List<string>() { "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Lowtop+canvas+size+chart.jpg";
                            break;
                        }
                    case "Low Top":
                        {
                            DanhSachSize = new List<string>() { "US5(EU35)", "US6(EU36)", "US7(EU37)", "US7.5(EU38)", "US8(EU39)", "US8.5(EU40)",
                                "US9(EU41)", "US10(EU42)", "US10.5(EU43)", "US11(EU44)", "US12(EU45)" };
                            DanhSachGia = new List<string>() { "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95", "49.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Lowtop+canvas+size+chart.jpg";
                            break;
                        }
                    case "Round Carpet":
                        {
                            DanhSachSize = new List<string>() { "60cm", "100cm", "120cm","150cm" };
                            DanhSachGia = new List<string>() { "44.99", "54.99", "63.99", "74.99" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Round+Carpet+Size+(1).jpg";
                            break;
                        }
                    case "Quilt":
                        {
                            DanhSachSize = new List<string>() { "SINGLE", "TWIN", "QUEEN", "KING", "SUPPER KING" };
                            DanhSachGia = new List<string>() { "56.95", "59.95", "76.95", "82.95", "88.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Quilts+size+chart.jpg";
                            break;
                        }
                    case "Rug":
                        {
                            DanhSachSize = new List<string>() { "SMALL", "MEDIUM", "LARGE" };
                            DanhSachGia = new List<string>() { "54.95", "69.95", "89.95" };
                            sizeChart = "https://sizeallproduct.s3.amazonaws.com/Size+Chart/Size+Chart.jpg";
                            break;
                        }
                        #endregion
                }
                Folder folder = new Folder(path);
                if (folder.SubFolders.Count > 0)
                {
                    for (int j = 0; j < folder.SubFolders.Count; j++)
                    {
                        Folder itemFolder = folder.SubFolders[j] as Folder;
                        if (itemFolder.Files.Count > 0)
                        {
                            foreach (FileInfo file in itemFolder.Files)
                            {
                                    string pathImage = "";
                                    if (!String.IsNullOrEmpty(pathImage))
                                    {
                                        pathImage += ", ";
                                    }
                                    pathImage += "http://35.197.63.152/uploadcdn/" + folder.Name + "/" +
                                        itemFolder.Name + "/" + file.Name;
                                if (!String.IsNullOrEmpty(pathImage))
                                {
                                    if (!String.IsNullOrEmpty(sizeChart))
                                    {
                                        pathImage += ", " + sizeChart;
                                    }
                                    string title = ToTitleCase(RemoveLastedString(file.Name)).Trim();
                                    if (!title.ToLower().Replace(" ", "").Contains(name.ToLower().Replace(" ", "")))
                                    {
                                        title += " " + ToTitleCase(name);
                                    }
                                    var parent = new Export()
                                    {
                                        ID = id + "",
                                        Type = (name.Equals("Shade") || name.Equals("Auto Sun") || name.Equals("Car Seat") || name.Equals("Shower Curtain") || name.Equals("Umbrella")) ? "simple" : "variable",
                                        SKU = "",
                                        Name = title,
                                        Published = "1",
                                        IsFeatured = "0",
                                        VisibilityInCatalog = "visible",
                                        ShortDescription = "",
                                        Description = "<p>" + title + "</p>" + des,
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
                                        RegularPrice = name.Equals("Auto Sun") ? "39.95" : name.Equals("Car Seat") ? "59.95" : name.Equals("Shower Curtain") ? "49.95" : name.Equals("Umbrella") ? "38.95" : "",
                                        Categories = ToTitleCase(itemFolder.Name),
                                        Tags = ToTitleCase(itemFolder.Name),
                                        ShippingClass = "",
                                        Images = pathImage,
                                        Parent = "",
                                        GroupedProducts = "",
                                        Upsells = "",
                                        CrossSells = "",
                                        ExternalURL = "",
                                        ButtonText = "",
                                        Position = "",
                                        SwatchesAttributes = "",
                                    };
                                    if (name.Equals("Sneaker") || name.Equals("Boots") || name.Equals("Chunky Sneakers") || name.Equals("Sneaker Shoes") || name.Equals("Sporty Sneaker") || name.Equals("Leather Boots"))
                                    {
                                        parent.Attribute1Global = "1";
                                        parent.Attribute1Name = "Style";
                                        parent.Attribute1Value = "Men, Women";
                                        parent.Attribute1Visible = "1";

                                        parent.Attribute2Global = "1";
                                        parent.Attribute2Name = "Size";
                                        parent.Attribute2Value = GetValue(DanhSachSize);
                                        parent.Attribute2Visible = "1";
                                    }
                                    else if (!name.Equals("Auto Sun") && !name.Equals("Car Seat") && !name.Equals("Shower Curtain") && !name.Equals("Umbrella") && !name.Equals("Shade"))
                                    {
                                        parent.Attribute1Global = "1";
                                        parent.Attribute1Name = "Size";
                                        parent.Attribute1Value = GetValue(DanhSachSize);
                                        parent.Attribute1Visible = "1";
                                    }
                                    if (parent != null)
                                    {
                                        dsResult.Add(parent);
                                        id++;
                                        if (!name.Equals("Auto Sun") && !name.Equals("Car Seat") && !name.Equals("Shower Curtain") && !name.Equals("Umbrella") && !name.Equals("Shade"))
                                        {
                                            string Gender = "Men";
                                            for (int i = 0; i < DanhSachSize.Count; i++)
                                            {
                                                if (DanhSachSize[i].Equals("0"))
                                                {
                                                    Gender = "Women";
                                                }
                                                else
                                                {
                                                    var child = new Export()
                                                    {
                                                        ID = id + "",
                                                        Type = "variation",
                                                        SKU = "",
                                                        Name = title,
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
                                                        RegularPrice = name.Equals("Phone Case") ? "29.95" :
                                                        name.Equals("Sneaker") ? "59.95" :
                                                        name.Equals("Leather Boots") ? "65.95" :
                                                        (name.Equals("Chunky Sneakers") || name.Equals("Sneaker Shoes") || name.Equals("Sporty Sneaker")) ? "74.95" :
                                                        name.Equals("Boots") ? "74.95"
                                                        : DanhSachGia[i],
                                                        Categories = "",
                                                        Tags = parent.Tags,
                                                        ShippingClass = "",
                                                        Images = parent.Images,
                                                        Parent = "id:" + parent.ID,
                                                        GroupedProducts = "",
                                                        Upsells = "",
                                                        CrossSells = "",
                                                        ExternalURL = "",
                                                        ButtonText = "",
                                                        Position = "",
                                                        SwatchesAttributes = "",
                                                    };
                                                    if (name.Equals("Sneaker") || name.Equals("Boots") || name.Equals("Chunky Sneakers") || name.Equals("Sneaker Shoes") || name.Equals("Sporty Sneaker") || name.Equals("Leather Boots"))
                                                    {
                                                        child.Attribute1Global = "1";
                                                        child.Attribute1Name = "Style";
                                                        child.Attribute1Value = Gender;
                                                        child.Attribute1Visible = "1";

                                                        child.Attribute2Global = "1";
                                                        child.Attribute2Name = "Size";
                                                        child.Attribute2Value = DanhSachSize[i];
                                                        child.Attribute2Visible = "1";
                                                    }
                                                    else
                                                    {
                                                        child.Attribute1Global = "1";
                                                        child.Attribute1Name = "Size";
                                                        child.Attribute1Value = DanhSachSize[i];
                                                        child.Attribute1Visible = "";
                                                    }
                                                    if (child != null)
                                                    {
                                                        id++;
                                                        dsResult.Add(child);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Product is not found!");
                        }
                    }
                }
                File.WriteAllText(Environment.CurrentDirectory + "\\ID\\id.txt", id.ToString());
                return dsResult;
            }
            catch
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }
        public string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        public string RemoveLastedString(string str)
        {
            string result = "";
            var arr = str.Split(' ');
            foreach (var item in arr)
            {
                if (!String.IsNullOrEmpty(result))
                {
                    result += " ";
                }
                if (!item.Contains("jpeg") && !item.Contains("png") && !item.Contains("jpg"))
                {
                    result += item;
                }
            }
            return result;
        }
    }
}
