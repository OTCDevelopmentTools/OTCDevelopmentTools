//using Bird.Client.Mtchmkr.Portable.Services;
//using Bird.Client.Mtchmkr.Portable.Views;
using Bird.Client.Mtchmkr.Business.Repositories;
using Bird.Client.Mtchmkr.Portable.Common;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using Plugin.PushNotification;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bird.Client.Mtchmkr.Portable
{
    public partial class App : Application
    {
        private static App m_App;
        private static bool m_IsUserLoggedIn = false;
        private static bool m_IsUserAdmin = false;
        public static MtchmkrAppRepository ServiceManager { get; private set; }

        public static bool IsUserLoggedIn
        {
            get { return m_IsUserLoggedIn; }
            //set
            //{
            //    if (m_IsUserLoggedIn != value)
            //        m_IsUserLoggedIn = value;           
            //}
        }
        public static async Task<bool> Login(string userName, string password)
        {
            return true;
        }
        public static async Task<bool> LogOut()
        {
            return true;
        }
        public static void LoadUser(ProfileModel profile)
        {
            
            if (null != profile)
            {
               // m_App.Profile = profile;
                m_IsUserLoggedIn = true;
                m_IsUserAdmin = profile.IsAdmin;
                m_App.LoadShell();
            }
        }
        public App()
        {
            InitializeComponent();
            ServiceManager = new MtchmkrAppRepository();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            //MainPage = new ShowCasePage();
            //return;

            m_App = this;
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(SearchSettingsPage), typeof(SearchSettingsPage));
            Routing.RegisterRoute(nameof(RequestSettingsPage), typeof(RequestSettingsPage));
//            Routing.RegisterRoute(nameof(CurrentMatchesPage), typeof(CurrentMatchesPage));
            Routing.RegisterRoute(nameof(GamesTypesPage), typeof(GamesTypesPage));
            Routing.RegisterRoute(nameof(PlayedMatchesPage), typeof(PlayedMatchesPage));
            Routing.RegisterRoute(nameof(RequestsPage), typeof(RequestsPage));
            Routing.RegisterRoute(nameof(DashBoardPage), typeof(DashBoardPage));
            Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));

            LoadShell();

            //           DependencyService.Register<MockDataStore>();

        }
        //private BasePlayerModel m_Profile;
        // public BasePlayerModel Profile
        //{
        //    get { return m_Profile; }
        //    set 
        //    {
        //        if (m_Profile == value) return;
        //        m_Profile = value; 
        //        OnPropertyChanged(nameof(Profile));
        //    }
        //}
        //public static BasePlayerModel ProfileModel
        //{
        //    get { return m_App.Profile; }
        //    set {  m_App.Profile = value; }
        //}
        public static void AdminCheck()
        {
            if (!m_IsUserAdmin)
            {
                m_App.LoadShell();
            }
        }
        public static void StandardMode()
        {
            m_App.MainPage = new AppShell();
        }
        public static void LoadAdmin()
        {
            if (!IsUserLoggedIn)
            {
                m_App.MainPage = new LoginShell();
            }
            else
            {
                m_App.MainPage = new AdminShell();
            }
        }
        private static PasswordConstraint m_PasswordConstraint = new PasswordConstraint
        {
            AllowNull=false,
            MinimumLength = 8,
            RequiresLowerCase = true,
            RequiresUpperCase = true,
            RequiresSpecialCharacter = true,
            RequiresNumber = true
        };
        public static PasswordConstraint PasswordConstraint
        {
            get => m_PasswordConstraint;
        }
        public void LoadShell()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new LoginShell();
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {
            SetMessageText($"{"Rajendra Notify"} TOKEN REC: {CrossPushNotification.Current.Token}");

            // Handle when your app starts
            CrossPushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                Xamarin.Essentials.Preferences.Set("TokenDevice",p.Token);
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
                SetMessageText($"TOKEN REC: {p.Token}");
            };

            CrossPushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                    if (p.Data.ContainsKey("body"))
                    {
                        SetMessageText($"{p.Data["body"]}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            };

            CrossPushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    SetMessageText(p.Identifier);
                }
                else if (p.Data.ContainsKey("color"))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //mPage.Navigation.PushAsync(new ContentPage()
                        //{
                        //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")
                        //});
                    });
                }
                else if (p.Data.ContainsKey("aps.alert.title"))
                {
                    SetMessageText($"{p.Data["aps.alert.title"]}");
                }
            };
            CrossPushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Dismissed");
            };
        }

        private void SetMessageText(string text)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
               // mPage.Message = text;
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
