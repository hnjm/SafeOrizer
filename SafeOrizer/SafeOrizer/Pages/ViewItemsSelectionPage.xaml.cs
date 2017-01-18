using SafeOrizer.Pages;
using System;

using Xamarin.Forms;

namespace SafeOrizer.Pages
{
    public partial class ViewItemsSelectionPage : ContentPage
    {
        public ViewItemsSelectionPage()
        {
            InitializeComponent();

            this.ViewFilesButton.Clicked += this.ViewFilesButton_ClickedAsync;

            this.ViewGalleryButton.Clicked += this.ViewGalleryButton_ClickedAsync;
        }

        private async void ViewGalleryButton_ClickedAsync(object sender, EventArgs e) => 
            await this.Navigation.PushAsync(new GalleryPage());

        private async void ViewFilesButton_ClickedAsync(object sender, EventArgs e) => 
            await this.Navigation.PushAsync(new FileListPage());
    }
}
