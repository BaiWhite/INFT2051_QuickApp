using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using INFT2051_QuickApp.Models;
using INFT2051_QuickApp.ViewModels;

namespace INFT2051_QuickApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        BusinessDetailViewModel viewModel;

        public ItemDetailPage(BusinessDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            LocationLabel.Text = "StoreLongitude" + viewModel.Item.StoreLongitude + "; StoreLatitude" + viewModel.Item.StoreLatitude;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new BusinessPage
            {
                CompanyName = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new BusinessDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}