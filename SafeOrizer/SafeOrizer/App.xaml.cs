using SafeOrizer.Helpers;
using SafeOrizer.Interfaces;
using SafeOrizer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SafeOrizer
{
    public partial class App : Application
    {
        static Database database;

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            var nav = new NavigationPage(new UnlockPage())
            {
                BarBackgroundColor = Color.FromRgb(244, 67, 54),
                BarTextColor = Color.White
            };
            

            this.MainPage = nav;
        }

        public static Database Database
        {
            get
            {
                if (database == null)
                { 
                    database = new Database(DependencyService.Get<IDbHelper>().GetLocalFilePath("SafeOrizer.db3"));
                }

                return database;
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
