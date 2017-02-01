// <copyright file="AddItemsViewModel.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using SafeOrizer.Helpers;
    using SafeOrizer.Models;
    using Xamarin.Forms;

    /// <summary>
    /// ViewModel for the AddItemsPage. Contains business logic
    /// </summary>
    public class AddItemsViewModel
    {
        private Command addImagesCommand;
        private Command addVideosCommand;
        private Command captureImageCommand;
        private Command addFilesCommand;

        /// <summary>
        /// Gets the command to add images to the collection
        /// </summary>
        public Command AddImagesCommand => this.addImagesCommand ?? (this.addImagesCommand = new Command(async () => await this.ExecuteAddImagesCommand()));

        /// <summary>
        /// Gets the command to add videos to the collection
        /// </summary>
        public Command AddVideosCommand => this.addVideosCommand ?? (this.addVideosCommand = new Command(async () => await this.ExecuteAddVideosCommand()));

        /// <summary>
        /// Gets the command to capture new images for the collection
        /// </summary>
        public Command CaptureImageCommand => this.captureImageCommand ?? (this.captureImageCommand = new Command(async () => await this.ExecuteCaptureImageCommand()));

        /// <summary>
        /// Gets the command to add files to the collection
        /// </summary>
        public Command AddFilesCommand => this.addFilesCommand ?? (this.addFilesCommand = new Command(() => this.ExecuteAddFilesCommand()));

        /// <summary>
        /// Execution of AddImagesCommand
        /// </summary>
        /// <returns>Task</returns>
        public async Task ExecuteAddImagesCommand()
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
                    this.SaveMediaAsync(file, FileType.Image);
                }
            }
            else
            {
                // https://forums.xamarin.com/discussion/22499/looking-to-pop-up-an-alert-like-displayalert-but-from-the-view-model-xamarin-forms-labs
                // TO DO
                MessagingCenter.Send(this, "DisplayAlert", "Permission for storage is not granted or picking photo is not support");
            }
        }

        /// <summary>
        /// Execution of AddVideosCommand
        /// </summary>
        /// <returns>Task</returns>
        public async Task ExecuteAddVideosCommand()
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
                MessagingCenter.Send(this, "DisplayAlert", "Permission for storage is not granted or picking photo is not support");
            }
        }

        /// <summary>
        /// Execution of CaptureImageCommand
        /// </summary>
        /// <returns>Task</returns>
        public async Task ExecuteCaptureImageCommand()
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
                MessagingCenter.Send(this, "DisplayAlert", "Unable to take photos.");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }
        }

        /// <summary>
        /// Execution of AddFilesCommand
        /// </summary>
        public void ExecuteAddFilesCommand() => MessagingCenter.Send(this, "DisplayAlert", "Not Implemented");

        /// <summary>
        /// Saves media in the database
        /// </summary>
        /// <param name="file">the file to save</param>
        /// <param name="type">the filetype of the file</param>
        private async void SaveMediaAsync(MediaFile file, FileType type)
        {
            var binaryData = await ConvertHelpers.ReadFullyAsync(file.GetStream());

            var dataToSave = new Content
            {
                EncryptableData = binaryData,
                FileName = "Random file",
                FileType = type,
                DateAdded = DateTime.Now,
                Hash = binaryData.GetHashCode().ToString()
            };

            await App.Database.SaveItemAsync(dataToSave);

            MessagingCenter.Send(this, "DisplayAlert", "Media successfully added!");
        }
    }
}
