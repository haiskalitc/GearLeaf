using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUploadAmazonS3.Model
{
    public class Export
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Published { get; set; }
        public string IsFeatured { get; set; }
        public string VisibilityInCatalog { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string DataSalePriceStarts { get; set; }
        public string DataSalePriceEnds { get; set; }
        public string TaxStatus { get; set; }
        public string TaxClass { get; set; }
        public string InStock { get; set; }
        public string Stock { get; set; }
        public string LowStockAmount { get; set; }
        public string BackordersAllowed { get; set; }
        public string SoldIndividually { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string AllowCustomerReviews { get; set; }
        public string PurchaseNote { get; set; }
        public string SalePrice { get; set; }
        public string RegularPrice { get; set; }
        public string Categories { get; set; }
        public string Tags { get; set; }
        public string ShippingClass { get; set; }
        public string Images { get; set; }
        public string DownloadLimit { get; set; }
        public string DownloadExpiryDays { get; set; }
        public string Parent { get; set; }
        public string GroupedProducts { get; set; }
        public string Upsells { get; set; }
        public string CrossSells { get; set; }
        public string ExternalURL { get; set; }
        public string ButtonText { get; set; }
        public string Position { get; set; }
        public string SwatchesAttributes { get; set; }
        public string Attribute1Name { get; set; }
        public string Attribute1Value { get; set; }
        public string Attribute1Visible { get; set; }
        public string Attribute1Global { get; set; }
        public string Attribute1Default { get; set; }
        public string Attribute2Name { get; set; }
        public string Attribute2Value { get; set; }
        public string Attribute2Visible { get; set; }
        public string Attribute2Global { get; set; }
        public string Attribute2Default { get; set; }

    }
}
