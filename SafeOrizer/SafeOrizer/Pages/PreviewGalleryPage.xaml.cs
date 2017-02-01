// <copyright file="PreviewGalleryPage.xaml.cs" company="Christoph Nienaber">
// Copyright (c) Christoph Nienaber. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for full license information.
// </copyright>

namespace SafeOrizer.Pages
{
    using System.Diagnostics;
    using Xamarin.Forms;

    /// <summary>
    /// A Page that displays a small horizontal collection of images and a large preview image on top
    /// </summary>
    public partial class PreviewGalleryPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewGalleryPage"/> class.
        /// </summary>
        public PreviewGalleryPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Overrides the OnAppearing event to load data from the ViewModel
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Debug.WriteLine(">>>>> Firing OnAppearing from PreviewGalleryPage");

            await this._viewModel.ExecuteLoadDataCommandAsync();
        }
    }
}
