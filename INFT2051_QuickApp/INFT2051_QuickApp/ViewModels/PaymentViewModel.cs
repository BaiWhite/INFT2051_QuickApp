using INFT2051_QuickApp.Services;
using INFT2051_QuickApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace INFT2051_QuickApp.ViewModels
{
    class PaymentViewModel
    {
        private readonly ApiServices apiServices = new ApiServices();

        public int CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string ExpiryDate { get; set; }
        public int SecurityCode { get; set; }
        public ICommand MakePaymentCommond
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var accesstoken = await apiServices.MakePaymentAsync(ApplicationSettings.UserID, CardNumber, NameOnCard, ExpiryDate, SecurityCode);

                        if (accesstoken)
                        {
                            ApplicationSettings.Paid = accesstoken;
                            App.Current.MainPage = new MainPage();
                        }
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

        public PaymentViewModel()
        {
        }
    }
}
