using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanLoaiAnh.Models
{
    public class Collection
    {
        public string SanPham { get; set; }
        public List<MyFileInfo> DanhSachAnh { get; set; }
    }
}
