using PhanLoaiAnh.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanLoaiAnh
{
    public class Handle
    {
        public mainEntities db = new mainEntities();
        private Handle()
        {
        }
        public static Handle getInstance { get { return NestedSelenium.instance; } }
        private class NestedSelenium
        {
            static NestedSelenium()
            {
            }
            internal static readonly Handle instance = new Handle();
        }
        public List<LoaiSanPham> GetListLoaiSanPham()
        {
            try
            {
                var ds = db.LoaiSanPhams.ToList();
                if (ds.Count > 0)
                {
                    return ds;
                }
                return new List<LoaiSanPham>();
            }
            catch
            {
                return new List<LoaiSanPham>();
            }
        }

        public List<SanPham> GetListSanPham()
        {
            try
            {
                var ds = db.SanPhams.ToList();
                if (ds.Count > 0)
                {
                    return ds;
                }
                return new List<SanPham>();
            }
            catch(Exception ex)
            {
                return new List<SanPham>();
            }
        }
        public void ThemSanPham(List<string> ds)
        { 
            try
            {
                foreach (var item in ds)
                {
                   var add =  db.SanPhams.Add(new SanPham()
                    {
                        Name = item.Trim()
                    });
                    db.SaveChanges();
                }
            }
            catch(Exception EX)
            {
            }
        }
    }
}
