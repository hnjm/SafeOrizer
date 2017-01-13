using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SafeOrizer.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _itemCount = "-1";

        public string ItemCount
        {
            get => _itemCount;
            set => SetProperty(ref _itemCount, value, "ItemCount");
        }

        public ICommand ClearDbCommand { get; private set; }

        public SettingsViewModel()
        {
            this._itemCount = App.Database.GetItemCount().GetAwaiter().GetResult().ToString();
            this.ClearDbCommand = new Command(async () => await ClearDbAsync());
        }


        public async Task ClearDbAsync()
        {
            await App.Database.DeleteAllItems();

            this.ItemCount = "0";
        }

        public bool CanClearDb() => true;
    }
}
