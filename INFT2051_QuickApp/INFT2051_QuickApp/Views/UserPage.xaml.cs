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
	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();
            if (ApplicationSettings.Paid)
            {
                UserPaymentStatus.Text = "Already Made Payment";
            }
            else
            {
                UserPaymentStatus.Text = "Pay For Own Company Page";
            }
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MakePaymentPage());
        }

        private void Button_Clicked_LogOut(object sender, EventArgs e)
        {
            ApplicationSettings.IsSignIn = false;
            ApplicationSettings.Paid = false;
            ApplicationSettings.CompanyName = null;
            ApplicationSettings.Password = null;
            ApplicationSettings.UserID = -1;

            App.Current.MainPage = new MainPage();
        }
    }
}