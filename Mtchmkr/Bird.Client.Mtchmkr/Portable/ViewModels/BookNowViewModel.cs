using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class BookNowViewModel:BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        private ShowCaseResponse model;
        public ShowCaseResponse Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        public ICommand BookCommand { get => new Command(async () => await Book()); }

        async Task Book()
        {
            _progDialog.ShowProgress("Loading...");

            var Result = await PlayerBookingConfirmMethod(Model.matchId, true);
            if (Result)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "MTCH booking confirmed.", "Ok");
                App.Current.MainPage.Navigation.PopToRootAsync(true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "MTCH booking failed for MTCH.", "Ok");
            }

            await App.Current.MainPage.Navigation.PopAsync();

            _progDialog.HideProgress();
        }

        public BookNowViewModel(INavigation navigation , ShowCaseResponse model) :base(navigation)
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            Model = model;
            OnPropertyChanged(nameof(FirstName));
        }
        public string FirstName { get => Model.name; }

        public async Task<bool> PlayerBookingConfirmMethod(Guid id, bool status)
        {
            var result = await App.ServiceManager.ShowcaseGetIsAgreedResult(id,status);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}