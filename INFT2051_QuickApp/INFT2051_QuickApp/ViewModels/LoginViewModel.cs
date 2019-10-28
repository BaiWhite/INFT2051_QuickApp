using INFT2051_QuickApp.Services;
using INFT2051_QuickApp.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace INFT2051_QuickApp.ViewModels
{
    public class LoginViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        public string CompanyName { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var accesstoken = await _apiServices.LoginAsync(CompanyName, Password);

                        if (accesstoken.CompanyName != null)
                        {
                            ApplicationSettings.CompanyName = accesstoken.CompanyName;
                            ApplicationSettings.Password = accesstoken.Password;
                            ApplicationSettings.IsSignIn = true;
                            ApplicationSettings.Paid = accesstoken.paid;
                            ApplicationSettings.UserID = accesstoken.UserID;
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

        public LoginViewModel()
        {
            CompanyName = ApplicationSettings.CompanyName;
            Password = ApplicationSettings.Password;
        }
    }
}