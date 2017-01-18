using SafeOrizer.Models;
using SafeOrizer.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Forms;

namespace SafeOrizer.Pages
{
    public partial class AddItemsPage : ContentPage
    {
        public AddItemsPage()
        {
            InitializeComponent();

            // Open the camera when clicked
            this.AddCameraButton.Clicked += this.AddCameraButton_ClickedAsync;

            // Open File Explorer when clicked
            this.AddFilesButton.Clicked += this.AddFilesButton_ClickedAsync;

            // Add images from the local gallery when clicked
            this.AddImagesButton.Clicked += this.AddImagesButton_ClickedAsync;

            this.AddVideosButton.Clicked += this.AddVideosButton_ClickedAsync;

            
        }

        private async void AddVideosButton_ClickedAsync(object sender, System.EventArgs e)
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }

            if (storageStatus == PermissionStatus.Granted && CrossMedia.Current.IsPickVideoSupported)
            {
                var videoFile = await CrossMedia.Current.PickVideoAsync();

                // Debug
                videoFile = null;
            }
            else
            {
                await DisplayAlert("No Permission", "Permission for storage is not granted or picking video is not support", "OK");
            }
        }

        private async void AddImagesButton_ClickedAsync(object sender, System.EventArgs e)
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }

            if (storageStatus == PermissionStatus.Granted && CrossMedia.Current.IsPickPhotoSupported)
            {
                // Select the image
                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file != null)
                {
                    SaveMediaAsync(file, FileType.Image);
                }
               
            }
            else
            {
                await DisplayAlert("No Permission", "Permission for storage is not granted or picking photo is not support", "OK");
            }
        }
        private async void AddFilesButton_ClickedAsync(object sender, System.EventArgs e) => 
            await DisplayAlert("Files", "not implemented", "I know");

        private async void AddCameraButton_ClickedAsync(object sender, System.EventArgs e)
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = false,
                    DefaultCamera = CameraDevice.Front,
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file != null)
                {
                    SaveMediaAsync(file, FileType.Image);
                }
                
            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }
        }

        public async void SaveMediaAsync(MediaFile file, FileType type)
        {
            var binaryData = await ConvertHelpers.ReadFullyAsync(file.GetStream());

            var dataToSave = new EncryptedData
            {
                Data = binaryData,
                FileName = "Random file",
                FileType = type,
                DateAdded = DateTime.Now,
                Hash = binaryData.GetHashCode().ToString()

            };

            await App.Database.SaveItemAsync(dataToSave);

            await DisplayAlert("Success!", "Media successfully added!", "Hell yeah!");
        }


    }
}
