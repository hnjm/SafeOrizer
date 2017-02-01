// <copyright file="GalleryViewModel.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using MvvmHelpers;
    using SafeOrizer.Helpers;
    using SafeOrizer.Models;
    using SafeOrizer.Pages;
    using Xamarin.Forms;

    /// <summary>
    /// ViewModel for all GalleryPages. Retrieves images from the <see cref="Database"/> and provides commands for manipulation
    /// </summary>
    public class GalleryViewModel : BaseViewModel
    {
        private Command previewImageCommand;
        private ObservableCollection<GalleryImage> images = new ObservableCollection<GalleryImage>();
        private ImageSource previewImage;
        private Command addItemCommand;
        private Command settingsCommand;
        private Command loadDataCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryViewModel"/> class.
        /// </summary>
        public GalleryViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the bindable collection of images
        /// </summary>
        public ObservableCollection<GalleryImage> Images
        {
            get => this.images ?? (this.images = new ObservableCollection<GalleryImage>());
            set
            {
                this.images = value;
                this.OnPropertyChanged(nameof(this.Images));
            }
        }

        /// <summary>
        /// Gets or sets the bindable PreviewImage
        /// </summary>
        public ImageSource PreviewImage
        {
            get => this.previewImage; set => this.SetProperty(ref this.previewImage, value);
        }

        /// <summary>
        /// Gets command to load data
        /// </summary>
        public Command LoadDataCommand => this.loadDataCommand ??
                    (this.loadDataCommand = new Command(async () => await this.ExecuteLoadDataCommandAsync()));

        /// <summary>
        /// Gets the Command to show a preview of a selected image
        /// </summary>
        public Command PreviewImageCommand => this.previewImageCommand ?? new Command<Guid>((img) =>
        {
            var image = this.images.Single(x => x.ImageId == img).ImageData;

            this.PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));
        });

        /// <summary>
        /// Gets the Command to add items to the collection
        /// </summary>
        public Command AddItemCommand => this.addItemCommand ??
            (this.addItemCommand = new Command(async () => await this.ExecuteAddItemCommandAsync()));

        /// <summary>
        /// Gets the Command to access the SettingsPage
        /// </summary>
        public Command SettingsCommand => this.settingsCommand ??
            (this.settingsCommand = new Command(async () => await this.ExecuteSettingsCommandAsync()));

        /// <summary>
        /// Execution of LoadDataCommand
        /// </summary>
        /// <returns>Task</returns>
        public async Task ExecuteLoadDataCommandAsync()
        {
            this.LoadDataCommand.ChangeCanExecute();

            await this.LoadDataAsync();

            this.LoadDataCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Loads data from the database
        /// </summary>
        /// <returns>Task</returns>
        private async Task LoadDataAsync()
        {
            // If the data source did not change, you don't need to load.
            if (!await this.CheckIfDataSourceChanged())
            {
                return;
            }

            this.LoadDataCommand.ChangeCanExecute();
            this.IsBusy = true;

            Debug.WriteLine(">>>>> Starting read from db");
            var images = await App.Database.GetImageItemsAsync();
            Debug.WriteLine(">>>>> Done reading from db");

            foreach (var image in images)
            {
                Debug.WriteLine($">>>>> Started reading image with ID {image.Id}");
                var imageSource = ImageSource.FromStream(() => new MemoryStream(image.EncryptableData));
                Debug.WriteLine($">>>>> Done reading image with ID {image.Id}");
                this.images.Add(new GalleryImage { Source = imageSource, ImageData = image.EncryptableData });
                Debug.WriteLine($">>>>> Added image with ID {image.Id} to collection");
            }

            Debug.WriteLine(">>>>> Done loading images.");

            this.LoadDataCommand.ChangeCanExecute();

            this.IsBusy = false;

            return;
        }

        /// <summary>
        /// Checks if the data source is changed and clears the collection if it did
        /// </summary>
        /// <returns>true if the data source changed, otherwise false</returns>
        private async Task<bool> CheckIfDataSourceChanged()
        {
            if (await App.Database.GetItemCount() != this.images.Count)
            {
                this.images.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Execution of AddItemCommand
        /// </summary>
        /// <returns>Task</returns>
        private async Task ExecuteAddItemCommandAsync()
        {
            this.AddItemCommand.ChangeCanExecute();

            await App.Navigation.PushAsync(new AddItemsPage());

            this.AddItemCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Execution of the SettingsCommand
        /// </summary>
        /// <returns>Task</returns>
        private async Task ExecuteSettingsCommandAsync()
        {
            this.SettingsCommand.ChangeCanExecute();

            await App.Navigation.PushAsync(new SettingsPage());

            this.SettingsCommand.ChangeCanExecute();
        }
    }
}