using FFImageLoading.Forms;
using System;
using System.IO;
using Xamarin.Forms;

namespace SafeOrizer.Views
{
    public partial class ViewGalleryPage : ContentPage
    {
        public ViewGalleryPage()
        {
            InitializeComponent();

            this.Appearing += this.ViewGalleryPage_Appearing; 
        }

        private void ViewGalleryPage_Appearing(object sender, EventArgs e)
        {
            this.LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            var images = await App.Database.GetImageItemsAsync();
            var videos = await App.Database.GetVideoItemsAsync();

            var i = 0;
            foreach (var image in images)
            {
                if (i == 5) return;

                var cachedImage = new CachedImage
                {
                    Source = ImageSource.FromStream(() =>
                    {
                        var stream = new MemoryStream(image.Data);
                        return stream;
                    }),
                    Margin = new Thickness(10)
                    
                };

                this.MainLayout.Children.Add(cachedImage);
                i++;
            }
        }
    }
}
