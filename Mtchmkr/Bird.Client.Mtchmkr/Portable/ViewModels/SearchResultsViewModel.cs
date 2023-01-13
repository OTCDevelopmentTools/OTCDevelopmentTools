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

            List<PlayerBooking> lstObj = new List<PlayerBooking>();
            foreach (var item in SelectedPlayers)
            {
                PlayerBooking obj = new PlayerBooking();
                obj.createdByUser = Guid.Parse(Preferences.Get("UserId", string.Empty));
                obj.gameId = item.gameId;
                obj.locationId = LocationId;
                obj.requestedToUser = item.userId;
                obj.date = Convert.ToDateTime(BookingDate);
                obj.createdDate = DateTime.Now;
                obj.isAgreed = false;
                lstObj.Add(obj);
            }
            

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(lstObj);


            var Result = await PlayerBookingMethod(lstObj);
            if (Result)
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Player booking successfully.", "Ok");
                App.Current.MainPage.Navigation.PopToRootAsync(true);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Constants.APP_NAME, "Player booking failed for match.", "Ok");
            }

            _progDialog.HideProgress();
            
        }

        public async Task<bool> PlayerBookingMethod(List<PlayerBooking> _request)
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