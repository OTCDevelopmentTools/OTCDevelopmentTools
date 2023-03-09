using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Bird.Client.Mtchmkr.Portable.Views;
using Bird.Client.Mtchmkr.Business.ServiceCenter.Response;
using System.Collections.ObjectModel;
using System;
using Xamarin.Essentials;
using Bird.Client.Mtchmkr.Business.Common;
using Bird.Client.Mtchmkr.Portable.Interfaces;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Bird.Client.Mtchmkr.Portable.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Bird.Client.Mtchmkr.Portable.ViewModels
{
    public class SearchResultsViewModel : BaseViewModel
    {
        private readonly IProgressDialog _progDialog;

        private ObservableCollection<PlayerDTO> playersItemsSource;
        public ObservableCollection<PlayerDTO> PlayersItemsSource
        {
            get
            {
                return playersItemsSource;
            }
            set
            {
                playersItemsSource = value;
                OnPropertyChanged();
            }
        }

        private List<PlayerDTO> selectedPlayers;
        public List<PlayerDTO> SelectedPlayers
        {
            get
            {
                return selectedPlayers;
            }
            set
            {
                selectedPlayers = value;
                OnPropertyChanged();
            }
        }



        private Guid locationId;
        public Guid LocationId
        {
            get
            {
                return locationId;
            }
            set
            {
                locationId = value;
                OnPropertyChanged();
            }
        }

        private DateTime bookingDate;
        public DateTime BookingDate
        {
            get
            {
                return bookingDate;
            }
            set
            {
                bookingDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand BookCommand { get => new Command(() => CreateBooking()); }

        public SearchResultsViewModel(INavigation navigation):base(navigation)
        {
            _progDialog = DependencyService.Get<IProgressDialog>();
            PlayersItemsSource = new ObservableCollection<PlayerDTO>();
            SelectedPlayers = new List<PlayerDTO>();
        }

        async Task CreateBooking()
        {
            if (SelectedPlayers == null)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Please select player.", "Ok");
                return;
            }
            _progDialog.ShowProgress("Loading...");

            PlayerBooking obj = new PlayerBooking();
            obj.createdByUser = Guid.Parse(Preferences.Get("UserId", string.Empty)).ToString();
            obj.locationId = LocationId.ToString();
            obj.createdByUser = Preferences.Get("UserId", string.Empty);
            obj.matchDate = Convert.ToDateTime(BookingDate);
            obj.createdDate = DateTime.Now;
            obj.matchRequestUsers = new List<string>();
            obj.matchId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

            foreach (var item in SelectedPlayers)
            {
                obj.gameId = item.gameId.ToString();
                obj.matchRequestUsers.Add(item.userId.ToString());
            }

           
            var Result = await PlayerBookingMethod(obj);
            if (Result)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "MTCH request sent.", "Ok");
                await App.Current.MainPage.Navigation.PopToRootAsync(true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "MTCH request failed for MTCH.", "Ok");
            }
            _progDialog.HideProgress();
            
        }

        public async Task<bool> PlayerBookingMethod(PlayerBooking _request)
        {
            var result = await App.ServiceManager.PlayerBookingAsync(_request);
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