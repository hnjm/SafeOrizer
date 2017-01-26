using SafeOrizer.Helpers;
using PCLCrypto;
using Plugin.Vibrate;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SafeOrizer.Pages
{
    public partial class UnlockPage : ContentPage
    {
        public string Passphrase { get; set; }

        public UnlockPage()
        {
            InitializeComponent();

            this.Appearing += this.UnlockView_AppearingAsync;

            this._CompleteButton.Clicked += this._CompleteButton_ClickedAsync;
        }

        private async void UnlockView_AppearingAsync(object sender, EventArgs e) => await TestSQLiteAsync();

        private void TestSettingsAsync()
        {
            Debug.WriteLine($"Connectionstring value was: {Settings.ConnectionString}");
            Settings.ConnectionString = "sudes";
            Debug.WriteLine($"Connectionstring value now is: {Settings.ConnectionString}");

        }

        private async Task TestSQLiteAsync()
        {
            //// CREATE
            //var conn = new SQLiteAsyncConnection(Settings.ConnectionString);

            //await conn.CreateTableAsync<EncryptedData>();

            //Debug.WriteLine($"Table for {nameof(EncryptedData)} created!");


            //// INSERT
            //var encData = new EncryptedData
            //{
            //    Data = new byte[10],
            //    FileName = "Test",
            //    FileType = FileType.File,
            //    Hash = "Yes"
            //};

            //await conn.InsertAsync(encData);

            //Debug.WriteLine("New customer ID: {0}", encData.Id);

            //// QUERY
            //var query = conn.Table<EncryptedData>();

            //var result = await query.ToListAsync();

            //Debug.WriteLine("Files in database:");

            //foreach (var file in result)
            //{
            //    Debug.WriteLine("ID: " + file.Id);
            //    Debug.WriteLine("Filename: " + file.FileName);
            //}

            //await App.Database.SaveItemAsync(encData);

            var items = await App.Database.GetAllItemsAsync();

            foreach (var item in items)
            {
                Debug.WriteLine($"File ID: {item.Id}, File name: {item.FileName}, File txpe: {item.FileType}");
            }
        }

        private async void _CompleteButton_ClickedAsync(object sender, EventArgs e)
        {
            this.Passphrase = this._CodeEntry.Text;

            if (this.Passphrase == "1337")
            {
                await App.Navigation.PushAsync(new GalleryPage());
            }
            else
            {
                var vibrator = CrossVibrate.Current;
                vibrator.Vibration(500);
            }
        }

      
    }
}
