using SafeOrizer.Helpers;
using SafeOrizer.Interfaces;
using SafeOrizer.Pages;
using Xamarin.Forms;

namespace SafeOrizer
{
    public partial class App : Application
    {
        private static Database _database;
        public static NavigationPage Navigation;

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            Navigation = new NavigationPage(new UnlockPage())
            {
                BarBackgroundColor = Color.FromRgb(244, 67, 54),
                BarTextColor = Color.White
            };
            

            this.MainPage = Navigation;
        }

        public static Database Database
        {
            get
            {
                if (_database == null)
                { 
                    _database = new Database(DependencyService.Get<IDbHelper>().GetLocalFilePath("SafeOrizer.db3"));
                }

                return _database;
            }
        }
        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
