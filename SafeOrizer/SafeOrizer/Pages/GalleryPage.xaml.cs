using Xamarin.Forms;
using SafeOrizer.ViewModels;

namespace SafeOrizer.Pages
{
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await this._viewModel.ExecuteLoadDataCommandAsync();
        }
    }
}








// OLD

//using SafeOrizer.ViewModels;
//using System;
//using Xamarin.Forms;

//namespace SafeOrizer.Views
//{
//    public partial class GalleryPage : ContentPage
//    {
//        //protected GalleryViewModel ViewModel => this.BindingContext as GalleryViewModel;
//        private GalleryViewModel _viewModel;

//        public GalleryPage()
//        {
//            InitializeComponent();

//            this._viewModel = new GalleryViewModel();
//            this.BindingContext = this._viewModel;


//            //// on Android, we use a floating action button, so clear the ToolBarItems collection
//            //if (Device.OS == TargetPlatform.Android)
//            //{
//            //    this.ToolbarItems.Remove(this.addToolbarItem);

//            //    this.fab.Clicked = this.AndroidAddButtonClicked;
//            //}
//        }

//        protected override async void OnAppearing()
//        {
//            base.OnAppearing();

//            await this._viewModel.ExecuteLoadDataCommandAsync();
//        }


//        ///// <summary>
//        ///// The action to take when the + ToolbarItem is clicked on Android.
//        ///// </summary>
//        ///// <param name="sender">The sender.</param>
//        ///// <param name="e">The EventArgs</param>
//        //void AndroidAddButtonClicked(object sender, EventArgs e) => this.Navigation.PushAsync(new AddItemsPage());


//    }
//}
