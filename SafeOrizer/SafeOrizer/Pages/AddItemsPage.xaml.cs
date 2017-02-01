// <copyright file="AddItemsPage.xaml.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Pages
{
    using SafeOrizer.ViewModels;
    using Xamarin.Forms;

    /// <summary>
    /// Page to add items to the collection. Uses AddItemsViewModel and subscribes to Messages from it
    /// </summary>
    public partial class AddItemsPage : ContentPage
    {
        public AddItemsPage()
        {
            this.InitializeComponent();

            this.SubscribeToMessages();
        }

        /// <summary>
        /// Subscribes to all messages from the ViewModel
        /// </summary>
        private void SubscribeToMessages() =>
            MessagingCenter.Subscribe<AddItemsViewModel, string>(this, "DisplayAlert", async (sender, arg) =>
            {
                await this.DisplayAlert("Alert", arg, "OK");
            });
    }
}
