using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMSAngularProject.Models
{
    public class MasterViewModel
    {
        public int assetMasterId { get; set; }
        public Nullable<int> assetType { get; set; }
        public string assetTypeName { get; set; }
        public Nullable<int> make { get; set; }
        public string makeName { get; set; }
        public Nullable<int> assetName { get; set; }
        public string assetDefName { get; set; }
        public string model { get; set; }
        public string serialnumber { get; set; }
        public string makeYear { get; set; }
        public string purchaseDate { get; set; }
        public string warranty { get; set; }
        public Nullable<System.DateTime> warrantyFrom { get; set; }
        public Nullable<System.DateTime> warrantyTo { get; set; }

    }
}