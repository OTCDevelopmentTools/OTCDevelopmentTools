using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Request;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Helpers;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    
    public class LoginViewModel : BaseViewModel
    {
        private ContentPage m_Page;
        public Command LoginCommand { get => new Command(async () => await Login()); }
        public Command ForgotCommand { get => new Command(async () => await Forgot()); }
        public Command RegisterCommand { get => new Command(async () => await Register()); }
        public bool Message { get; set; }
        private readonly IProgressDialog _progDialog;
        public ICommand OnGoogleLoginCommand { get; set; }
        IGoogleClientManager _googleService = CrossGoogleClient.Current;


        public LoginViewModel(ContentPage page)
        {
            m_Page = page;
            _progDialog = DependencyService.Get<IProgressDialog>();

            OnGoogleLoginCommand = new Command(async () => await LoginGoogleAsync());
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


        private string password = string.Empty;
        public string Password
        {
            get => password;
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool m_IsFingerprintAvailable=false;
        private bool m_CheckForFingerprint = false;

        async Task<bool> CheckFingerPrint()
        {
            if (!m_CheckForFingerprint)
            {
                m_IsFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(true);
                 m_CheckForFingerprint = true;
            }
            return m_IsFingerprintAvailable;
        }
        public Task<bool> IsFingerprintAvailable
        {
            get 
            {
                if (!m_CheckForFingerprint)
                {
                    var result =  CrossFingerprint.Current.IsAvailableAsync(true);
                    m_IsFingerprintAvailable = result.Result;
                    m_CheckForFingerprint = true;
                }
                return Task.FromResult<bool>(m_IsFingerprintAvailable);
            }
            set
            {
                if (m_IsFingerprintAvailable == value.Result) return;
                m_IsFingerprintAvailable = value.Result;
                OnPropertyChanged(nameof(IsFingerprintAvailable));
            }
        }

        

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
      //      m_Page.Navigation.PushAsync

        }

        async Task Register()
        {
            await Shell.Current.Navigation.PopToRootAsync(animated: true);
            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
        }
        async Task Forgot()
        {
            await Shell.Current.Navigation.PopToRootAsync(animated: true);
            await Shell.Current.GoToAsync($"//{nameof(ForgotPasswordPage)}");
            
        }
        private async Task Login()
        {
            LoginCommandExecuteMethod();
        }
        private async Task<ProfileModel> Validate(string user, string pass)
        {
            return await Task<ProfileModel>.FromResult(ProfileHelper.Create());
        }

        public async void LoginCommandExecuteMethod()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Please enter username and password.", "Ok");
                return;
            }

            _progDialog.ShowProgress("Loading...");

            var result = await App.ServiceManager.LoginAsync(Username, Password);
            if (result != null)
            {
                var profile = await Validate(Username, Password);
                App.LoadUser(profile);
                Message = !App.IsUserLoggedIn;
                var userId = result.userId.ToString();
                Xamarin.Essentials.Preferences.Set("UserId", userId);
                Xamarin.Essentials.Preferences.Set("Username", Username);

                FcmDeviceInfo requestFcmInfo = new FcmDeviceInfo();
                requestFcmInfo.deviceId = DependencyService.Get<IBaseUrl>().GetIdentifier();
                requestFcmInfo.deviceToken = Xamarin.Essentials.Preferences.Get("TokenDevice", string.Empty); ;
                requestFcmInfo.deviceType = DeviceInfo.Platform.ToString();
                requestFcmInfo.userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
                requestFcmInfo.createdDate = DateTime.Now;

                var res = await App.ServiceManager.InsertFCMInfoAsync(requestFcmInfo);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
            }

            _progDialog.HideProgress();
        }

        async Task LoginGoogleAsync()
        {
            try
            {
                //if (!string.IsNullOrEmpty(_googleService.AccessToken))
                //{
                //    //Always require user authentication
                //    _googleService.Logout();
                //}
                _googleService.Logout();
                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
#if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            var socialLoginData = new NetworkAuthData
                            {
                                Id = e.Data.Id,
                                Picture = e.Data.Picture.AbsoluteUri,
                                Name = e.Data.Name
                            };

                            var responseCheckUser = await App.ServiceManager.CheckUserExistAsync(e.Data.Email);

                            if (responseCheckUser)
                            {
                                _progDialog.ShowProgress("Loading...");
                                Username = e.Data.GivenName;
                                Password = e.Data.Name;

                                var result = await App.ServiceManager.LoginAsync(Username, Password);
                                if (result != null)
                                {
                                    var profile1 = await Validate(Username, Password);
                                    App.LoadUser(profile1);
                                    Message = !App.IsUserLoggedIn;
                                    var userId1 = result.userId.ToString();
                                    Xamarin.Essentials.Preferences.Set("UserId", userId1);
                                    Xamarin.Essentials.Preferences.Set("Username", Username);
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
                                }

                                _progDialog.HideProgress();
                            }
                            else
                            {
                                _progDialog.ShowProgress("Loading...");
                                UserModel request = new UserModel();
                                request.name = e.Data.Name;
                                request.email = e.Data.Email;
                                request.userName = e.Data.GivenName;
                                request.password = e.Data.Name;
                                request.confirmPassword = e.Data.Name;
                                request.telephone = "9876543210";
                                request.createdDate = DateTime.Now;

                                var result = await App.ServiceManager.RegistrationAsync(request);
                                if (result)
                                {
                                    var resultLogin = await App.ServiceManager.LoginAsync(request.userName, request.password);
                                    if (resultLogin != null)
                                    {
                                        var profile1 = await Validate(Username, Password);
                                        App.LoadUser(profile1);
                                        Message = !App.IsUserLoggedIn;
                                        var userId1 = resultLogin.userId.ToString();
                                        Xamarin.Essentials.Preferences.Set("UserId", userId1);
                                        Xamarin.Essentials.Preferences.Set("Username", Username);
                                    }
                                    else
                                    {
                                        await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Somthing went wrong, please try again", "Ok");
                                    }
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Somthing went wrong, please try again", "Ok");
                                }

                                _progDialog.HideProgress();
                            }

                            //Username = e.Data.Email;
                            //Password = "123456";
                            //var profile = await Validate(Username, Password);
                            //App.LoadUser(profile);
                            //Message = !App.IsUserLoggedIn;
                            //var userId = Guid.NewGuid().ToString();
                            //Xamarin.Essentials.Preferences.Set("UserId", userId);
                            //Xamarin.Essentials.Preferences.Set("Username", Username);
                            

                            //await App.Current.MainPage.Navigation.PushModalAsync(new HomePage(socialLoginData));
                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
