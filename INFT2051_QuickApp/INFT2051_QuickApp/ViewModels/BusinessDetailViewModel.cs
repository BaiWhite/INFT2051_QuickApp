using System;

using INFT2051_QuickApp.Models;

namespace INFT2051_QuickApp.ViewModels
{
    public class BusinessDetailViewModel : BaseViewModel
    {
        public BusinessPage Item { get; set; }
        public BusinessDetailViewModel(BusinessPage item = null)
        {
            CompanyName = item?.CompanyName;
            Item = item;
        }
    }
}
