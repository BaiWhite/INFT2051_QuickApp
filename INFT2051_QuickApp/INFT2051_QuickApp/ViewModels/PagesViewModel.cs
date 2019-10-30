using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using INFT2051_QuickApp.Models;
using INFT2051_QuickApp.Views;
using INFT2051_QuickApp.Services;
using System.Collections.Generic;

namespace INFT2051_QuickApp.ViewModels
{
    public class PagesViewModel : BaseViewModel
    {
        public ObservableCollection<BusinessPage> BusinessPages { get; set; }
        public Command LoadPagesCommand { get; set; }

        public PagesViewModel()
        {
            CompanyName = "Browse";
            BusinessPages = new ObservableCollection<BusinessPage>();
            LoadPagesCommand = new Command(async () => await ExecuteLoadPagesCommand());

            MessagingCenter.Subscribe<NewItemPage, BusinessPage>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as BusinessPage;
                BusinessPages.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadPagesCommand()
        {
            ApiServices apiServices = new ApiServices();
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                BusinessPages.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    BusinessPages.Add(item);
                }
                List<BusinessPage> accesstoken = await apiServices.LoadPagesAsync();
                foreach (BusinessPage page in accesstoken)
                {
                    BusinessPages.Add(page);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}