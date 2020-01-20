using AutoUploadAmazonS3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class CountHandle
    {
        public mainEntities db = new mainEntities();
        private CountHandle()
        {
        }
        public static CountHandle getInstance { get { return NestedSelenium.instance; } }
        private class NestedSelenium
        {
            static NestedSelenium()
            {
            }
            internal static readonly CountHandle instance = new CountHandle();
        }

        public Count FindElementsById(long id)
        {
            try
            {
                var item = db.Counts.FirstOrDefault(model => model.Id.Equals(id));
                if (item != null)
                {
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Update(long newValue, long id)
        {
            try
            {
                var item = db.Counts.FirstOrDefault(model => model.Id.Equals(id));
                if (item != null)
                {
                    item.Count1 = newValue;
                    if (db.SaveChanges() > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
