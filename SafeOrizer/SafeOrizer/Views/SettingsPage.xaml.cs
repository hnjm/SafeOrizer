using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SafeOrizer.Views
{
    public partial class SettingsPage : ContentPage
    {
        public int ItemCount { get; set; }

        public SettingsPage()
        {
            InitializeComponent();

            this.Appearing += this.SettingsPage_AppearingAsync;


            this.ClearDbButton.Clicked += this.ClearDbButton_ClickedAsync;
        }

        private async void SettingsPage_AppearingAsync(object sender, EventArgs e)
        {
            var allItems = await App.Database.GetAllItemsAsync();

            this.ItemCount = allItems.Count();
            this.ItemsCountLabel.Text = "Items Count: " + this.ItemCount.ToString();

        }
        private async void ClearDbButton_ClickedAsync(object sender, EventArgs e)
        {
            await App.Database.DeleteAllItems();

            this.ItemCount = 0;
            this.ItemsCountLabel.Text = "Items Count: " + this.ItemCount.ToString();
        }
            

    }
}
