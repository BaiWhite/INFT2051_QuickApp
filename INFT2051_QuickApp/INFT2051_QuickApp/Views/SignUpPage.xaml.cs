using INFT2051_QuickApp.Models;
using INFT2051_QuickApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051_QuickApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private readonly ApiServices apiServices = new ApiServices();

        public SignUpPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                CompanyName = usernameEntry.Text,
                Password = passwordEntry.Text,
            };
            var signUpSucceeded = TryingSignUpAsync(user);
            if (await signUpSucceeded)
            {
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
        }

        async Task<bool> TryingSignUpAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.CompanyName) || string.IsNullOrWhiteSpace(user.Password) || (confirmPassworkdEntry.Text != passwordEntry.Text))
            {
                return false;
            }
            try
            {
                var accesstoken = await apiServices.RegisterUserAsync(user);

                if (accesstoken.CompanyName != null)
                {
                    ApplicationSettings.CompanyName = accesstoken.CompanyName;
                    ApplicationSettings.Password = accesstoken.Password;
                    ApplicationSettings.IsSignIn = true;
                    ApplicationSettings.Paid = accesstoken.paid;
                    ApplicationSettings.UserID = accesstoken.UserID;
                    App.Current.MainPage = new MainPage();
                }
                else
                {
                    messageLabel.Text = "Company Name Already Been Used";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("caught exception");
                Console.WriteLine(e.ToString());
                throw;
            }
            return true;
        }
    }
}