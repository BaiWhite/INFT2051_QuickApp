using INFT2051_QuickApp.Services;
using Plugin.Media.Abstractions;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace INFT2051_QuickApp.ViewModels
{
    public class BusinessPageViewModel
    {
        private readonly ApiServices apiServices = new ApiServices();

        public string Id { get; set; }
        public string Description { get; set; }
        
        // public MediaFile CompanyImage { get; set; }

        public string StoreLongitude { get; set; }
        public string StoreLatitude { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var accesstoken = await apiServices.UploadPageAsync(ApplicationSettings.CompanyName, Description, StoreLatitude, StoreLongitude);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("caught exception");
                        Console.WriteLine(e.ToString());
                        throw;
                    }
                });
            }
        }

        public BusinessPageViewModel()
        {
        }
    }
}