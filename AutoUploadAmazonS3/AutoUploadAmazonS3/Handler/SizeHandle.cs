using AutoUploadAmazonS3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class SizeHandle
    {
        public Entities db = new Entities();
        private SizeHandle()
        {
        }
        public static SizeHandle getInstance { get { return NestedSelenium.instance; } }
        private class NestedSelenium
        {
            static NestedSelenium()
            {
            }
            internal static readonly SizeHandle instance = new SizeHandle();
        }

        public Size FindElementsById(string idCategory)
        {
            try
            {
                var item = db.Sizes.FirstOrDefault(model => model.IdCategories.Equals(idCategory));
                if (item != null)
                {
                    return item;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int Update(string idCategory, string name)
        {
            try
            {
                var item = db.Sizes.FirstOrDefault(model => model.IdCategories.Equals(idCategory));
                if (item != null)
                {
                    item.Name = name;
                    if (db.SaveChanges() > 0)
                    {
                        return 1;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int UpdateSize(long idCategory, string name)
        {
            try
            {
                var item = db.Categories.FirstOrDefault(model => model.Id.Equals(idCategory));
                if (item != null)
                {
                    item.SizeChart = name;
                    db.Entry(item).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        return 1;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
