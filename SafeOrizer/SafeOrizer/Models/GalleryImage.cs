using System;
using Xamarin.Forms;
using MvvmHelpers;

namespace SafeOrizer.Models
{
    public class GalleryImage : ObservableObject
    {
        public GalleryImage()
        {
            ImageId = Guid.NewGuid();
        }

        public Guid ImageId
        {
            get;
            set;
        }

        public ImageSource Source
        {
            get;
            set;
        }

        public byte[] OrgImage
        {
            get;
            set;
        }

    }
}



// OLD

//using MvvmHelpers;
//using System;
//using Xamarin.Forms;

//namespace SafeOrizer.Models
//{
//    public class GalleryImage : ObservableObject
//    {
//        public GalleryImage()
//        {
//            this.ImageId = Guid.NewGuid();
//        }

//        ///// <summary>
//        ///// TODO
//        ///// </summary>
//        ///// <param name="source"></param>
//        ///// <param name="data"></param>
//        //public GalleryImage(ImageSource source, byte[] data)
//        //{
//        //    this.Source = source;
//        //    this.ImageData = data;
//        //    this.ImageId = Guid.NewGuid();
//        //}

//        public Guid ImageId
//        {
//            get;
//            set;
//        }

//        public ImageSource Source
//        {
//            get;
//            set;
//        }

//        public byte[] ImageData
//        {
//            get;
//            set;
//        }
//    }
//}
