using INFT2051_QuickApp.Models;
using INFT2051_QuickApp.Services;
using INFT2051_QuickApp.ViewModels;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace INFT2051_QuickApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyAppPage : ContentPage
	{
        // private MediaFile mediaFile;
        // private BusinessPageViewModel businessPage = new BusinessPageViewModel();
        private bool initialed = false;
        private Label latitude;
        private Label longitude;
        private Label lastUpdated;

        public MyAppPage()
        {
            InitializeComponent();
            Label.Text = ApplicationSettings.CompanyName;
            this.latitude = this.FindByName<Label>("LatitudeLabel");
            this.longitude = this.FindByName<Label>("LongitudeLabel");
            this.lastUpdated = this.FindByName<Label>("LastUpdatedLabel");

            if (CrossGeolocator.Current.IsGeolocationAvailable)
                this.lastUpdated.Text = "Geolocation is unavailable. Make sure you enable location services on your device!";

            if (CrossGeolocator.Current.IsGeolocationEnabled)
                this.lastUpdated.Text = "Geolocation is not enabled";
        }

        //public async void PickPhoto_ClickedAsync(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("PickImage Not Working", "Unable Pick Photo", "OK");
        //        return;
        //    }

        //    mediaFile = await CrossMedia.Current.PickPhotoAsync();

        //    if (mediaFile == null)
        //    {
        //        return;
        //    }

        //    FileImage.Source = ImageSource.FromStream(() =>
        //    {
        //        initialed = true;
        //        return mediaFile.GetStream();
        //    });
        //}

        //public async void TakePhoto_ClickedAsync(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("No Camera", "Camera Not Avaliable", "OK");
        //        return;
        //    }

        //    mediaFile = await CrossMedia.Current.TakePhotoAsync(
        //        new StoreCameraMediaOptions
        //        {
        //            Directory = "sample",
        //            Name = "company.jpg"
        //        });

        //    if (mediaFile == null)
        //    {
        //        return;
        //    }

        //    FileImage.Source = ImageSource.FromStream(() =>
        //    {
        //        initialed = true;
        //        return mediaFile.GetStream();
        //    });
        //}

        // On appear, set the plugin to start listenting
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
        }

        // On dissapear, set the plugin to stop listening
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
        }

        // Called when the users position changes
        void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var position = e.Position;

            this.lastUpdated.Text = DateTime.Now.ToString();
            this.latitude.Text = position.Latitude.ToString();
            this.longitude.Text = position.Longitude.ToString();
        }

        void Current_PositionError(object sender, Plugin.Geolocator.Abstractions.PositionErrorEventArgs e)
        {
            this.lastUpdated.Text = "Error loading position";
            this.latitude.Text = "";
            this.longitude.Text = "";
        }

        public async void UploadFile_ClickedAsync(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            if (initialed)
            {
                // businessPage.CompanyImage = mediaFile;
            }
            try
            {
                BusinessPage accesstoken = await apiServices.UploadPageAsync(ApplicationSettings.CompanyName, Description.Text, LatitudeLabel.Text, LongitudeLabel.Text);
                initialed = accesstoken.UploadSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine("caught exception");
                Console.WriteLine(ex.ToString());
                throw;
            }
            if (initialed)
            {
                await DisplayAlert("Upload Success", "Page Successful Uploaded", "OK");
            }
        }
    }
}