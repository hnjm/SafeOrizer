// <copyright file="SettingsViewModel.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MvvmHelpers;
    using Xamarin.Forms;

    public class SettingsViewModel : BaseViewModel
    {
        private string itemCount = "-1";

        public SettingsViewModel()
        {
            this.itemCount = App.Database.GetItemCount().GetAwaiter().GetResult().ToString();
            this.ClearDbCommand = new Command(async () => await this.ClearDbAsync());
        }

        public string ItemCount
        {
            get => this.itemCount;
            set => this.SetProperty(ref this.itemCount, value, "ItemCount");
        }

        public ICommand ClearDbCommand { get; private set; }

        public async Task ClearDbAsync()
        {
            await App.Database.DeleteAllItems();

            this.ItemCount = "0";
        }
    }
}
