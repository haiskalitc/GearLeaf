using AutoUploadAmazonS3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class SizeHandle
    {
        public mainEntities db = new mainEntities();
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

        public void Update(string idCategory, string name)
        {
            try
            {
                var item = db.Sizes.FirstOrDefault(model => model.IdCategories.Equals(idCategory));
                if (item != null)
                {
                    item.Name = name;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
