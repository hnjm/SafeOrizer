using System;
using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Plugin.Media.Abstractions;
using System.Linq;
using SafeOrizer.Models;
using SafeOrizer.Pages;

namespace SafeOrizer.ViewModels
{
    public class GalleryViewModel : BaseViewModel
    {
        ICommand _cameraCommand, _previewImageCommand = null;
        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();
        ImageSource _previewImage = null;
        Command _addItemCommand, _settingsCommand, _loadDataCommand;
        
        public GalleryViewModel()
        {
        }

        /// <summary>
        /// Command to load acquaintances
        /// </summary>
        public Command LoadDataCommand => this._loadDataCommand ??
                    (this._loadDataCommand = new Command(async () => await ExecuteLoadDataCommandAsync()));

        public async Task ExecuteLoadDataCommandAsync()
        {
            this.LoadDataCommand.ChangeCanExecute();

            await this.LoadDataAsync();

            this.LoadDataCommand.ChangeCanExecute();
        }

        private async Task LoadDataAsync()
        {
            if (this._images.Count > 0)
            {
                return;
            }

            var images = await App.Database.GetImageItemsAsync();
            //var videos = await App.Database.GetVideoItemsAsync();

            var i = 0;
            foreach (var image in images)
            {
                // DEBUG for PERFORMANCE
                if (i == 6) return;

                var imageSource = ImageSource.FromStream(() => new MemoryStream(image.Data));

                this._images.Add(new GalleryImage { Source = imageSource, OrgImage = image.Data });
                i++;
            }

            return;
        }

        public ObservableCollection<GalleryImage> Images => _images;
        public ImageSource PreviewImage
        {
            get => _previewImage; set => SetProperty(ref _previewImage, value);
        }

        public ICommand CameraCommand => this._cameraCommand ?? 
            new Command(async () => await ExecuteCameraCommand(), () => CanExecuteCameraCommand());

        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            return true;
        }

        public async Task ExecuteCameraCommand()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });

            if (file == null)
                return;


            byte[] imageAsBytes = null;
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

            _images.Add(new GalleryImage { Source = imageSource, OrgImage = imageAsBytes });

            return;
        }

        public ICommand PreviewImageCommand
        {
            get
            {
                return _previewImageCommand ?? new Command<Guid>((img) => {

                    var image = _images.Single(x => x.ImageId == img).OrgImage;

                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));

                });
            }
        }

        public Command AddItemCommand => this._addItemCommand ??
            (this._addItemCommand = new Command(async () => await ExecuteAddItemCommandAsync()));

        public async Task ExecuteAddItemCommandAsync()
        {
            this.AddItemCommand.ChangeCanExecute();

            await App.Navigation.PushAsync(new AddItemsPage());

            this.AddItemCommand.ChangeCanExecute();
        }

        public Command SettingsCommand => this._settingsCommand ??
            (this._settingsCommand = new Command(async () => await ExecuteSettingsCommandAsync()));

        private async Task ExecuteSettingsCommandAsync()
        {
            this.SettingsCommand.ChangeCanExecute();

            await App.Navigation.PushAsync(new SettingsPage());

            this.SettingsCommand.ChangeCanExecute();
        }

    }
}




// OLD

//using FFImageLoading.Forms;
//using MvvmHelpers;
//using SafeOrizer.Models;
//using SafeOrizer.Views;
//using System;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Xamarin.Forms;

//namespace SafeOrizer.ViewModels
//{
//    public class GalleryViewModel : BaseViewModel
//    {
//        Command _loadDataCommand;
//        Command _addItemCommand;
//        Command _settingsCommand;

//        ICommand _previewImageCommand;

//        ImageSource _previewImage = null;
//        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();

//        public ImageSource PreviewImage
//        {
//            get => this._previewImage;
//            set => SetProperty(ref this._previewImage, value);
//        }

//        public ObservableCollection<GalleryImage> Images => this._images;

//        public ICommand PreviewImageCommand => this._previewImageCommand ?? new Command<Guid>((img) =>
//        {
//            var image = this._images.Single(x => x.ImageId == img).ImageData;
//            this.PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));
//        });


//        public Command AddItemCommand => this._addItemCommand ??
//            (this._addItemCommand = new Command(async () => await ExecuteAddItemCommandAsync()));

//        public async Task ExecuteAddItemCommandAsync()
//        {
//            this.AddItemCommand.ChangeCanExecute();

//            await App.Navigation.PushAsync(new AddItemsPage());

//            this.AddItemCommand.ChangeCanExecute();
//        }

//        public Command SettingsCommand => this._settingsCommand ??
//            (this._settingsCommand = new Command(async () => await ExecuteSettingsCommandAsync()));

//        private async Task ExecuteSettingsCommandAsync()
//        {
//            this.SettingsCommand.ChangeCanExecute();

//            await App.Navigation.PushAsync(new SettingsPage());

//            this.SettingsCommand.ChangeCanExecute();
//        }

//        /// <summary>
//        /// Command to load acquaintances
//        /// </summary>
//        public Command LoadDataCommand => this._loadDataCommand ??
//                    (this._loadDataCommand = new Command(async () => await ExecuteLoadDataCommandAsync()));

//        public async Task ExecuteLoadDataCommandAsync()
//        {
//            this.LoadDataCommand.ChangeCanExecute();

//            await this.LoadDataAsync();

//            this.LoadDataCommand.ChangeCanExecute();
//        }

//        private async Task LoadDataAsync()
//        {
//            var images = await App.Database.GetImageItemsAsync();
//            //var videos = await App.Database.GetVideoItemsAsync();

//            this._images = new ObservableCollection<GalleryImage>();

//            var i = 0;
//            foreach (var image in images)
//            {
//                // DEBUG for PERFORMANCE
//                if (i == 5) return;

//                //var cachedImage = new CachedImage
//                //{
//                //    Source = ImageSource.FromStream(() =>
//                //    {
//                //        var stream = new MemoryStream(image.Data);
//                //        return stream;
//                //    }),
//                //    Margin = new Thickness(10)
//                //};

//                //var imageSource = ImageSource.FromStream(() =>
//                //{
//                //    var stream = new MemoryStream(image.Data);
//                //    return stream;
//                //});

//                var imageSource = ImageSource.FromStream(() => new MemoryStream(image.Data));

//                this._images.Add(new GalleryImage { Source = imageSource, ImageData = image.Data });
//                i++;
//            }
//        }
//    }
//}