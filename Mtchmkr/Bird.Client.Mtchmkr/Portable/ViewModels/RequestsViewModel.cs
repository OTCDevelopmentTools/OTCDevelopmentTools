using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using Bird.Client.Mtchmkr.Portable.Models;
using Bird.Client.Mtchmkr.Portable.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class RequestsViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        private ObservableCollection<PendingMatchResponse> pendingMatchSource;
        public ObservableCollection<PendingMatchResponse> PendingMatchSource
        {
            get
            {
                return pendingMatchSource;
            }
            set
            {
                pendingMatchSource = value;
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

        public ICommand PlayNowCommand { get; }

        public RequestsViewModel() 
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            GetPendingMatchData();

            PlayNowCommand = new Command<PendingMatchResponse>(PlayMatchMethod);
        }

        public async void PlayMatchMethod(PendingMatchResponse obj)
        {
            var Item = obj as PendingMatchResponse;
            Preferences.Set("MatchId", Item.matchId.ToString());

            await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Match Booked Please Update Score Card.", "Ok");
        }

        public override void OnAppearing()
        {
            IsBusy = true;
            GetPendingMatchData();
        }


        public async void GetPendingMatchData()
        {
            _progDialog.ShowProgress("Loading...");
            var userId = Guid.Parse(Preferences.Get("UserId", string.Empty));
            var result = await App.ServiceManager.GetPendingMatchAsync(userId);
            if (result != null && result.Count > 0)
            {
                PendingMatchSource = new ObservableCollection<PendingMatchResponse>(result);
                IsNoDataView = false;
            }
            else
            {
                PendingMatchSource = new ObservableCollection<PendingMatchResponse>();
                IsNoDataView = true;
            }

            _progDialog.HideProgress();
        }
    }
}