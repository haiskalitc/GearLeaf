using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawReviewAmazon
{
    public class Review
    {
        public string OrderID { get; set; }
        public string Customer { get; set; }
        public string TransactionID { get; set; }
        public string TrackingCode { get; set; }
        public string CarrierName { get; set; }
    }
}
