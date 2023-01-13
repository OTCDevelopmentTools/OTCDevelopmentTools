using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;
        public string Email { get; set; }
        public string Message { get; set; }

        public bool ShowMessage { get; set; }
        public bool ShowSendCodeTo { get; set; }

        private bool? m_ToEmail = true;

        public Command SendCommand { get => new Command(async () => await SendMethod()); }

        private string username = string.Empty;
        public string Username
        {
            get => username;
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ForgotPasswordViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
        }


        private async Task SendMethod()
        {
            if (string.IsNullOrEmpty(Username))
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Please enter username.", "Ok");
                return;
            }

            _progDialog.ShowProgress("Loading...");
            var result = await App.ServiceManager.ForgotPasswordAsync(Username);
            if (result)
            {
                Xamarin.Essentials.Preferences.Set("Username", Username);
                await App.Current.MainPage.Navigation.PushAsync(new ResetPasswordPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Somthing went wrong, please try again", "Ok");
            }

            _progDialog.HideProgress();
            
        }

        async Task SendEmail()
        {

        }
        bool ValidateEmail()
        {
            Message = "Email format is not correct";
            ShowMessage = true;
            return true;
        }
    }
}