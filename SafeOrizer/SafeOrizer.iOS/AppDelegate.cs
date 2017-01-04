using Foundation;
using UIKit;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using FFImageLoading.Forms.Touch;

namespace SafeOrizer.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        string VSMobileCenterAppId = "e57e54e2-1952-44d5-98bf-f43678a91bcd";
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        { 

            global::Xamarin.Forms.Forms.Init();

            // Mobile center integration
            MobileCenter.Start(this.VSMobileCenterAppId,
                    typeof(Analytics), typeof(Crashes));

            CachedImageRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
