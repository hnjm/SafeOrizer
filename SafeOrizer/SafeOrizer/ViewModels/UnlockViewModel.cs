namespace SafeOrizer.ViewModels
{
    using System.Threading.Tasks;
    using MvvmHelpers;
    using Plugin.Vibrate;
    using SafeOrizer.Pages;
    using Xamarin.Forms;

    public class UnlockViewModel : BaseViewModel
    {
        private string passCode;
        private Command completeCommand;

        public string PassCode
        {
            get => this.passCode;
            set => this.SetProperty(ref this.passCode, value, "PassCode");
        }

        public Command CompleteCommand => this.completeCommand ??
                            (this.completeCommand = new Command(async () => await this.ExecuteCompleteCommandAsync()));

        public async Task ExecuteCompleteCommandAsync()
        {
            this.CompleteCommand.ChangeCanExecute();

            await this.CheckCompletionAsync();

            this.CompleteCommand.ChangeCanExecute();
        }

        private async Task CheckCompletionAsync()
        {
            if (this.passCode == "1337")
            {
                await App.Navigation.PushAsync(new PreviewGalleryPage());
            }
            else
            {
                var vibrator = CrossVibrate.Current;
                vibrator.Vibration(500);
            }
        }
    }
}
