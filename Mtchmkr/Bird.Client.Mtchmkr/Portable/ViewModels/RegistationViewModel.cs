using Bird.Client.Mtchmkr.Business.ServiceCenter.Request;
using Bird.Client.Mtchmkr.Helpers;
using Bird.Client.Mtchmkr.Portable.Common;
using Bird.Client.Mtchmkr.Portable.Comon;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class RegistationViewModel : BaseViewModel
    {
        public string Message { get; private set; }
        public bool IsValid { get; private set; }
        private readonly IProgressDialog _progDialog;


        private string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string email = string.Empty;
        public string Email
        {
            get => email;
            set
            {
                if (email == value) return;
                email = value;
                OnPropertyChanged(nameof(Email));
                ValidEmail = ValidateEmail();
            }
        }

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

        private string password1 = string.Empty;
        public string Password1
        {
            get => password1;
            set
            {
                if (password1 == value) return;
                password1 = value;
                OnPropertyChanged(nameof(Password1));
                ValidatePasswords();
            }
        }

        private string password2 = string.Empty;
        public string Password2
        {
            get => password2;
            set
            {
                if (password2 == value) return;
                password2 = value;
                OnPropertyChanged(nameof(Password2));
                ValidatePasswords();
            }
        }

        private string telephone = string.Empty;
        public string Telephone
        {
            get => telephone;
            set
            {
                if (telephone == value) return;
                telephone = value;
                OnPropertyChanged(nameof(Telephone));
                ValidateTelephone();
            }
        }

        private bool m_ValidEmail = true;
        public bool ValidEmail
        {
            get => m_ValidEmail;
            set
            {
                //if (m_ValidEmail == value)
                    m_ValidEmail = value;
                OnPropertyChanged(nameof(ValidEmail));
                OnPropertyChanged(nameof(InvalidEmail));
            }
        }

        public bool InvalidEmail
        {
            get => !ValidEmail;
        }

        private bool m_ShowPassword = false;
        public bool ShowPassword
        {
            get { return !m_ShowPassword; }
            set
            {
                if (m_ShowPassword == value) return;
                m_ShowPassword = value;
                OnPropertyChanged(nameof(ShowPassword));
            }
        }
        private string m_ShowPasswordText = Constants.ShowPasswordText;
        public string ShowPasswordText
        {
            get => m_ShowPasswordText;
            set
            {
                if (m_ShowPasswordText == value) return;
                m_ShowPasswordText = value;
                OnPropertyChanged(nameof(ShowPasswordText));
            }
        }

        private string m_TelephoneText;
        public string TelephoneText
        {
            get => m_TelephoneText;
            set
            {
                if (m_TelephoneText == value) return;
                m_TelephoneText = value;
                OnPropertyChanged(nameof(TelephoneText));
            }
        }

        private string m_ShowPasswordImage = Constants.ShowPasswordImage;
        public string ShowPasswordImage
        {
            get => m_ShowPasswordImage;
            set
            {
                if (m_ShowPasswordImage == value) return;
                m_ShowPasswordImage = value;
                OnPropertyChanged(nameof(ShowPasswordImage));
            }
        }
        private bool m_ValidPassword;
        public bool ValidPassword
        {
            get { return m_ValidPassword; }
            set
            {
                if (m_ValidPassword == value) return;
                m_ValidPassword = value;
                OnPropertyChanged(nameof(ValidPassword));
            }
        }

        private string m_PasswordText;
        public string PasswordText
        {
            get { return m_PasswordText; }
            set
            {
                if (m_PasswordText == value) return;
                m_PasswordText = value;
                OnPropertyChanged(nameof(PasswordText));
            }
        }


        public ICommand TogglePassword { get => new Command(async () => await Toggle()); }
        public ICommand RegisterCommand { get => new Command(async () => await Register()); }

        private bool _applyValidation;

        async Task Toggle()
        {
            ShowPassword = ShowPassword;
            if (ShowPassword)
            {
                ShowPasswordText = Constants.ShowPasswordText;
                ShowPasswordImage = Constants.ShowPasswordImage;
            }
            else
            {
                ShowPasswordText = Constants.HidePasswordText;
                ShowPasswordImage = Constants.HidePasswordImage;
            }
        }
        public RegistationViewModel()
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
        }

        bool Validate()
        {
            ValidEmail = ValidateEmail();
            ValidPassword = ValidatePasswords();
            return ValidEmail && ValidPassword && ValidateTelephone();
        }

        bool ValidateEmail()
        {
            if (!_applyValidation) return true;
            ValidEmail = EmailHelper.ValidateEmail(Email);
            return ValidEmail;
        }

        bool ValidatePasswords()
        {
            if (!_applyValidation) return true;
            if (Password1 != Password2)
            {
                PasswordText = "The passwords must be be equal";
                return false;
            }
            if (!PasswordHelper.ValidatePassword(Password1, App.PasswordConstraint))
            {
                PasswordText = App.PasswordConstraint.ToString();
                return false;
            }
            return true;
        }

        bool ValidateTelephone()
        {
            if (!_applyValidation) return true;
            if (Telephone?.Length > 12 || Telephone?.Length < 7)
            {
                TelephoneText = "Please enter a valid telephone number";
                return false;
            }
            TelephoneText = String.Empty;
            return true;
        }

        async Task Register()
        {
            _applyValidation = true;
            if (Validate())
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Username)
                    || string.IsNullOrEmpty(Password1) || string.IsNullOrEmpty(Password2) || string.IsNullOrEmpty(Telephone))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "All fields are required.", "Ok");
                    return;
                }
                _progDialog.ShowProgress("Loading...");
                UserModel request = new UserModel();
                request.name = Name;
                request.email = Email;
                request.userName = Username;
                request.password = Password1;
                request.confirmPassword = Password2;
                request.telephone = Telephone;
                request.createdDate = DateTime.Now;

                var result = await App.ServiceManager.RegistrationAsync(request);
                if (result)
                {
                    var resultLogin = await App.ServiceManager.LoginAsync(Username, Password1);
                    if (resultLogin != null)
                    {
                        var profile1 = await Validate(Username, Password1);
                        App.LoadUser(profile1);
                        var Message = !App.IsUserLoggedIn;
                        var userId1 = resultLogin.userId.ToString();
                        Xamarin.Essentials.Preferences.Set("UserId", userId1);
                        Xamarin.Essentials.Preferences.Set("Username", Username);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Somthing went wrong, please try again", "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Somthing went wrong, please try again", "Ok");
                }

                _progDialog.HideProgress();
            }
        }

        private async Task<ProfileModel> Validate(string user, string pass)
        {
            return await Task<ProfileModel>.FromResult(ProfileHelper.Create());
        }
    }
}
