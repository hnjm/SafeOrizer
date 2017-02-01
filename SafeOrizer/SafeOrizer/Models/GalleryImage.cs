// <copyright file="GalleryImage.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Models
{
    using System;
    using MvvmHelpers;
    using Xamarin.Forms;

    /// <summary>
    /// Model for the GalleryViewModel.
    /// </summary>
    public class GalleryImage : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryImage"/> class.
        /// </summary>
        public GalleryImage()
        {
            this.ImageId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the ImageId
        /// </summary>
        public Guid ImageId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Source of the Image
        /// </summary>
        public ImageSource Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Data of the Image
        /// </summary>
        public byte[] ImageData
        {
            get;
            set;
        }
    }
}