using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        private string password = string.Empty;
        public string NewPassword
        {
            get => password;
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword == value) return;
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public Command SendCommand { get => new Command(() => SendMethod()); }

        public ResetPasswordViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
        }

        private async void SendMethod()
        {
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "All fields are required.", "Ok");
                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "The passwords must be be equal", "Ok");
                return;
            }
            
            _progDialog.ShowProgress("Loading...");

            var username = Xamarin.Essentials.Preferences.Get("Username",string.Empty);

            var httpClient = new HttpClient();
            HttpContent httpContent = new StringContent(string.Empty);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PutAsync(String.Format("https://mtchmkr2022.azurewebsites.net/User/ResetPassword?userName={0}&newPassword={1}", username, NewPassword), httpContent);

            if (response.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Password reset successfully", "Ok");
                App.Current.MainPage = new LoginShell();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
            }

            _progDialog.HideProgress();
        }

    }
}