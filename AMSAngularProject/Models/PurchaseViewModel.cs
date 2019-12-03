using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMSAngularProject.Models
{
    public class PurchaseViewModel
    {
        public int purchaseId { get; set; }
        public string purchaseOrderNo { get; set; }
        public Nullable<int> purchaseAssetName { get; set; }
        public Nullable<int> purchaseAssetType { get; set; }
        public Nullable<int> purchaseVendorName { get; set; }
        public string assetName { get; set; }
        public string assetType { get; set; }
        public string vendorName { get; set; }
        public decimal quantity { get; set; }
      
       // public string vendorName { get; set; }
        public string purchaseDateStr { get; set; }
        public string purchaseDelivDateStr { get; set; }
        public string purchaseStatus { get; set; }
    }
}