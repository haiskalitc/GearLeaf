using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CrawDataLolonesies
{
    public class GetProduct
    {
        public GetProduct()
        {
        }
        public List<Product> Get(string url)
        {
            var dsProduct = new List<Product>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Users\Administrator\Desktop\sitemap_products_1.xml");
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name.Equals("url"))
                    {
                        if (node.ChildNodes.Count > 3)
                        {
                            var URL = node.ChildNodes.Item(0).InnerText;
                            var title = node.ChildNodes.Item(3).ChildNodes.Item(1).InnerText;
                            if (!String.IsNullOrEmpty(URL) && !String.IsNullOrEmpty(title))
                            {
                                dsProduct.Add(new Product() { URL = URL, Title = title });
                            }
                        }
                    }
                }
                return dsProduct;
            }
            catch
            {
                return dsProduct;
            }
        }
    }
}
