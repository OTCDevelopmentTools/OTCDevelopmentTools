using Bird.Client.Mtchmkr.Portable.Comon;
using Bird.Client.Mtchmkr.Portable.Views;
//using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private Shell m_Shell;
        private string m_EventNotification = "99901";
        private Color m_NotificationBackColour = Color.Red;

        public AppShellViewModel(Shell shell)
        {
            m_Shell= shell;
            
        }
        public ICommand SignOutCommand { get => new Command(async () => await SignOut()); }
        public ICommand GameTypesCommand { get => new Command(async () => await Open(nameof(GamesTypesPage))); }
        public ICommand RequestSettingsCommand { get => new Command(async () => await Open(nameof(RequestSettingsPage))); }
        public ICommand SearchSettingsCommand { get => new Command(async () => await Open(nameof(SearchSettingsPage))); }
        public ICommand OpenCommand { get => new Command(async () => await Open(nameof(GamesTypesPage))); }
        public ICommand AdminCommand { get => new Command(async () => await Admin()); }

        public ICommand LogOutCommand { get => new Command(async () => await Logout()); }

        async Task Logout()
        {
           App.Current.MainPage = new LoginShell();
        }

        private bool m_IsAdmin = false;

        public bool IsAdmin
        {
            get { return m_IsAdmin; }
            set
            {
                if (m_IsAdmin == value) return;
                m_IsAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        private async Task Open(string route)
        {
            await m_Shell.GoToAsync(route);
            Shell.Current.FlyoutIsPresented = false;
        }
        private async Task Admin()
        {
            App.LoadAdmin();
        }
        private async Task SignOut()
        {
            Xamarin.Essentials.Preferences.Remove(Constants.Is_User_Logged_In);
            
     //       m_Navigation.GoToLoginFlow
        }

        public string EventNotification
        {
            get { return m_EventNotification; }
            set 
            {
                if (m_EventNotification == value) return;
                m_EventNotification = value;
                OnPropertyChanged(nameof(EventNotification));
            }
        }

        public Color NotificationBackColour
        {
            get { return m_NotificationBackColour; }
            set
            {
                if (m_NotificationBackColour == value) return;
                m_NotificationBackColour = value;
                OnPropertyChanged(nameof(NotificationBackColour));
            }
        }
    }
}