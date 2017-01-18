using MvvmHelpers;
using Plugin.Vibrate;
using SafeOrizer.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SafeOrizer.ViewModels
{
    public class UnlockViewModel : BaseViewModel
    {
        string _passCode;
        Command _completeCommand;

        public string PassCode
        {
            get => _passCode;
            set => SetProperty(ref _passCode, value, "PassCode");
        }

        public Command CompleteCommand => this._completeCommand ??
                            (this._completeCommand = new Command(async () => await ExecuteCompleteCommandAsync()));

        public async Task ExecuteCompleteCommandAsync()
        {
            this.CompleteCommand.ChangeCanExecute();

            await CheckCompletionAsync();

            this.CompleteCommand.ChangeCanExecute();
        }

        private async Task CheckCompletionAsync()
        {
            if (this._passCode == "1337")
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
