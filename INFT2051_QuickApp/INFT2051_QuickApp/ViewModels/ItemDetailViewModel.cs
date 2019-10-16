using System;

using INFT2051_QuickApp.Models;

namespace INFT2051_QuickApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            CompanyName = item?.CompanyName;
            Item = item;
        }
    }
}
