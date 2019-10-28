
using INFT2051_QuickApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051_QuickApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}