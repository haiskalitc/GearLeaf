using AutoUploadAmazonS3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handler
{
    public class ColorsHandle
    {
        public Entities db = new Entities();
        private ColorsHandle()
        {
        }
        public static ColorsHandle getInstance { get { return NestedSelenium.instance; } }
        private class NestedSelenium
        {
            static NestedSelenium()
            {
            }
            internal static readonly ColorsHandle instance = new ColorsHandle();
        }
        public List<Color> GetList()
        {
            try
            {
                var ds = db.Colors.ToList();
                if (ds.Count() > 0)
                {
                    return ds;
                }
                return new List<Color>();
            }
            catch
            {
                return new List<Color>();
            }
        }
    }
}
