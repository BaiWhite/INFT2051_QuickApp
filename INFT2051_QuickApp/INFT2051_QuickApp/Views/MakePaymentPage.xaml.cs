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
	public partial class MakePaymentPage : ContentPage
	{
		public MakePaymentPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new UserPage());
        }
    }
}