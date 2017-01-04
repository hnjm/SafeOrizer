using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SafeOrizer.Views
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
            await this.Navigation.PushAsync(new ViewGalleryPage());

        private async void ViewFilesButton_ClickedAsync(object sender, EventArgs e) => 
            await this.Navigation.PushAsync(new ViewFilesPage());
    }
}
