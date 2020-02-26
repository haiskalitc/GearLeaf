using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET.WooCommerce.v3.Extension;

namespace WooAPI
{
    public class WooComerceAPI
    {
        public async void Add()
        {
            //RestAPI rest = new RestAPI("http://35.184.118.137", "ck_bb6fdd89ce5e7d2ff523578227edab8987f5ef3d", "cs_edbaecabd3d83d1b2326f91ae05127eb01545fbe");
            //WCObject wc = new WCObject(rest);

            ////Get all products
            //var products = await wc.Product.GetAll();

            //////Add new product
            ////Product p = new Product()
            ////{
            ////    id = 1,
            ////    type = "",
            ////    sku = "",
            ////    name = "",
            ////    status = "1",
            ////    featured = true,
            ////    catalog_visibility = "visible",
            ////    short_description = "",
            ////    description = "",
            ////    date_on_sale_from = null,
            ////    date_on_sale_to = null,
            ////    tax_status = "taxable",
            ////    tax_class = "",
            ////    manage_stock = false, // Instock
            ////    stock_status = "", // stock status
            ////    stock_quantity = 0, // low stock
            ////    backorders_allowed = false,
            ////    sold_individually = false,
            ////    weight = 0,
            ////    reviews_allowed = true,
            ////    purchase_note = "",
            ////    sale_price = 0,
            ////    regular_price = 0,
            ////    categories = new List<ProductCategoryLine>() { new ProductCategoryLine() { } },
            ////    tags = new List<ProductTagLine>(),
            ////    shipping_class = "",
            ////    images = new List<ProductImage>() { new ProductImage() { src = "", } },
            ////    download_limit = 0,
            ////    download_expiry = 0,
            ////    parent_id = 0,
            ////    //
            ////    grouped_products = new List<int>() { },
            ////    upsell_ids = new List<int>() { },
            ////    attributes = new List<ProductAttributeLine>()
            ////    {
            ////        new ProductAttributeLine() {name = "", options = new List<string>() {""},variation = true, visible = true }
            ////        new ProductAttributeLine() {name = "", options = new List<string>() {""},variation = true, visible = true }
            ////        new ProductAttributeLine() {name = "", options = new List<string>() {""},variation = true, visible = true }
            ////    }
            ////};
            ////await wc.Product.Add(p);
            var client = new HttpClient();

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("ck_bb6fdd89ce5e7d2ff523578227edab8987f5ef3d", "cs_edbaecabd3d83d1b2326f91ae05127eb01545fbe"),});

            // Get the response.

            HttpResponseMessage response = await client.GetAsync(
                "http://35.184.118.137/wp-json/wc/v3/orders");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }
    }
}
