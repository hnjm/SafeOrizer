using Xamarin.Forms;
using SafeOrizer.Droid.Helpers;
using SafeOrizer.Interfaces;
using System.IO;

[assembly: Dependency(typeof(DbHelper))]
namespace SafeOrizer.Droid.Helpers
{
    public class DbHelper : IDbHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}