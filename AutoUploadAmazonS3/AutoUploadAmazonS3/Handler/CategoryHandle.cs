using AutoUploadAmazonS3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class CategoryHandle
    {
        public Entities db = new Entities();
        private CategoryHandle()
        {
        }
        public static CategoryHandle getInstance { get { return NestedSelenium.instance; } }
        private class NestedSelenium
        {
            static NestedSelenium()
            {
            }
            internal static readonly CategoryHandle instance = new CategoryHandle();
        }
        public List<Category> GetList()
        {
            try
            {
                var ds = db.Categories.ToList();
                if (ds.Count() > 0)
                {
                    return ds;
                }
                return new List<Category>();
            }
            catch
            {
                return new List<Category>();
            }
        }
    }
}
