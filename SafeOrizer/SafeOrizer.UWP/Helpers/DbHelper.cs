using SafeOrizer.Interfaces;
using SafeOrizer.UWP.Helpers;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbHelper))]
namespace SafeOrizer.UWP.Helpers
{
    public class DbHelper : IDbHelper
    {
        public string GetLocalFilePath(string filename) => 
            Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
    }
}
