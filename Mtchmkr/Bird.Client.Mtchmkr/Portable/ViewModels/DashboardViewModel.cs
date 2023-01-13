using System.Windows.Input;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel(INavigation navigation):base(navigation)
        {

        }
        public ICommand StandardModeCommand { get => new Command(StandardMode); }

        void StandardMode()
        {
            App.StandardMode();
        }

    }
}