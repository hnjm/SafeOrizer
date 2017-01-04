using SafeOrizer.Helpers;
using PCLCrypto;
using Plugin.Vibrate;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SafeOrizer.Views
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
                await Navigation.PushAsync(new BasePage());
            }
            else
            {
                var vibrator = CrossVibrate.Current;
                vibrator.Vibration(500);
            }
        }

        // https://github.com/AArnott/PCLCrypto/wiki/Crypto-Recipes#derive-a-symmetric-key-from-a-password
        private void TestEncryption()
        {
            string password = this.Passphrase;
            byte[] salt = new byte[16];
            NetFxCrypto.RandomNumberGenerator.GetBytes(salt); // Generate salt one time and save on the device
            int iterations = 5000; // higher makes brute force attacks more expensive
            int keyLengthInBytes = 256; // kreate a 256 bit key, maximum for AES
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);




        }

        //// Create a buffer with strong random data
        //byte[] cryptoRandomBuffer = new byte[15];
        //NetFxCrypto.RandomNumberGenerator.GetBytes(cryptoRandomBuffer);

        //// Generate a random number
        //uint randomFrom0To15 = WinRTCrypto.CryptographicBuffer.GenerateRandomNumber() % 16;

        //// Generate a HMAC for the data
        //        byte[] keyMaterial;
        //        byte[] data;
        //        var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
        //        CryptographicHash hasher = algorithm.CreateHash(keyMaterial);
        //        hasher.Append(data);
        //        byte[] mac = hasher.GetValueAndReset();
        //        string macBase64 = Convert.ToBase64String(mac);
    }
}
