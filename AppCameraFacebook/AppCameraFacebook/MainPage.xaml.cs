using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace AppCameraFacebook
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public async void TakePhotoButton_Clicked(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No Camera available.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true
                });
            if (file != null)
            {
                
                PathLabel.Text = file.AlbumPath;
                MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
        }
        public async void PickPhotoButton_Clicked(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ooops", "Pick photo is not supported !", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file != null)
            {
               
                PathLabel.Text = "Photo path" + file.Path;
                MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }

        }
        public async void TakeVideoButton_Clicked(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                await DisplayAlert("No Camera", ":( No Camera available", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
            {
                SaveToAlbum =true,
                Quality=VideoQuality.Medium,
            });
            if (file == null)
            {
                
                PathLabel.Text = "Photo Path" + file.Path;
                MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
        }
        public async void PickVideoButton_Clicked(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                await DisplayAlert("No Camera", ":( No Camera available", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickVideoAsync();
            
            if (file == null)
            {
               
                PathLabel.Text = "Photo Path" + file.Path;
                MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }
        }
    }
}
