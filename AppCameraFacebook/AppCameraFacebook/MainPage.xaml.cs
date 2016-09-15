using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;

namespace AppCameraFacebook
{

    public partial class MainPage : ContentPage
    {
        private MediaFile _mediaFile;
        public MainPage()
        {
            InitializeComponent();
        }
        public async void TakePhotoButton_Clicked(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Erro Camera", ":( Não é possivel utilizar a câmera agora." + Environment.NewLine + "Tente novamente mais tarde.", "OK");
                return;
            }
            _mediaFile = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true
                });
            if (_mediaFile != null)
            {
                
                PathLabel.Text = _mediaFile.AlbumPath;
                MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = _mediaFile.GetStream();
                    _mediaFile.Dispose();
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
             _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile != null)
            {
               
                PathLabel.Text = "Photo path" + _mediaFile.Path;
                MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = _mediaFile.GetStream();
                    _mediaFile.Dispose();
                    return stream;
                });
            }

        }

        public async void UploadPhoto_Clicked(Object sender, EventArgs e)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(_mediaFile.GetStream()),
            "\"file\"",
            $"\"{_mediaFile.Path}\"");
            var httpClient = new HttpClient();
            var url = "your adress here endereco aqui";
            var httpResponseMessage = await httpClient.PostAsync(url, content);
            string retorno = await httpResponseMessage.Content.ReadAsStringAsync();

        }


    }
}
