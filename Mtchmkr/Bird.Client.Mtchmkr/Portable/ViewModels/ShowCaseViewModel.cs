using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Helpers;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class ShowCaseViewModel : CollectionViewModel<ProposedMatchModel>
    {
        private readonly IProgressDialog _progDialog;

        private ObservableCollection<ShowCaseResponse> showCaseItemSource;
        public ObservableCollection<ShowCaseResponse> ShowCaseItemSource
        {
            get
            {
                return showCaseItemSource;
            }
            set
            {
                showCaseItemSource = value;
                OnPropertyChanged();
            }
        }

        private bool isNoDataView = false;
        public bool IsNoDataView
        {
            get
            {
                return isNoDataView;
            }
            set
            {
                isNoDataView = value;
                OnPropertyChanged();
            }
        }

        public ShowCaseViewModel(INavigation navigation):base(navigation)
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            SendFCMToken();
        }
        protected override void LoadData()
        {
            base.LoadData();
            //var items = ProposedHelper.Create(20);
            //Collection = items;
        }
        private Command _Command;

       public ICommand BookCommand { get => new Command<ShowCaseResponse>(async (s) => await Book(s)); }

        async Task Book(ShowCaseResponse s)
        {
            ShowCaseResponse model = s as ShowCaseResponse;
            await Navigation.PushAsync(new BookNowPage(s));
        }

        public async void GetShowCaseData()
        {
            if (_progDialog.IsShowing)
                return;
            _progDialog.ShowProgress("Loading...");
            var userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
            var result = await App.ServiceManager.GetShowCaseDataAsync(userId);
            if (result != null && result.Count > 0)
            {
                ShowCaseItemSource = new ObservableCollection<ShowCaseResponse>(result);
                IsNoDataView = false;
            }
            else
            {
                ShowCaseItemSource = new ObservableCollection<ShowCaseResponse>();
                IsNoDataView = true;
            }

            _progDialog.HideProgress();
        }

        private async void SendFCMToken()
        {
            FcmDeviceInfo requestFcmInfo = new FcmDeviceInfo();
            requestFcmInfo.deviceId = DependencyService.Get<IBaseUrl>().GetIdentifier();
            requestFcmInfo.deviceToken = Xamarin.Essentials.Preferences.Get("TokenDevice", string.Empty); ;
            requestFcmInfo.deviceType = DeviceInfo.Platform.ToString();
            requestFcmInfo.userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
            requestFcmInfo.createdDate = DateTime.Now;
            await App.ServiceManager.InsertFCMInfoAsync(requestFcmInfo);
        }
    }
}