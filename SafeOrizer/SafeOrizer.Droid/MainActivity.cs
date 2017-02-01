using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using HockeyApp.Android;
//using HockeyApp.Android.Metrics;
using Plugin.Permissions;
//using Microsoft.Azure.Mobile;
using FFImageLoading.Forms.Droid;
using SafeOrizer.Droid.Helpers;

namespace SafeOrizer.Droid
{
    [Activity(Label = "SafeOrizer.Droid", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string HockeyAppId = "13286fc90aa24b5d865cf9ea067ced6f";
        const string VSMobileCenterAppId = "1752819c-152a-4a2e-b146-38e3e7d16854";

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            

            base.OnCreate(bundle);

            System.Diagnostics.Debug.Listeners.Add(new LogTraceListener());

            // Hockeyapp integration
            //CheckForUpdates();
            CrashManager.Register(this, HockeyAppId);
            //MetricsManager.Register(this.Application, this.HockeyAppId);

            // Mobile Center integration
            //MobileCenter.Configure(this.VSMobileCenterAppId);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init();

            LoadApplication(new App());
        }

        private void CheckForUpdates() =>
            // Remove this for store builds!
            UpdateManager.Register(this, HockeyAppId);

        private void UnregisterManagers() => 
            UpdateManager.Unregister();

        protected override void OnPause()
        {
            base.OnPause();
            //UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //UnregisterManagers();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) => 
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }
}

