using Xamarin.Forms;

namespace SafeOrizer.Views
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            InitializeComponent();

            this.AddButton.Clicked += this.AddButton_ClickedAsync;

            this.ViewButton.Clicked += this.ViewButton_ClickedAsync;

            this.SettingsButton.Clicked += this.SettingsButton_ClickedAsync;
        }

        private async void SettingsButton_ClickedAsync(object sender, System.EventArgs e) =>
            await this.Navigation.PushAsync(new SettingsPage());

        private async void ViewButton_ClickedAsync(object sender, System.EventArgs e) => 
            await this.Navigation.PushAsync(new ViewItemsSelectionPage());

        private async void AddButton_ClickedAsync(object sender, System.EventArgs e) => 
            await this.Navigation.PushAsync(new AddItemsPage());
    }
}
