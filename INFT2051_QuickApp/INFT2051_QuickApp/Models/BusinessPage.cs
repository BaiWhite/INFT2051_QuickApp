using System;
using System.Collections.Generic;
using System.Text;

namespace INFT2051_QuickApp.Models
{
    public class BusinessPage
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        // public MediaFile CompanyImage { get; set; }

        public string StoreLongitude { get; set; }
        public string StoreLatitude { get; set; }

        public bool UploadSuccess { get; set; }
    }
}
